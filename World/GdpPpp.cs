using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Statistics.World;

public class GdpPpp
{
	public int Id { get; set; }
	public virtual Country Country { get; set; }

	public DateTime Date { get; set; }
	public double Value { get; set; }

	public static string Info => "This indicator provides values for gross domestic product (GDP) expressed in current international dollars, converted by purchasing power parity (PPP) conversion factor.  GDP is the sum of gross value added by all resident producers in the country plus any product taxes and minus any subsidies not included in the value of the products. PPP conversion factor is a spatial price deflator and currency converter that eliminates the effects of the differences in price levels between countries.  From April 2020, Ã¢Â€ÂœGDP: linked series (current LCU)Ã¢Â€Â [NY.GDP.MKTP.CN.AD] is used as underlying GDP in local currency unit so that itÃ¢Â€Â™s in line with time series of PPP conversion factors for GDP, which are extrapolated with linked GDP deflators. Source: International Comparison Program, World Bank ; World Development Indicators database, World Bank ; Eurostat-OECD PPP Programme.";
	public static string Source => "World Bank";

	/// <summary>
	/// Pull GDP PPP from the quandl api and store them in the database
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
			context.GdpPpps.Include(ppp => ppp.Country).Load();
		}

		catch (ArgumentNullException ex)
		{
			message = $"Source was null when loading gdp ppp from database. Gdp ppp will not be cached.\n\n{ex.Message}";
			caption = "Database Error";
			MessageBox.Show(message, caption, MessageBoxButton.OK);
		}

		catch (Microsoft.Data.Sqlite.SqliteException ex)
		{
			message = $"There was a problem with the database while loading gdp ppp. Gdp ppp will not be cached\n\n{ex.Message}";
			caption = "Database Error";
			MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.OK);
			return false;
		}

		foreach (Country country in context.Countries)
		{
			//string query = $"https://www.quandl.com/api/v3/datasets/ODA/{country.Code}_LP/data.json?api_key={Keys.Quandl}";

			string seriesId = "NY.GDP.MKTP.PP.CD";
			string query = $"https://www.quandl.com/api/v3/datatables/WB/DATA?series_id={seriesId}&country_code={country.Code}&api_key={Keys.Quandl}";
			try
			{
				// Throws InvalidOperation, HttpRequest, TaskCanceled  
				string result = await client.GetStringAsync(query);
				Newtonsoft.Json.Linq.JObject obj =
				JsonConvert
				.DeserializeObject(result) as Newtonsoft.Json.Linq.JObject;

				var x = obj
					.SelectToken("datatable")
					.SelectToken("data")
					.ToObject<List<List<string>>>();

				IEnumerable<GdpPpp> gdpPpps = x.Select(x => new GdpPpp()
				{
					Date = DateTime.ParseExact(x[3], "yyyy", CultureInfo.InvariantCulture),
					Value = double.Parse(x[4]),
					Country = country
				});

				Debug.WriteLine($"Found {gdpPpps.Count()} gdp ppp for {country.Name}");

				gdpPpps = gdpPpps
					.Where(y => !context.GdpPpps
					.Any(w => w.Country == country && w.Date == y.Date));

				context.GdpPpps.AddRange(gdpPpps);

				// Throws DbUpdate exception
				int c = await context.SaveChangesAsync();

				changes += c;
				Debug.WriteLine($"{c} ({changes}) changes");
			}

			catch (InvalidOperationException ex)
			{
				message = $"There was a problem with the URL query string while caching gdp ppp.\n\n{ex.Message}\n\nContinue?";
				caption = "URL Error";

				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (HttpRequestException ex)
			{
				message = $"There was a connection problem while caching gdp ppp.\n\n{ex.Message}\n\nContinue?";
				caption = "Connection Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			catch (TaskCanceledException ex)
			{
				message = $"There was a timeout connecting to the database while caching gdp ppp.\n\n{ex.Message}\n\nContinue?";
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
				message = $"There was a problem deserializing the gdp ppp data.\n\n{ex.Message}\n\nContinue?";
				caption = "Deserialization Error";
				MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

				if (r == MessageBoxResult.No)
				{
					break;
				}
			}

			//catch (Microsoft.Data.Sqlite.SqliteException ex)
			//{
			//	message = $"There was a problem with the database while loading gdp ppp.\n\n{ex.Message}\n\nContinue?";
			//	caption = "Database Error";
			//	MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

			//	if (r == MessageBoxResult.No)
			//	{
			//		break;
			//	}
			//}
		}

		// Report to user
		message = $"{changes} gdp ppps saved to database.";
		caption = "Database Updated";
		MessageBox.Show(message, caption, MessageBoxButton.OK);

		returner = true;
		return returner;
	}

}

