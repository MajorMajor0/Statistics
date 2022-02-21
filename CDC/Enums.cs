using System.ComponentModel;

namespace Statistics.CDC;

internal enum AgeGroup
{
	[Description("0-17 years")]
	Age0_17,
	[Description("Under 1 year")]  
	Age0_1,
	[Description("1-4 years")]
	Age1_4,
	[Description("5-14 years")]
	Age5_14,
	[Description("15-24 years")] 
	Age15_24,
	[Description("18-29 years")] 
	Age18_29,
	[Description("25-34 years")] 
	Age25_34,
	[Description("30-39 years")] 
	Age30_39,
	[Description("35-44 years")] 
	Age35_44,
	[Description("40-49 years")] 
	Age40_49,
	[Description("45-54 years")] 
	Age45_54,
	[Description("50-64 years")] 
	Age50_64,
	[Description("55-64 years")] 
	Age55_64,
	[Description("65-74 years")] 
	Age65_74,
	[Description("75-84 years")] 
	Age75_84,
	[Description("85 years and over")]
	Age85up,
	[Description("All Ages")]
	All

}
