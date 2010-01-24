using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMSBlog.Model.Validation
{
    public class RuleViolation
    {
        public RuleViolation() { }

        public RuleViolation(string property, string violation)
        {
            Property = property;
            Violation = violation;
        }

        public string Property { get; set; }
        public string Violation { get; set; }       
    }
}
