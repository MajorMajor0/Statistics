using Statistics.US;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Statistics;

internal class Methods
{
	internal static string CompareStates(State state0, State state1)
	{
		Stopwatch watch = Stopwatch.StartNew();

		using Census.Context censusContext = new();
		using CDC.Context cdcContext = new();

		var ages = (Census.AgeGroup[])Enum.GetValues(typeof(Census.AgeGroup));

		List<string> labels = new();
		List<long>[] pops = { new(), new() };
		List<long>[] deaths = { new(), new() };
		List<double>[] deathsPC = { new(), new() };
		List<double> diff = new();
		double m = 100000.0;

		List<CDC.AgeGroup> cdcAges = new()
		{
			CDC.AgeGroup.Age5_14,
			CDC.AgeGroup.Age15_24,
			CDC.AgeGroup.Age25_34,
			CDC.AgeGroup.Age35_44,
			CDC.AgeGroup.Age45_54,
			CDC.AgeGroup.Age55_64,
			CDC.AgeGroup.Age65_74,
			CDC.AgeGroup.Age75_84,
			CDC.AgeGroup.Age85up,
			CDC.AgeGroup.All
		};

		foreach (var age in cdcAges)
		{
			labels.Add(age.Description());

			pops[0].Add(Census.cbg_b01
						.GetPopulation(state0, age, censusContext));

			pops[1].Add(Census.cbg_b01
						.GetPopulation(state1, age, censusContext));

			deaths[0].Add(CDC.Corona
				.GetDeathCount(state0, age, cdcContext));

			deaths[1].Add(CDC.Corona
				.GetDeathCount(state1, age, cdcContext));
		}

		const int w0 = 18;
		const int w1 = 18;

		string s0 = state0.Abbreviation;
		string s1 = state1.Abbreviation;

		string line = "";
		for (int i = 0; i < w0 + w1 * 7; i++)
			line += '-';

		string returner = $"{"Age",w0}{$"{s0} Pop",w1}{$"{s0} Death",w1}{$"{s0} Death/{m/1000}k",w1}{$"{s1} Pop",w1}{$"{s1} Death",w1}{$"{s1} Death/{m/1000}k",w1}{$"{s0}/{s1} Per Cap",w1}\n";

		returner += line;

		for (int i = 0; i < deaths[1].Count; i++)
		{
			deathsPC[0].Add(m * deaths[0][i] / pops[0][i]);
			deathsPC[1].Add(m * deaths[1][i] / pops[1][i]);
			diff.Add(deathsPC[0][i] / deathsPC[1][i]);

			returner += $"\n{labels[i],w0}{pops[0][i],w1}{deaths[0][i],w1}{deathsPC[0][i],w1:f2}{pops[1][i],w1}{deaths[1][i],w1}{deathsPC[1][i],w1:f2}{diff[i],w1:f2}";

			if (i == 8)
			{
				returner += '\n' + line;
			}
		}

		Debug.WriteLine(watch.Elapsed);
		return returner;
	}
}
