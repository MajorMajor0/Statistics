using Microsoft.EntityFrameworkCore;

using SODA;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Statistics.CDC;

public class Corona
{
	public int Id { get; set; }

	public DateTime DataAsOf { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public string State { get; set; }
	public string AgeGroup { get; set; }
	public string Group { get; set; }
	public string Sex { get; set; }


	public int TotalDeaths { get; set; }

	public int CovidDeaths { get; set; }
	public int FluDeaths { get; set; }
	public int PneumoniaDeaths { get; set; }
	public int PneumoniaAndCovidDeaths { get; set; }
	public int PneumoniaFluAndCovidDeaths { get; set; }

	internal static string Info => "Coronavirus death counts";
	internal static string Source => "CDC";

	private static readonly string resource = "9bhg-hcku";

	/// <summary>
	/// Wipes coronavirus deaths from the database and pulls the lates from the CDC to replace it.
	/// tryies to check whether the new data is at least apparently good before wiping the database.	/// </summary>
	/// <returns>Returns true if successfull, false if unsuccessfull</returns>
	internal static void Update()
	{
		string message;
		string caption;
		using Context context = new();

		context.Database.EnsureCreated();

		// Init
		var client = new SodaClient("https://data.cdc.gov", Keys.CDCAppToken);

		// Get a reference to the resource itself
		// The result (a Resouce object) is a generic type
		// The type parameter represents the underlying rows of the resource
		// and can be any JSON-serializable class
		try
		{
			context.Coronas.Load();

			int oldCount = context.Coronas.Local.Count;

			var dataset = client.GetResource<Dictionary<string, string>>(resource);
			var coronas = dataset.GetRows().Select(x => x.ToCorona());

			int newCount = coronas.Count();

			if (oldCount > newCount)
			{
				message = $"The DB currently has {oldCount} rows. The data found at CDC resource {resource} has only {newCount} rows. Do you want to take the new data?\n";
				caption = "Not Enough Rows";
				MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
				if (result == MessageBoxResult.No)
				{
					return;
				}
			}

			context.Coronas.AddRange(coronas);

			// Throws dbupdate exception
			int changes = context.SaveChanges();

			// Report to user
			message = $"{changes} corona data rows saved to database.";
			caption = "Database Updated";
			MessageBox.Show(message, caption, MessageBoxButton.OK);
		}

		catch (ArgumentOutOfRangeException ex)
		{
			message = $"Problem with the connection to {resource} (CDC Corona data)\n\n{ex.Message}";
			caption = "Connection error";
			MessageBox.Show(message, caption, MessageBoxButton.OK);
		}
		catch (DbUpdateException ex)
		{
			message = $"There was a problem saving the database.\n\n{ex.Message}";
			caption = "Database Error";
			MessageBox.Show(message, caption, MessageBoxButton.OK);
		}
		catch (Exception ex)
		{
			message = $"Unknown exception caching Corona virus data from the CDC\n\n{ex.Message}";
			caption = "Problem getting corona data";
			MessageBox.Show(message, caption, MessageBoxButton.OK);
		}

		return;
	}

	/// <summary>
	/// Returns a death count filtered by the parameters
	/// </summary>
	/// <param name="state">US.State or null. If null, returns entire US</param>
	/// <param name="ageGroup">CDC.agegroup for filtering</param>
	/// <param name="context">CDC.Context</param>
	/// <returns>Returns a total death count for the given parameters</returns>
	internal static long GetDeathCount(US.State state, AgeGroup ageGroup, Context context)
	{
		string stateName = state?.Name ?? "United States";

		if (ageGroup == CDC.AgeGroup.Age0_4)
		{
			return GetDeathCount(state, CDC.AgeGroup.Age0_1, context) +
			 GetDeathCount(state, CDC.AgeGroup.Age1_4, context);
		}

		var deaths = context
			.Coronas
			.Where(x => x.State == stateName)
			.Where(x => x.AgeGroup == ageGroup.Description())
			.Where(x => x.StartDate == new DateTime(2020, 01, 01))
			.Where(x => x.EndDate == new DateTime(2022, 02, 12))
			.Where(x => x.Group == "By Total")
			.Where(x => x.Sex == "All Sexes")
			.Sum(x => x.CovidDeaths);

		if(state == US.UnitedStates.NewYork)
		{
			deaths += context
			.Coronas
			.Where(x => x.State == "New York City")
			.Where(x => x.AgeGroup == ageGroup.Description())
			.Where(x => x.StartDate == new DateTime(2020, 01, 01))
			.Where(x => x.EndDate == new DateTime(2022, 02, 12))
			.Where(x => x.Group == "By Total")
			.Where(x => x.Sex == "All Sexes")
			.Sum(x => x.CovidDeaths);
		}

		return deaths;
	}
}



