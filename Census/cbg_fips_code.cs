using System;
using System.Collections.Generic;

namespace Statistics.Census
{
    public partial class cbg_fips_code
    {
        public byte[] state { get; set; }
        public byte[] state_fips { get; set; }
        public byte[] county_fips { get; set; }
        public byte[] county { get; set; }
        public byte[] class_code { get; set; }
    }
}
