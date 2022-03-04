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
	public static List<State> States;
	public State State0 { get; set; }
	public State State1 { get; set; }

	public MainWindowVM()
	{
		UpdateCDCCmd = new Command(UpdateCorona, "Update CDC", "Update CDC data from database");
		CoronaBonusCmd = new Command(AgeAdjustedDeathNorm, "Age Adj. Norm");
		CompareStatesCmd = new Command(CompareStates, CompareStatesCanExecute, "Compare States", "Compare two states corona stats");
		LoadCongressionalDistrictsCmd = new Command(LoadCongressionalDistricts, "Bonus");
		LoadCongressmenCmd = new Command(LoadCongressmen, "Load Congressmen");

		Commands.Add(UpdateCDCCmd);
		Commands.Add(CoronaBonusCmd);
		Commands.Add(CompareStatesCmd);
		Commands.Add(LoadCongressionalDistrictsCmd);
		Commands.Add(LoadCongressmenCmd);

		using  US.Context context = new();
		States = context.States.AsNoTracking().ToList();
	}
	public Command UpdateCDCCmd { get; set; }
	public Command CoronaBonusCmd { get; set; }
	public Command CompareStatesCmd { get; set; }
	public Command LoadCongressionalDistrictsCmd { get; set; }
	public Command LoadCongressmenCmd { get; set; }

	public static void UpdateCorona()
	{
		CDC.Corona.Update();
	}

	public void AgeAdjustedDeathNorm()
	{
		Display = Methods.AgeAdjustedNormalDeathRate(States.ToArray());
		OnPropertyChanged(nameof(Display));
	}

	public void CompareStates()
	{
		Display = Methods.CompareStates(State0, State1);
		OnPropertyChanged(nameof(Display));
	}

	public static void LoadCongressionalDistricts()
	{
		Loaders.CongressionalDistricts();
	}

	public static void LoadCongressmen()
	{
		Loaders.Congressmen(@"C:\data\congress\legislators-historical.yaml");
		Loaders.Congressmen(@"C:\data\congress\legislators-current.yaml");
	}
	private bool CompareStatesCanExecute()
	{
		return State0 != null && State1 != null;
	}

	public event PropertyChangedEventHandler PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}

