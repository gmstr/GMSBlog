using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMSBlog.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace GMSBlog.Web.Tests.Helpers
{
    /// <summary>
    /// Summary description for UrlHelperTests
    /// </summary>
    [TestClass]
    public class ContentLinkHelperTests
    {
        [TestMethod]
        public void ContentLinkHelper_Contains_Stylesheet_Extension_Method()
        {
            Assert.IsTrue(typeof(ContentLinkHelper).GetMethods().Any(x => x.Name == "Stylesheet"));
        }

        [TestMethod]
        public void StyleSheet_Returns_Valid_Output()
        {
            var stylesheet = "style.css";

            var output = ContentLinkHelper.Stylesheet(null, stylesheet);

            Assert.AreEqual(String.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", stylesheet), output);
        }

        [TestMethod]
        public void ContentLinkHelper_Contains_Javascript_Extension_Method()
        {
            Assert.IsTrue(typeof(ContentLinkHelper).GetMethods().Any(x => x.Name == "Javascript"));
        }

        [TestMethod]
        public void Javascript_Returns_Valid_Output()
        {
            var script = "jquery.js";

            var output = ContentLinkHelper.Javascript(null, script);

            Assert.AreEqual(String.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", script), output);
        }
    }
}
