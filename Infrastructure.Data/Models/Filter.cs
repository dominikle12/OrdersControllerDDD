using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class Filter
    {
        public string Attribute { get; set; } = "";
        public MatchMode MatchMode { get; set; }
        public string Operator { get; set; } = "";
        public string? Value { get; set; }
    }

    public enum MatchMode
    {
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
        Equals,
        NotEquals,
        DateIs,
        DateIsNot,
        DateBefore,
        DateAfter,
        GT,
        LT,
        Empty,
        EqualsOne // Split values using a |
    }
}
