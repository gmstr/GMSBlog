using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMSBlog.Model.Validation
{
    public interface IValidated
    {
        bool IsValid { get; }
        IEnumerable<RuleViolation> RuleViolations { get; }
    }
}
