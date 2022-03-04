using System;
using System.Collections.Generic;

namespace Statistics.US
{
    public partial class Term
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long District { get; set; }
        public string State { get; set; }
        public string Party { get; set; }
        public long? Congressman_ID { get; set; }

        public virtual Congressman Congressman { get; set; }
    }
}
