using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Statistics.World;

public class Population
{
	public int Id { get; set; }
	public virtual Country Country { get; set; }

	public DateTime Date { get; set; }
	public double Value { get; set; }

	public static string Info => "Population";
	public static string Source => "IMF Cross Country Macroeconomic Statistics";

	/// <summary>
	/// Pull populations from the quandl api and store them in the database
	/// </summary>
	/// <returns>Returns true if successfull, false if unsuccessfull</returns>
	public static async Task<bool> Update()
	{
		bool returner = false;


		// Init
		using Context context = new();
		using HttpClient client = new();
		client.Timeout = TimeSpan.FromSeconds(2);
		client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
		int changes = 0;

		string message;
		string caption;

		try
		{
			// Throws ArgumentNull
			context.Populations.Include(pop => pop.Country).Load();
		}

		catch (ArgumentNullException ex)
		{
			message = $"Source was null when loading populations from database. Populations will not be cached.\n\n{ex.Message}";
			caption = "Database Error";
			MessageBox.Show(message, caption, MessageBoxButton.OK);
		}

		foreach (Country country in context.Countries)
		{
			string query = $"https://www.quandl.com/api/v3/datasets/ODA/{country.Code}_LP/data.json?api_key={Keys.Quandl}";

			try
			{
				// Throws InvalidOperation, HttpRequest, TaskCanceled  
				string result = await client.GetStringAsync(query);
				Newtonsoft.Json.Linq.JObject obj =
				JsonConvert
				.DeserializeObject(result) as Newtonsoft.Json.Linq.JObject;

				IEnumerable<Population> populations = obj
					.SelectToken("dataset_data")
					.SelectToken("data")
					.ToObject<List<List<string>>>()
					.Select(x => new Population()
					{
						Date = DateTime.Parse(x[0]),
						Value = double.Parse(x[1]),
						Country = country
					});

				Debug.WriteLine($"Found {populations.Count()} populations for {country.Name}");

				populations = populations
					.Where(y => !context.Populations
					.Any(w => w.Country == country && w.Date == y.Date));

				context.Populations.AddRange(populations);

				// Throws DbUpdate exception
				changes += await context.SaveChangesAsync();
				Debug.WriteLine($"{changes} changes");

			}

			catch (InvalidOperationException ex)
			{
				message = $"There was a problem with the URL query string while caching populations.\n\n{ex.Message}\n\nContinue?";
				caption = "URL Error";

				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (HttpRequestException ex)
			{
				message = $"There was a connection problem while caching populations.\n\n{ex.Message}\n\nContinue?";
				caption = "Connection Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (TaskCanceledException ex)
			{
				message = $"There was a timeout connecting to the database while caching populations.\n\n{ex.Message}\n\nContinue?";
				caption = "Connection Error";

				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (DbUpdateException ex)
			{
				message = $"There was a problem saving the database.\n\n{ex.Message}\n\nContinue?";
				caption = "Database Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (JsonSerializationException ex)
			{
				message = $"There was a problem deserializing the population data.\n\n{ex.Message}\n\nContinue?";
				caption = "Deserialization Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (Microsoft.Data.Sqlite.SqliteException ex)
			{
				message = $"There was a problem with the database while loading population.\n\n{ex.Message}\n\nContinue?";
				caption = "Database Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}
		}

		// Report to user
		message = $"{changes} populations saved to database.";
		caption = "Database Updated";
		MessageBox.Show(message, caption, MessageBoxButton.OK);

		returner = true;
		return returner;
	}

}

