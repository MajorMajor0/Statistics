using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Statistics.FBI;

public class Context : DbContext
{
	public DbSet<Region> Regions { get; set; }
	public DbSet<Division> Divisions { get; set; }
	public DbSet<Agency> Agencies { get; set; }
	public DbSet<PopulationGroup> PopulationGroups { get; set; }

	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(
			$"Data Source={FileLocations.FBI}");

		optionsBuilder.UseLazyLoadingProxies();
		base.OnConfiguring(optionsBuilder);
	}

	public static void Load()
	{
		Region.Load();
		Division.Load();

		string line;
		int i = 0;
		string file = "2019_NIBRS_NATIONAL_MASTER_FILE_ENC_STATIC.txt";

		using Context context = new();
		using StreamReader reader = File.OpenText(file);

		try
		{
			//context.States.Load();

			while ((line = reader.ReadLine()) != null && i++ <= 5)
			{
				Console.WriteLine(line);

				// Process batch header
				if (line[..1] == "BH")
				{
					string stateAbbreviation = line[71..72];
					string cityName = line[41..70].Trim();


					Agency agency = new()
					{
						//State = context.States.First(x => x.Abbreviation == stateAbbreviation)

					};
				}

				// Process administrative segment
				else if (line[..1] == "01")
				{

				}


			}
		}
		catch (OutOfMemoryException ex)
		{
			MessageBoxResult mbr = MessageBox.Show(ex.Message, "Error", MessageBoxButton.YesNo);
			if (mbr is MessageBoxResult.No)
			{
				return;
			}

		}
		catch (IOException ex)
		{

		}

		//Agency GetAgencyFromBatchHeader(string line)
		//{


		//}

		//void ParseAdminSegment(string line)
		//{
		//}

		return;
	}
}
