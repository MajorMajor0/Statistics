using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Statistics.US;

internal static class Extensions
{
	public static Congressman ToCongressman(this Dictionary<string, object> dict)
	{
		var congressman = new Congressman();

		var names = dict["name"] as Dictionary<object, object>;
		congressman.FirstName = names["first"] as string;
		congressman.LastName = names["last"] as string;

		var bio = dict["bio"] as Dictionary<object, object>;
		congressman.Sex = bio["gender"] as string;

		CultureInfo enUS = new("en-US");


		if (bio.TryGetValue("birthday", out var birthday) &&
			DateTime.TryParseExact(birthday as string, "yyyy-MM-dd",
			enUS, DateTimeStyles.None, out DateTime birthDate))
		{
			congressman.Birthdate = birthDate;
		}
		else
		{

		}

		var termObjs = dict["terms"] as List<object>;

		foreach (var termDict in termObjs.Select(x => x as Dictionary<object, object>))
		{
			Term term = new();
			term.StartDate = DateTime.Parse(termDict["start"] as string);
			term.EndDate = DateTime.Parse(termDict["end"] as string);

			if (termDict.TryGetValue("party", out object partyObj))
			{
				term.Party = partyObj as string;
			}

			term.Type = termDict["type"] as string;
			term.State = termDict["state"] as string;

			if (term.Type == "rep")
				term.District = long.Parse(termDict["district"] as string);

			congressman.Terms.Add(term);
		}

		return congressman;
	}
}
