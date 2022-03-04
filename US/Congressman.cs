using System;
using System.Collections.Generic;

namespace Statistics.US
{
    public partial class Congressman
    {
        public Congressman()
        {
            Terms = new HashSet<Term>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Term> Terms { get; set; }
    }
}
