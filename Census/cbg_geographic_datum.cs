using System;
using System.Collections.Generic;

namespace Statistics.Census
{
    public partial class cbg_geographic_datum
    {
        public byte[] census_block_group { get; set; }
        public byte[] amount_land { get; set; }
        public byte[] amount_water { get; set; }
        public byte[] latitude { get; set; }
        public byte[] longitude { get; set; }
    }
}
