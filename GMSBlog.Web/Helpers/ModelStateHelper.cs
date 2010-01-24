using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using GMSBlog.Model.Validation;
using System.Collections.Generic;

namespace GMSBlog.Web.Helpers
{
    public static class ModelStateHelper
    {
        public static void AddRuleViolations(this ModelStateDictionary modelState, IEnumerable<RuleViolation> violations)
        {
            foreach (var violation in violations)
            {
                modelState.AddModelError(violation.Property, violation.Violation);
            }
        }
    }
}
