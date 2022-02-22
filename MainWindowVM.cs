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
	public IEnumerable<State> States => UnitedStates.States;
	public State State0 { get; set; }
	public State State1 { get; set; }

	internal MainWindowVM()
	{
		UpdateCDCCmd = new Command(UpdateCorona, "Update CDC", "Update CDC data from database");
		CoronaBonusCmd = new Command(CoronaBonus, "Corona Bonus", "Do some Corona stuff");
		CompareStatesCmd = new Command(CompareStates, CompareStatesCaneExecute, "Compare States", "Compare two states corona stats");

		Commands.Add(UpdateCDCCmd);
		Commands.Add(CoronaBonusCmd);
		Commands.Add(CompareStatesCmd);
	}
	public Command UpdateCDCCmd { get; set; }
	public Command CoronaBonusCmd { get; set; }
	public Command CompareStatesCmd { get; set; }

	public static void UpdateCorona()
	{
		CDC.Corona.Update();
	}
	public static async void CoronaBonus()
	{
		CDC.Corona.Update();
	}

	public void CompareStates()
	{
		Display = Methods.CompareStates(State0, State1);
		OnPropertyChanged(nameof(Display));
	}

	private bool CompareStatesCaneExecute()
	{
		return State0 != null && State1 != null;
	}

	public event PropertyChangedEventHandler PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}

