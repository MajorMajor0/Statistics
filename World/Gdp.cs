using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Statistics.World;

public class Gdp
{
	public int Id { get; set; }
	public virtual Country Country { get; set; }

	public DateTime Date { get; set; }
	public double Value { get; set; }

	public static string Info => "Total GDP, Current prices, US dollars";
	public static string Source => "UN National Accounts Estimates";

	/// <summary>
	/// Pull gdp from the quandl api and store them in the database
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
			context.Gdps.Include(pop => pop.Country).Load();
		}

		catch (ArgumentNullException ex)
		{
			message = $"Source was null when loading populations from database. GDP will not be cached.\n\n{ex.Message}";
			caption = "Database Error";
			MessageBox.Show(message, caption, MessageBoxButton.OK);
		}

		foreach (Country country in context.Countries)
		{
			try
			{
				string query = $"https://www.quandl.com/api/v3/datasets/UNAE/GDPCD_{country.Code}/data.json?api_key={Keys.Quandl}";

				// Throws InvalidOperation, HttpRequest, TaskCanceled  
				string result = await client.GetStringAsync(query);
				Newtonsoft.Json.Linq.JObject obj =
				JsonConvert
				// Swallows JsonSerializationException
				.DeserializeObject(result) as Newtonsoft.Json.Linq.JObject;

				IEnumerable<Gdp> gdps = obj
					.SelectToken("dataset_data")
					.SelectToken("data")
					.ToObject<List<List<string>>>()
					.Select(x => new Gdp()
					{
						Date = DateTime.Parse(x[0]),
						Value = double.Parse(x[1]),
						Country = country
					});

				Debug.WriteLine($"Found {gdps.Count()} gdps for {country.Name}");

				gdps = gdps
					.Where(y => !context.Gdps
					.Any(w => w.Country == country && w.Date == y.Date));

				context.Gdps.AddRange(gdps);

				// Throws DbUpdate exception
				int c = await context.SaveChangesAsync();
				changes += c;
				Debug.WriteLine($"{c} ({changes}) changes");
			}

			catch (InvalidOperationException ex)
			{
				message = $"There was a problem with the URL query string while caching gdp.\n\n{ex.Message}\n\nContinue?";
				caption = "URL Error";

				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (HttpRequestException ex)
			{
				message = $"There was a connection problem while caching gdp.\n\n{ex.Message}\n\nContinue?";
				caption = "Connection Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (TaskCanceledException ex)
			{
				message = $"There was a timeout connecting to the database while caching gdp.\n\n{ex.Message}\n\nContinue?";
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
				message = $"There was a problem deserializing the gdp data.\n\n{ex.Message}\n\nContinue?";
				caption = "Deserialization Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (Microsoft.Data.Sqlite.SqliteException ex)
			{
				message = $"There was a problem with the database while loading gdp.\n\n{ex.Message}\n\nContinue?";
				caption = "Database Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}
		}

		// Report to user
		message = $"{changes} gdp changes saved to database.";
		caption = "Database Updated";
		MessageBox.Show(message, caption, MessageBoxButton.OK);

		returner = true;
		return returner;
	}
}
