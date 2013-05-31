using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameterized_Query
{
    public class SearchParameter
    {
        public string FieldName { get; set; }
        public ComparisonType SearchType { get; set; }
        public object SearchValue { get; set; }
        public object SearchValue2 { get; set; }
    }

    public enum ComparisonType
    {
        Equals,
        LessThan,
        MoreThan,
        Contains,
        Between,
        StartsWith
    }
}
