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

        [TestMethod]
        public void ContentLinkHelper_Contains_MetaDescription_Extension_Method()
        {
            Assert.IsTrue(typeof(ContentLinkHelper).GetMethods().Any(x => x.Name == "MetaDescription"));
        }

        [TestMethod]
        public void MetaDescription_Returns_Valid_Output()
        {
            var description = "Test Description";

            var output = ContentLinkHelper.MetaDescription(null, description);

            Assert.AreEqual("<meta name=\"description\" content=\"Test Description\" />", output);
        }

        [TestMethod]
        public void ContentLinkHelper_Contains_AddDashesToTitle_Extension_Method()
        {
            Assert.IsTrue(typeof(ContentLinkHelper).GetMethods().Any(x => x.Name == "AddDashesToTitle"));
        }

        [TestMethod]
        public void AddDashesToTitle_Returns_Valid_Output()
        {
            var title = "Test Title";

            var output = ContentLinkHelper.AddDashesToTitle(null, title);

            Assert.AreEqual("Test-Title", output);
        }

        [TestMethod]
        public void AddDashesToTitle_Makes_One_Dash_Two_For_Hyphenated_Words()
        {
            var title = "Test Dash-Containing Title";

            var output = ContentLinkHelper.AddDashesToTitle(null, title);

            Assert.AreEqual("Test-Dash--Containing-Title", output);
        }

        [TestMethod]
        public void ContentLinkHelper_Contains_RemoveDashesFromTitle_Extension_Method()
        {
            Assert.IsTrue(typeof(ContentLinkHelper).GetMethods().Any(x => x.Name == "RemoveDashesFromTitle"));
        }

        [TestMethod]
        public void RemoveDashesFromTitle_Returns_Valid_Output_For_Simple_Case()
        {
            var title = "Test-Title";

            var output = ContentLinkHelper.RemoveDashesFromTitle(null, title);

            Assert.AreEqual("Test Title", output);
        }

        [TestMethod]
        public void RemoveDashesFromTitle_Returns_Valid_Output_For_Complex_Case()
        {
            var title = "Test-Title---A-Complex-Title";

            var output = ContentLinkHelper.RemoveDashesFromTitle(null, title);

            Assert.AreEqual("Test Title - A Complex Title", output);
        }

        [TestMethod]
        public void RemoveDashesFromTitle_Returns_Valid_Output_For_Hyphenated_Case()
        {
            var title = "Test-Dash--Containing-Title";

            var output = ContentLinkHelper.RemoveDashesFromTitle(null, title);

            Assert.AreEqual("Test Dash-Containing Title", output);
        }

        [TestMethod]
        public void ContentLinkHelper_Contains_MetaKeywords_Extension_Method()
        {
            Assert.IsTrue(typeof(ContentLinkHelper).GetMethods().Any(x => x.Name == "MetaKeywords"));
        }

        [TestMethod]
        public void MetaKeywords_Returns_Valid_Output()
        {
            var description = "test,keywords,thing";

            var output = ContentLinkHelper.MetaKeywords(null, description);

            Assert.AreEqual("<meta name=\"keywords\" content=\"test,keywords,thing\" />", output);
        }
    }
}
