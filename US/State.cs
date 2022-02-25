using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Statistics.US;

public static class UnitedStates
{
	public static List<State> All50States { get; } = new();
	public static State Alabama => All50States[0];
	public static State Alaska => All50States[1];
	public static State Arizona => All50States[2];
	public static State Arkansas => All50States[3];
	public static State California => All50States[4];
	public static State Colorado => All50States[5];
	public static State Connecticut => All50States[6];
	public static State Delaware => All50States[7];
	public static State Florida => All50States[8];
	public static State Georgia => All50States[9];
	public static State Hawaii => All50States[10];
	public static State Idaho => All50States[11];
	public static State Illinois => All50States[12];
	public static State Indiana => All50States[13];
	public static State Iowa => All50States[14];
	public static State Kansas => All50States[15];
	public static State Kentucky => All50States[16];
	public static State Louisiana => All50States[17];
	public static State Maine => All50States[18];
	public static State Maryland => All50States[19];
	public static State Massachusetts => All50States[20];
	public static State Michigan => All50States[21];
	public static State Minnesota => All50States[22];
	public static State Mississippi => All50States[23];
	public static State Missouri => All50States[24];
	public static State Montana => All50States[25];
	public static State Nebraska => All50States[26];
	public static State Nevada => All50States[27];
	public static State NewHampshire => All50States[28];
	public static State NewJersey => All50States[29];
	public static State NewMexico => All50States[30];
	public static State NewYork => All50States[31];
	public static State NorthCarolina => All50States[32];
	public static State NorthDakota => All50States[33];
	public static State Ohio => All50States[34];
	public static State Oklahoma => All50States[35];
	public static State Oregon => All50States[36];
	public static State Pennsylvania => All50States[37];
	public static State RhodeIsland => All50States[38];
	public static State SouthCarolina => All50States[39];
	public static State SouthDakota => All50States[40];
	public static State Tennessee => All50States[41];
	public static State Texas => All50States[42];
	public static State Utah => All50States[43];
	public static State Vermont => All50States[44];
	public static State Virginia => All50States[45];
	public static State Washington => All50States[46];
	public static State WestVirginia => All50States[47];
	public static State Wisconsin => All50States[48];
	public static State Wyoming => All50States[49];



	static UnitedStates()
	{
		var list = new List<State>
			{
				new State( 0, "Alabama", "AL", "AL", 1),
				new State( 1, "Alaska", "AK", "AK", 2),
				new State( 2, "Arizona", "AZ", "AZ", 4),
				new State( 3, "Arkansas", "AR", "AR", 5),
				new State( 4, "California", "CA", "CA", 6),
				new State( 5, "Colorado", "CO", "CO", 8),
				new State( 6, "Connecticut", "CT", "CT", 9),
				new State( 7, "Delaware", "DE", "DE", 10),
				new State( 8, "Florida", "FL", "FL", 12),
				new State( 9, "Georgia", "GA", "GA", 13),
				new State( 10, "Hawaii", "HI", "HI", 15),
				new State( 11, "Idaho", "ID", "ID", 16),
				new State( 12, "Illinois", "IL", "IL", 17),
				new State( 13, "Indiana", "IN", "IN", 18),
				new State( 14, "Iowa", "IA", "IA", 19),
				new State( 15, "Kansas", "KS", "KS", 20),
				new State( 16, "Kentucky", "KY", "KY", 21),
				new State( 17, "Louisiana", "LA", "LA", 22),
				new State( 18, "Maine", "ME", "ME", 23),
				new State( 19, "Maryland", "MD", "MD", 24),
				new State( 20, "Massachusetts", "MA", "MA", 25),
				new State( 21, "Michigan", "MI", "MI", 26),
				new State( 22, "Minnesota", "MN", "MN", 27),
				new State( 23, "Mississippi", "MS", "MS", 28),
				new State( 24, "Missouri", "MO", "MO", 29),
				new State( 25, "Montana", "MT", "MT", 30),
				new State( 26, "Nebraska", "NB", "NE", 31),
				new State( 27, "Nevada", "NV", "NV", 32),
				new State( 28, "New Hampshire", "NH", "NH", 33),
				new State( 29, "New Jersey", "NJ", "NJ", 34),
				new State( 30, "New Mexico", "NM", "NM", 35),
				new State( 31, "New York", "NY", "NY", 36),
				new State( 32, "North Carolina", "NC", "NC", 37),
				new State( 33, "North Dakota", "ND", "ND", 38),
				new State( 34, "Ohio", "OH", "OH", 39),
				new State( 35, "Oklahoma", "OK", "OK", 40),
				new State( 36, "Oregon", "OR", "OR", 41),
				new State( 37, "Pennsylvania", "PA", "PA", 42),
				new State( 38, "Rhode Island", "RI", "RI", 44),
				new State( 39, "South Carolina", "SC", "SC", 45),
				new State( 40, "South Dakota", "SD", "SD", 46),
				new State( 41, "Tennessee", "TN", "TN", 47),
				new State( 42, "Texas", "TX", "TX", 48),
				new State( 43, "Utah", "UT", "UT", 49),
				new State( 44, "Vermont", "VT", "VT", 50),
				new State( 45, "Virginia", "VA", "VA", 51),
				new State( 46, "Washington", "WA", "WA", 53),
				new State( 47, "West Virginia", "WV", "WV", 54),
				new State( 48, "Wisconsin", "WI", "WI", 55),
				new State( 49, "Wyoming", "WY", "WY", 56),
			};
		All50States.AddRange(list);
	}
}

public class State
{
	public int Id { get; init; }
	public string Name { get; init; }
	public string Abbreviation { get; init; }
	string PostalAbbreviation { get; init; }
	public int FIPS { get; init; }

	public State(int id, string name, string abbr, string postAbbr, int fips)
	{
		Id = id;
		Name = name;
		Abbreviation = abbr;
		PostalAbbreviation = postAbbr;
		FIPS = fips;

	}
}


