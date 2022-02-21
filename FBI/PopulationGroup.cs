using System.Collections.Generic;
using System.Linq;

namespace Statistics.FBI;

public class PopulationGroup
{
	public string Code { get; set; }

	public string Type { get; set; }
	public int Min { get; set; }
	public int Max { get; set; } = int.MaxValue;

	public static void Load()
	{
		Context context = new();


		var list = new List<(string, int, int, string)>()
			{
				("0", 0, int.MaxValue, "Possession" ),
				("1", 250000, int.MaxValue, "City" ),
				("1A", 1000000, int.MaxValue, "Possession" ),
				("1B", 500000, 999999, "City" ),
				("1C", 250000, 499999, "Possession" ),
				("2", 100000, 249999, "City" ),
				("3", 50000, 99999, "Possession" ),
				("4", 25000, 49999, "City" ),
				("5", 10000, 24999, "Possession" ),
				("6", 2500, 9999, "City" ),
				("7", 0, 2499, "City" ),
				("8", 0, int.MaxValue, "Non MSA County" ),
				("8A", 100000, int.MaxValue, "Non MSA County" ),
				("8B", 25000, 99999, "Non MSA County" ),
				("8C", 10000, 24999, "Non MSA County" ),
				("8D", 0, 9999, "Non MSA County" ),
				("8E", 0, int.MaxValue, "Non MSA County" ),
				("9", 0, int.MaxValue, "MSA County" ),
				("9A", 100000, int.MaxValue, "MSA County" ),
				("9B", 25000, 99999, "MSA County" ),
				("9C", 10000, 24999, "MSA County" ),
				("9D", 0, 9999, "MSA County" ),
				("9E", 0, int.MaxValue, "MSA State" ),
			};

		IEnumerable<PopulationGroup> popGroups = list.Select(x => new PopulationGroup
		{
			Code = x.Item1,
			Min = x.Item2,
			Max = x.Item3,
			Type = x.Item4
		});

		context.PopulationGroups.AddRange(popGroups);
		context.SaveChanges();
	}
}
