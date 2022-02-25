using System;
using System.Collections.Generic;

namespace Statistics.US
{
    public partial class ocid
    {
        public byte[] id { get; set; }
        public byte[] name { get; set; }
        public byte[] sameAs { get; set; }
        public byte[] sameAsNote { get; set; }
        public byte[] validThrough { get; set; }
        public byte[] census_geoid { get; set; }
        public byte[] census_geoid_12 { get; set; }
        public byte[] census_geoid_14 { get; set; }
        public byte[] openstates_district { get; set; }
        public byte[] placeholder_id { get; set; }
        public byte[] sch_dist_stateid { get; set; }
        public byte[] state_id { get; set; }
        public byte[] validFrom { get; set; }
    }
}
