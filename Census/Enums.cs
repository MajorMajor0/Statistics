using System.ComponentModel;

namespace Statistics.Census
{
	internal enum AgeGroup
	{
		[Description("Under 5")] 
		Age0_5,
		[Description("5 - 9")]   
		Age5_9,
		[Description("10 - 14")] 
		Age10_14,
		[Description("15 - 17")] 
		Age15_17,
		[Description("18 - 19")] 
		Age18_19,
		[Description("20")]  
		Age20,
		[Description("21")]  
		Age21,
		[Description("22 - 24")] 
		Age22_24,
		[Description("25 - 29")] 
		Age25_29,
		[Description("30 - 34")] 
		Age30_34,
		[Description("35 - 39")] 
		Age35_39,
		[Description("40 - 44")] 
		Age40_44,
		[Description("45 - 49")] 
		Age45_49,
		[Description("50 - 54")] 
		Age50_54,
		[Description("55 - 59")] 
		Age55_59,
		[Description("60 - 61")] 
		Age60_61,
		[Description("62 - 64")] 
		Age62_64,
		[Description("65 - 66")] 
		Age65_66,
		[Description("67 - 69")] 
		Age67_69,
		[Description("70 - 74")] 
		Age70_74,
		[Description("75 - 79")] 
		Age75_79,
		[Description("80 - 84")] 
		Age80_84,
		[Description("85 +")]
		Age85up,
		[Description("All")]     
		Any,

		[Description("5 - 14")]
		Age5_14,
		[Description("15 - 24")]
		Age15_24,
		[Description("25 - 34")]
		Age25_34,
		[Description("35 - 44")]
		Age35_44,
		[Description("45 - 54")]
		Age45_54,
		[Description("55 - 64")]
		Age55_64,
		[Description("65 - 74")]
		Age65_74,
		[Description("75 - 84")]
		Age75_84
	}
}
