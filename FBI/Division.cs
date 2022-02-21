using Microsoft.EntityFrameworkCore;

using Statistics.FBI;

using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Statistics.FBI;

public class Division
{
	public int ID { get; set; }

	public virtual Region Region { get; set; }
	public string Name { get; set; }

	public static void Load()
	{
		Context context = new ();
		context.Regions.Load();

		var list = new List<(string, int, int)>()
			{
				("Possesions",  0, 0),
				("New England", 1 , 1),
				("New England", 1 , 1),
				("Mid Atlantic", 2 , 1),
				("North Central", 3, 2),
				("West North Central", 4, 2),
				("South Atlantic", 5, 3),
				("East South Central", 6, 3),
				("West South Central", 7, 3),
				("Mountain", 8, 4),
				("Pacific", 9, 4)
			};

		IEnumerable<Division> divisions = list.Select(x => new Division
		{
			Name = x.Item1,
			ID = x.Item2,
			Region = context.Regions.First(y => y.ID == x.Item3)
		});

		context.Divisions.AddRange(divisions);

		try
		{
			context.SaveChanges();
		}

		catch (DbUpdateException ex)
		{
			string message = $"There was a problem saving the database while creating Crime.Divisions.\n\n{ex.Message}";
			string caption = "Database Error";
			MessageBoxResult r = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
		}
	}
}
