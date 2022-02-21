using System;
using System.Diagnostics;
using System.Linq;

namespace Statistics.Census;

public partial class cbg_b01
{
	internal static long GetPopulation(US.State state, CDC.AgeGroup ageGroup, Context context)
	{
		switch (ageGroup)
		{

			case CDC.AgeGroup.Age5_14:
				return GetPopulation(state, AgeGroup.Age5_14, context);
			case CDC.AgeGroup.Age15_24:
				return GetPopulation(state, AgeGroup.Age15_24, context);
			case CDC.AgeGroup.Age25_34:
				return GetPopulation(state, AgeGroup.Age25_34, context);
			case CDC.AgeGroup.Age35_44:
				return GetPopulation(state, AgeGroup.Age35_44, context);
			case CDC.AgeGroup.Age45_54:
				return GetPopulation(state, AgeGroup.Age45_54, context);
			case CDC.AgeGroup.Age55_64:
				return GetPopulation(state, AgeGroup.Age55_64, context);
			case CDC.AgeGroup.Age65_74:
				return GetPopulation(state, AgeGroup.Age65_74, context);
			case CDC.AgeGroup.Age75_84:
				return GetPopulation(state, AgeGroup.Age75_84, context);
			case CDC.AgeGroup.Age85up:
				return GetPopulation(state, AgeGroup.Age85up, context);
			case CDC.AgeGroup.All:
				return GetPopulation(state, AgeGroup.Age5_14, context);
			default:
				throw new ArgumentOutOfRangeException("Unhandled age range.");
		}
	}
	internal static long GetPopulation(US.State state, AgeGroup ageGroup, Context context)
	{
		string[] ageRanges =
		{
			"Under 5", // B01001e27 + B01001e3, F + M
			"5 - 9",   // B01001e28 + B01001e4, F + M
			"10 - 14", // B01001e29 + B01001e5, F + M
			"15 - 17", // B01001e30 + B01001e6, F + M
			"18 - 19", // B01001e31 + B01001e7, F + M
			"20",	   // B01001e32 + B01001e8, F + M
			"21",	   // B01001e33 + B01001e9, F + M
			"22 - 24", // B01001e34 + B01001e10, F + M
			"25 - 29", // B01001e35 + B01001e11, F + M
			"30 - 34", // B01001e36 + B01001e12, F + M
			"35 - 39", // B01001e37 + B01001e13, F + M
			"40 - 44", // B01001e38 + B01001e14, F + M
			"45 - 49", // B01001e39 + B01001e15, F + M
			"50 - 54", // B01001e40 + B01001e16, F + M
			"55 - 59", // B01001e41 + B01001e17, F + M
			"60 - 61", // B01001e42 + B01001e18, F + M
			"62 - 64", // B01001e43 + B01001e19, F + M
			"65 - 66", // B01001e44 + B01001e20, F + M
			"67 - 69", // B01001e45 + B01001e21, F + M
			"70 - 74", // B01001e46 + B01001e22, F + M
			"75 - 79", // B01001e47 + B01001e23, F + M
			"80 - 84", // B01001e48 + B01001e24, F + M
			"85 +",	   // B01001e49 + B01001e25, F + M
			"All",	   // B01001e26 + B01001e2, F + M
		};

		Stopwatch watch = Stopwatch.StartNew();
		var rows = context
			.cbg_b01
			.Where(x => x.State == state.FIPS);

		switch (ageGroup)
		{
			case AgeGroup.Age0_5:
				return rows.Sum(x => x.B01001e27 + x.B01001e3);
			case AgeGroup.Age5_9:
				return rows.Sum(x => x.B01001e28 + x.B01001e4);
			case AgeGroup.Age5_14:
				return rows.Sum(x => x.B01001e28 + x.B01001e29 + x.B01001e4 + x.B01001e5);
			case AgeGroup.Age10_14:
				return rows.Sum(x => x.B01001e29 + x.B01001e5);
			case AgeGroup.Age15_17:
				return rows.Sum(x => x.B01001e30 + x.B01001e6);
			case AgeGroup.Age15_24:
				return rows.Sum(x => x.B01001e30 + x.B01001e31 + x.B01001e32 + x.B01001e33 + x.B01001e34 +
				x.B01001e6 + x.B01001e7 + x.B01001e8 + x.B01001e9 + x.B01001e10);
			case AgeGroup.Age18_19:
				return rows.Sum(x => x.B01001e31 + x.B01001e7);
			case AgeGroup.Age20:
				return rows.Sum(x => x.B01001e32 + x.B01001e8);
			case AgeGroup.Age21:
				return rows.Sum(x => x.B01001e33 + x.B01001e9);
			case AgeGroup.Age22_24:
				return rows.Sum(x => x.B01001e34 + x.B01001e10);
			case AgeGroup.Age25_29:
				return rows.Sum(x => x.B01001e35 + x.B01001e11);
			case AgeGroup.Age25_34:
				return rows.Sum(x => x.B01001e35 + x.B01001e36 + x.B01001e11 + x.B01001e12);
			case AgeGroup.Age30_34:
				return rows.Sum(x => x.B01001e36 + x.B01001e12);
			case AgeGroup.Age35_39:
				return rows.Sum(x => x.B01001e37 + x.B01001e13);
			case AgeGroup.Age35_44:
				return rows.Sum(x => x.B01001e37 + x.B01001e38 + x.B01001e13 + x.B01001e14);
			case AgeGroup.Age40_44:
				return rows.Sum(x => x.B01001e38 + x.B01001e14);
			case AgeGroup.Age45_49:
				return rows.Sum(x => x.B01001e39 + x.B01001e15);
			case AgeGroup.Age45_54:
				return rows.Sum(x => x.B01001e39 + x.B01001e40 + x.B01001e15 + x.B01001e16);
			case AgeGroup.Age50_54:
				return rows.Sum(x => x.B01001e40 + x.B01001e16);
			case AgeGroup.Age55_59:
				return rows.Sum(x => x.B01001e41 + x.B01001e17);
			case AgeGroup.Age55_64:
				return rows.Sum(x => x.B01001e41 + x.B01001e42 + x.B01001e43 +
				x.B01001e17 + x.B01001e18 + x.B01001e19);
			case AgeGroup.Age60_61:
				return rows.Sum(x => x.B01001e42 + x.B01001e18);
			case AgeGroup.Age62_64:
				return rows.Sum(x => x.B01001e43 + x.B01001e19);
			case AgeGroup.Age65_66:
				return rows.Sum(x => x.B01001e44 + x.B01001e20);
			case AgeGroup.Age65_74:
				return rows.Sum(x => x.B01001e44 + x.B01001e45 + x.B01001e46 +
				x.B01001e20 + x.B01001e21 + x.B01001e22);
			case AgeGroup.Age67_69:
				return rows.Sum(x => x.B01001e45 + x.B01001e21);
			case AgeGroup.Age70_74:
				return rows.Sum(x => x.B01001e46 + x.B01001e22);
			case AgeGroup.Age75_79:
				return rows.Sum(x => x.B01001e47 + x.B01001e23);
			case AgeGroup.Age75_84:
				return rows.Sum(x => x.B01001e47 + x.B01001e48 + x.B01001e23 + x.B01001e24);
			case AgeGroup.Age80_84:
				return rows.Sum(x => x.B01001e48 + x.B01001e24);
			case AgeGroup.Age85up:
				return rows.Sum(x => x.B01001e49 + x.B01001e25);
			case AgeGroup.Any:
				return rows.Sum(x => x.B01001e2 + x.B01001e26);
			default:
				throw new ArgumentOutOfRangeException("Unhandled age range");
		}
	}
}



