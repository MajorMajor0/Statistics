using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Statistics.FBI;

public class Region
{
	public int ID { get; set; }
	public string Name { get; set; }

	public static void Load()
	{
		Context context = new ();

		var list = new List<(string, int)>()
			{
				( "Northeast ", 1 ),
				( "North Central", 2 ),
				( "South", 3 ),
				( "West", 4 ),
			};

		IEnumerable<Region> regions = list.Select(x => new Region
		{
			Name = x.Item1,
			ID = x.Item2
		});

		context.Regions.AddRange(regions);

		try
		{
			context.SaveChanges();
		}

		catch (DbUpdateException ex)
		{
			string message = $"There was a problem saving the database while creating Crime.Regions.\n\n{ex.Message}";
			string caption = "Database Error";
			MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
		}
	}
}
