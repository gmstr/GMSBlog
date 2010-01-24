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
using StructureMap.Configuration.DSL;
using GMSBlog.Service;
using GMSBlog.Service.NHibernate;
using StructureMap.Attributes;

namespace GMSBlog.Web.Support
{
    public class GMSBlogRegistry : Registry
    {
        public GMSBlogRegistry()
        {
            ForRequestedType<IBlogService>()
                .TheDefault.Is.OfConcreteType<NHibernateBlogService>().WithCtorArg("autoCommit").EqualTo(true);
        }
    }
}