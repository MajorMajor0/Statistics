using System;
using System.Collections.Generic;

namespace Statistics.CDC;

internal static class Extensions
{
	public static Corona ToCorona(this Dictionary<string, string> dict)
	{
		var dc = new Corona();

		if (dict.TryGetValue("data_as_of", out string str0) &&
			DateTime.TryParse(str0, out DateTime dataAsOf))
		{
			dc.DataAsOf = dataAsOf;
		}
		if (dict.TryGetValue("start_date", out string str1) &&
			DateTime.TryParse(str1, out DateTime startDate))
		{
			dc.StartDate = startDate;
		}
		if (dict.TryGetValue("end_date", out string str2) &&
			DateTime.TryParse(str2, out DateTime endDate))
		{
			dc.EndDate = endDate;
		}
		if (dict.TryGetValue("group", out string group))
		{
			dc.Group = group;
		}
		if (dict.TryGetValue("state", out string state))
		{
			dc.State = state;
		}
		if (dict.TryGetValue("sex", out string sex))
		{
			dc.Sex = sex;
				
		}
		if (dict.TryGetValue("age_group", out string ageGroup))
		{
			dc.AgeGroup = ageGroup;
		}
		if (dict.TryGetValue("covid_19_deaths", out string str3) &&
			int.TryParse(str3, out int covidDeaths))
		{
			dc.CovidDeaths = covidDeaths;
		}
		if (dict.TryGetValue("pneumonia_deaths", out string str4) &&
			int.TryParse(str4, out int pneumoniaDeaths))
		{
			dc.PneumoniaDeaths = pneumoniaDeaths;
		}
		if (dict.TryGetValue("influenza_deaths", out string str5) &&
			int.TryParse(str5, out int fluDeaths))
		{
			dc.FluDeaths = fluDeaths;
		}
		if (dict.TryGetValue("pneumonia_and_covid_19_deaths", out string str6) &&
			int.TryParse(str6, out int pneumonia_and_covid_19_deaths))
		{
			dc.PneumoniaAndCovidDeaths = pneumonia_and_covid_19_deaths;
		}

		return dc;
	}
}
