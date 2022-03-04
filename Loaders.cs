using Microsoft.EntityFrameworkCore;

using Statistics.US;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

namespace Statistics;

internal class Loaders
{
	/// <summary>
	/// Run this after updating census_block_cd116
	/// </summary>
	internal static void CongressionalDistricts()
	{
		var context = new Census.Context();
		context.ChangeTracker.AutoDetectChangesEnabled = false;

		var watch = Stopwatch.StartNew();

		int i = 0;

		foreach (var cbg in context.cbg_b01)
		{
			i++;
			if (cbg.CongressionalDistrict is not null)
			{
				continue;
			}

			var cds = context
				.census_bloc_cd116s
				.AsNoTracking()
				.Where(x => x.BLOCK_GROUP == cbg.census_block_group)
				.ToArray();

			// If there are duplicates
			if (cds.Any(x => x.CD116 != cds[0].CD116))
			{
				// Get the most common value
				var mode = cds.GroupBy(x => x.CD116)
					.OrderByDescending(g => g.Count())
					.Select(g => g.Key)
					.FirstOrDefault();

				// Try to parse it
				if (int.TryParse(mode, out int cd))
				{
					cbg.CongressionalDistrict = cd;
				}
				else
				{
					Debug.WriteLine($"CD116 on {cds[0].BLOCK_GROUP} did not parse to an integer.");
				}
			}
			else if (!cds.Any())
			{
				Debug.WriteLine($"CBG {cbg.census_block_group} is not in a district.");
			}
			else
			{
				if (int.TryParse(cds[0].CD116, out int cd))
				{
					cbg.CongressionalDistrict = cd;
				}
				else
				{
					Debug.WriteLine($"CD116 on {cds[0].BLOCK_GROUP} did not parse to an integer.");
				}
			}

			if (i % 1000 == 0)
			{
				context.ChangeTracker.DetectChanges();
				context.SaveChanges();
				Debug.WriteLine($"{i} / {220333} ({ watch.Elapsed.TotalSeconds:f0} s)");
				watch.Restart();
			}
		}
	}


	internal static void Congressmen(string filename)
	{
		using US.Context context = new();

		using TextReader reader = File.OpenText(filename);

		var deserializer = new Deserializer();

		var dicts = deserializer.Deserialize<List<Dictionary<string, object>>>(reader);

		int i = 0;
		int n = dicts.Count;

		List<Congressman> congressmen = new();

		foreach (var dict in dicts)
		{
			var congressman = dict.ToCongressman();

			congressmen.Add(congressman);

			Debug.WriteLine($"Congressman {i++} / {n}");
		}

		context.Congressmen.AddRange(congressmen);
		int ch = context.SaveChanges();
		Debug.WriteLine($"{ch} changes");
	}

}
