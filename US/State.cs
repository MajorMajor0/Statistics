using System;
using System.Collections.Generic;

namespace Statistics.US
{
    public partial class State
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string PostalAbbreviation { get; set; }
        public long? FIPS { get; set; }
    }
}
