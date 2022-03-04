using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.FBI;

[Index(nameof(Code), IsUnique = true)]
public class Agency
{
	public string Code { get; set; }
	//public US.City City { get; set; }
	//public US.State State { get; set; }

	public string PopulationGroup { get; set; }
}
