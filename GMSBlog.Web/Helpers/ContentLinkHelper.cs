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

namespace GMSBlog.Web.Helpers
{
    public static class ContentLinkHelper
    {
        public static string Stylesheet(this HtmlHelper helper, string stylesheet)
        {
            return String.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", stylesheet);
        }

        public static string Javascript(this HtmlHelper helper, string script)
        {
            return String.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", script);
        }
    }
}
