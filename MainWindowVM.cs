using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Statistics.MVVM;
using Statistics.US;

namespace Statistics;

internal class MainWindowVM : INotifyPropertyChanged
{
	public string Display { get; set; }
	public List<Command> Commands { get; set; } = new();
	internal MainWindowVM()
	{
		UpdateCDCCmd = new Command(UpdateCorona, "Update CDC", "Update CDC data from database");
		CoronaBonusCmd = new Command(CoronaBonus, "Corona Bonus", "Do some Corona stuff");
		GetFloridaCmd = new Command(GetFlorida, "Get Florida", "Get Florida");

		Commands.Add(UpdateCDCCmd);
		Commands.Add(CoronaBonusCmd);
		Commands.Add(GetFloridaCmd);
	}
	public Command UpdateCDCCmd { get; set; }
	public Command CoronaBonusCmd { get; set; }
	public Command GetFloridaCmd { get; set; }

	public static async void UpdateCorona()
	{
		await CDC.Corona.Update();
	}
	public static async void CoronaBonus()
	{
		CDC.Corona.Update();
	}

	internal void GetFlorida()
	{
		State state0 = UnitedStates.Florida;
		State state1 = UnitedStates.NewJersey;

		Stopwatch watch = Stopwatch.StartNew();

		using Census.Context censusContext = new();
		using CDC.Context cdcContext = new();

		var ages = (Census.AgeGroup[])Enum.GetValues(typeof(Census.AgeGroup));

		List<string> labels = new();
		List<long>[] pops = { new(), new() };
		List<long>[] deaths = { new(), new() };
		List<double>[] deathsPC = { new(), new() };
		List<double> diff = new();

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

		const int w = 18;
		const string f = "f3";
		string s0 = state0.Abbreviation;
		string s1 = state1.Abbreviation;
		Display = $"{"Age",w}{$"{s0} Pop",w}{$"{s0} Deaths",w}{$"{s0} Death PerCap",w}{$"{s1} Pop",w}{$"{s1} Deaths",w}{$"{s1} Death PerCap",w}{"Diff",w}\n";

		for (int i = 0; i < w * 8; i++)
		{
			Display += '-';
		}

		for (int i = 0; i < deaths[1].Count; i++)
		{
			deathsPC[0].Add(10000.0 * deaths[0][i] / pops[0][i]);
			deathsPC[1].Add(10000.0 * deaths[1][i] / pops[1][i]);
			diff.Add(deathsPC[0][i] / deathsPC[1][i]);

			Display += $"\n{labels[i],w}{pops[0][i],w}{deaths[0][i],w}{deathsPC[0][i],w:f2}{pops[1][i],w}{deaths[1][i],w}{deathsPC[1][i],w:f2}{diff[i],w:f2}";
		}

		Debug.WriteLine(watch.Elapsed);
		OnPropertyChanged(nameof(Display));
	}

	public event PropertyChangedEventHandler PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] String propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

}

