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
using StructureMap;

namespace GMSBlog.Web.Support
{
    public static class Bootstrapper
    {
        public static void ConfigureStructureMap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.UseDefaultStructureMapConfigFile = false;
                x.AddRegistry(new GMSBlogRegistry());
            });
        }
    }
}
