using Microsoft.EntityFrameworkCore;

using Statistics.US;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using YamlDotNet;
using YamlDotNet.Serialization;

namespace Statistics;

internal class Methods
{
	private static int nAges => cdcAges.Count;
	private static readonly List<CDC.AgeGroup> cdcAges = new()
	{
		CDC.AgeGroup.Age0_4,
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
	private static readonly double popNormal = 100000.0;

	private struct DeadPool
	{
		public string Name { get; init; }

		// Raw numbers of deaths by age group
		public List<long> DeathsByAge { get; } = new();

		// Number of citizens in each age group
		public List<long> PopsByAge { get; } = new();

		// Per capita deaths in each age group
		public List<double> DeathsByAgePerCap { get; } = new();

		// Number of deaths expected in each age group based on national average
		public List<double> DeathsByAgeExpected { get; } = new();

		// Total number of deaths expected
		public double DeathsExpected => DeathsByAgeExpected.Sum() - DeathsByAgeExpected.Last();

		// Total dead people from corona in teh state
		public long TotalDeaths => DeathsByAge.Last();

		// Figure of merit: Total deaths as a multiple of expected deaths
		public double DeathNorm => TotalDeaths / DeathsExpected;
	}

	internal static string CompareStates(State state0, State state1)
	{
		Stopwatch watch = Stopwatch.StartNew();

		using Census.Context censusContext = new();
		using CDC.Context cdcContext = new();

		List<string> labels = new();
		List<long>[] pops = { new(), new() };
		List<long>[] deaths = { new(), new() };
		List<double>[] deathsPC = { new(), new() };
		List<double> diff = new();

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

		const int w0 = -18;
		const int w1 = 18;

		string s0 = state0.Abbreviation;
		string s1 = state1.Abbreviation;

		string line = "";
		for (int i = 0; i < w0 + w1 * 9; i++)
			line += '-';

		string returner = $"{"Age",w0}{$"{s0} Pop",w1}{$"{s0} Death",w1}{$"{s0} Death/{popNormal / 1000}k",w1}{$"{s1} Pop",w1}{$"{s1} Death",w1}{$"{s1} Death/{popNormal / 1000}k",w1}{$"{s0}/{s1} Per Cap",w1}\n";

		returner += line;

		for (int i = 0; i < deaths[1].Count; i++)
		{
			deathsPC[0].Add(popNormal * deaths[0][i] / pops[0][i]);
			deathsPC[1].Add(popNormal * deaths[1][i] / pops[1][i]);
			diff.Add(deathsPC[0][i] / deathsPC[1][i]);

			returner += $"\n{labels[i],w0}{pops[0][i],w1}{deaths[0][i],w1}{deathsPC[0][i],w1:f2}{pops[1][i],w1}{deaths[1][i],w1}{deathsPC[1][i],w1:f2}{diff[i],w1:f2}";

			if (i == cdcAges.Count - 2)
			{
				returner += '\n' + line;
			}
		}

		Debug.WriteLine(watch.Elapsed);
		return returner;
	}

	internal static string AgeAdjustedNormalDeathRate(State[] stateArray)
	{
		List<State> states = new(stateArray);

		Stopwatch watch = Stopwatch.StartNew();

		using Census.Context censusContext = new();
		using CDC.Context cdcContext = new();

		cdcContext.ChangeTracker.AutoDetectChangesEnabled = false;
		cdcContext.ChangeTracker.LazyLoadingEnabled = false;
		cdcContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

		watch.Restart();

		// Null for the entire US first in line.
		states.Insert(0, null);
		int nStates = states.Count;

		var labels = cdcAges.Select(x => x.Description());

		var deadPools = new List<DeadPool>();

		// Populate lists of deaths by age to compare with reference
		// reference is [0] for US total
		for (int i = 0; i < nStates; i++)
		{
			var dp = new DeadPool { Name = states[i]?.Name ?? "US" };

			foreach (var age in cdcAges)
			{
				dp.DeathsByAge.Add(CDC.Corona
					.GetDeathCount(states[i], age, cdcContext));

				dp.PopsByAge.Add(Census.cbg_b01
							.GetPopulation(states[i], age, censusContext));

				dp.DeathsByAgePerCap.Add((double)dp.DeathsByAge.Last() / dp.PopsByAge.Last());
			}
			deadPools.Add(dp);

			if (dp.Name == "New Mexico")
			{

			}

			Debug.WriteLine($"State {i}: {dp.Name} ({watch.Elapsed.TotalSeconds:f1} s)");
			watch.Restart();
		}

		var refDp = deadPools[0];
		foreach (var deadPool in deadPools)
		{
			for (int i = 0; i < nAges; i++)
			{
				deadPool
					.DeathsByAgeExpected
					.Add(refDp.DeathsByAgePerCap[i] * deadPool.PopsByAge[i]);
			}
		}

		const int w0 = -18;
		const int w1 = 18;

		string line = "";
		for (int i = 0; i < Math.Abs(w0) + Math.Abs(w1) * 3; i++)
			line += '-';

		string returner = $"{"State",w0}{$"Total Deaths",w1}{$"Expected Deaths",w1}{"Death Norm",w1}\n";

		returner += line;

		deadPools = deadPools.OrderBy(x => x.DeathNorm).ToList();

		foreach (var state in deadPools)
		{

			returner += $"\n{state.Name,w0}{state.TotalDeaths,w1}{state.DeathsExpected,w1:f0}{state.DeathNorm,w1:f2}";

			//if (i == 8)
			//{
			//	returner += '\n' + line;
			//}
		}

		Debug.WriteLine(watch.Elapsed);
		return returner;
	}


}
