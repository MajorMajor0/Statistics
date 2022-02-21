using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Statistics.US;

public static class UnitedStates
{
	public static List<State> States { get; } = new();
	public static State Alabama => States[0];
	public static State Alaska => States[1];
	public static State Arizona => States[2];
	public static State Arkansas => States[3];
	public static State California => States[4];
	public static State Colorado => States[5];
	public static State Connecticut => States[6];
	public static State Delaware => States[7];
	public static State Florida => States[8];
	public static State Georgia => States[9];
	public static State Hawaii => States[10];
	public static State Idaho => States[11];
	public static State Illinois => States[12];
	public static State Indiana => States[13];
	public static State Iowa => States[14];
	public static State Kansas => States[15];
	public static State Kentucky => States[16];
	public static State Louisiana => States[17];
	public static State Maine => States[18];
	public static State Maryland => States[19];
	public static State Massachusetts => States[20];
	public static State Michigan => States[21];
	public static State Minnesota => States[22];
	public static State Mississippi => States[23];
	public static State Missouri => States[24];
	public static State Montana => States[25];
	public static State Nebraska => States[26];
	public static State Nevada => States[27];
	public static State NewHampshire => States[28];
	public static State NewJersey => States[29];
	public static State NewMexico => States[30];
	public static State NewYork => States[31];
	public static State NorthCarolina => States[32];
	public static State NorthDakota => States[33];
	public static State Ohio => States[34];
	public static State Oklahoma => States[35];
	public static State Oregon => States[36];
	public static State Pennsylvania => States[37];
	public static State RhodeIsland => States[38];
	public static State SouthCarolina => States[39];
	public static State SouthDakota => States[40];
	public static State Tennessee => States[41];
	public static State Texas => States[42];
	public static State Utah => States[43];
	public static State Vermont => States[44];
	public static State Virginia => States[45];
	public static State Washington => States[46];
	public static State WestVirginia => States[47];
	public static State Wisconsin => States[48];
	public static State Wyoming => States[49];

	static UnitedStates()
	{

		var list = new List<(int, string, string, int)>
			{
				(1,"Alabama","AL", 1),
				(2,"Alaska","AK", 2),
				(3,"Arizona","AZ", 4),
				(4,"Arkansas","AR", 5),
				(5,"California","CA", 6),
				(6,"Colorado","CO", 8),
				(7,"Connecticut","CT", 9),
				(8,"Delaware","DE", 10),
				(9,"Florida","FL", 12),
				(10,"Georgia","GA", 13),
				(11,"Hawaii","HI", 15),
				(12,"Idaho","ID", 16),
				(13,"Illinois","IL", 17),
				(14,"Indiana","IN", 18),
				(15,"Iowa","IA", 19),
				(16,"Kansas","KS", 20),
				(17,"Kentucky","KY", 21),
				(18,"Louisiana","LA", 22),
				(19,"Maine","ME",23),
				(20,"Maryland","MD", 24),
				(21,"Massachusetts","MA", 25),
				(22,"Michigan","MI" ,26),
				(23,"Minnesota","MN", 27),
				(24,"Mississippi","MS" ,28),
				(25,"Missouri","MO", 29),
				(26,"Montana","MT", 30),
				(27,"Nebraska","NB", 31),
				(28,"Nevada","NV", 32),
				(29,"New Hampshire","NH", 33),
				(30,"New Jersey","NJ", 34),
				(31,"New Mexico","NM", 35),
				(32,"New York","NY", 36),
				(33,"North Carolina","NC", 37),
				(34,"North Dakota","ND", 38),
				(35,"Ohio","OH", 39),
				(36,"Oklahoma","OK", 40),
				(37,"Oregon","OR", 41),
				(38,"Pennsylvania","PA", 42),
				(39,"Rhode Island","RI", 44),
				(40,"South Carolina","SC",45),
				(41,"South Dakota","SD",46),
				(42,"Tennessee","TN", 47),
				(43,"Texas","TX", 48),
				(44,"Utah","UT", 49),
				(45,"Vermont","VT", 50),
				(46,"Virginia","VA", 51),
				(47,"Washington","WA", 53),
				(48,"West Virginia","WV", 54),
				(49,"Wisconsin","WI", 55),
				(50,"Wyoming","WY", 56),
			};

		IEnumerable<State> states = list
			.Select(x => new State
			{
				Id = x.Item1,
				Name = x.Item2,
				Abbreviation = x.Item3,
				FIPS = x.Item4,
			});

		States.AddRange(states);
	}
}

public class State
{
	public int Id { get; init; }
	public string Name { get; init; }
	public string Abbreviation { get; init; }
	public int FIPS { get; init; }
}


