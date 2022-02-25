using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Statistics.Census;

public partial class cbg_b01
{
	internal static long GetPopulation(US.State state, CDC.AgeGroup ageGroup, Context context)
	{
		return ageGroup switch
		{
			CDC.AgeGroup.Age0_4 => GetPopulation(state, AgeGroup.Age0_4, context),
			CDC.AgeGroup.Age5_14 => GetPopulation(state, AgeGroup.Age5_14, context),
			CDC.AgeGroup.Age15_24 => GetPopulation(state, AgeGroup.Age15_24, context),
			CDC.AgeGroup.Age25_34 => GetPopulation(state, AgeGroup.Age25_34, context),
			CDC.AgeGroup.Age35_44 => GetPopulation(state, AgeGroup.Age35_44, context),
			CDC.AgeGroup.Age45_54 => GetPopulation(state, AgeGroup.Age45_54, context),
			CDC.AgeGroup.Age55_64 => GetPopulation(state, AgeGroup.Age55_64, context),
			CDC.AgeGroup.Age65_74 => GetPopulation(state, AgeGroup.Age65_74, context),
			CDC.AgeGroup.Age75_84 => GetPopulation(state, AgeGroup.Age75_84, context),
			CDC.AgeGroup.Age85up => GetPopulation(state, AgeGroup.Age85up, context),
			CDC.AgeGroup.All => GetPopulation(state, AgeGroup.All, context),
			_ => throw new ArgumentOutOfRangeException("Unhandled age range."),
		};
	}

	/// <summary>
	/// Get populations filtered by state and age
	/// </summary>
	/// <param name="state">US.State or null. If null, total population for the US</param>
	/// <param name="ageGroup">Census .agegroup for filtering</param>
	/// <param name="context">Census.Context</param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	internal static long GetPopulation(US.State state, AgeGroup ageGroup, Context context)
	{
		Stopwatch watch = Stopwatch.StartNew();

		IEnumerable<cbg_b01> rows;
		if (state is null)
		{
			rows = context.cbg_b01;
		}
		else
		{
			rows = context
				.cbg_b01
				.Where(x => x.State == state.FIPS);
		}

		return ageGroup switch
		{
			AgeGroup.Age0_4 => rows.Sum(x => x.B01001e27 + x.B01001e3),
			AgeGroup.Age5_9 => rows.Sum(x => x.B01001e28 + x.B01001e4),
			AgeGroup.Age5_14 => rows.Sum(x => x.B01001e28 + x.B01001e29 + x.B01001e4 + x.B01001e5),
			AgeGroup.Age10_14 => rows.Sum(x => x.B01001e29 + x.B01001e5),
			AgeGroup.Age15_17 => rows.Sum(x => x.B01001e30 + x.B01001e6),
			AgeGroup.Age15_24 => rows.Sum(x => x.B01001e30 + x.B01001e31 + x.B01001e32 + x.B01001e33 + x.B01001e34 +
			x.B01001e6 + x.B01001e7 + x.B01001e8 + x.B01001e9 + x.B01001e10),
			AgeGroup.Age18_19 => rows.Sum(x => x.B01001e31 + x.B01001e7),
			AgeGroup.Age20 => rows.Sum(x => x.B01001e32 + x.B01001e8),
			AgeGroup.Age21 => rows.Sum(x => x.B01001e33 + x.B01001e9),
			AgeGroup.Age22_24 => rows.Sum(x => x.B01001e34 + x.B01001e10),
			AgeGroup.Age25_29 => rows.Sum(x => x.B01001e35 + x.B01001e11),
			AgeGroup.Age25_34 => rows.Sum(x => x.B01001e35 + x.B01001e36 + x.B01001e11 + x.B01001e12),
			AgeGroup.Age30_34 => rows.Sum(x => x.B01001e36 + x.B01001e12),
			AgeGroup.Age35_39 => rows.Sum(x => x.B01001e37 + x.B01001e13),
			AgeGroup.Age35_44 => rows.Sum(x => x.B01001e37 + x.B01001e38 + x.B01001e13 + x.B01001e14),
			AgeGroup.Age40_44 => rows.Sum(x => x.B01001e38 + x.B01001e14),
			AgeGroup.Age45_49 => rows.Sum(x => x.B01001e39 + x.B01001e15),
			AgeGroup.Age45_54 => rows.Sum(x => x.B01001e39 + x.B01001e40 + x.B01001e15 + x.B01001e16),
			AgeGroup.Age50_54 => rows.Sum(x => x.B01001e40 + x.B01001e16),
			AgeGroup.Age55_59 => rows.Sum(x => x.B01001e41 + x.B01001e17),
			AgeGroup.Age55_64 => rows.Sum(x => x.B01001e41 + x.B01001e42 + x.B01001e43 +
x.B01001e17 + x.B01001e18 + x.B01001e19),
			AgeGroup.Age60_61 => rows.Sum(x => x.B01001e42 + x.B01001e18),
			AgeGroup.Age62_64 => rows.Sum(x => x.B01001e43 + x.B01001e19),
			AgeGroup.Age65_66 => rows.Sum(x => x.B01001e44 + x.B01001e20),
			AgeGroup.Age65_74 => rows.Sum(x => x.B01001e44 + x.B01001e45 + x.B01001e46 +
x.B01001e20 + x.B01001e21 + x.B01001e22),
			AgeGroup.Age67_69 => rows.Sum(x => x.B01001e45 + x.B01001e21),
			AgeGroup.Age70_74 => rows.Sum(x => x.B01001e46 + x.B01001e22),
			AgeGroup.Age75_79 => rows.Sum(x => x.B01001e47 + x.B01001e23),
			AgeGroup.Age75_84 => rows.Sum(x => x.B01001e47 + x.B01001e48 + x.B01001e23 + x.B01001e24),
			AgeGroup.Age80_84 => rows.Sum(x => x.B01001e48 + x.B01001e24),
			AgeGroup.Age85up => rows.Sum(x => x.B01001e49 + x.B01001e25),
			AgeGroup.All => rows.Sum(x => x.B01001e2 + x.B01001e26),
			_ => throw new ArgumentOutOfRangeException("Unhandled age range"),
		};
	}
}



