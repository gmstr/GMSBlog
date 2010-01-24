using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GMSBlog.Model.Validation;

namespace GMSBlog.Model.Tests
{
    /// <summary>
    /// Summary description for RuleViolationTests
    /// </summary>
    [TestClass]
    public class RuleViolationTests
    {
        [TestMethod]
        public void RuleViolation_Class_Exists()
        {
            var violation = new RuleViolation();

            Assert.IsNotNull(violation);
        }

        [TestMethod]
        public void RuleViolation_Contains_Property_String()
        {
            var violation = new RuleViolation();

            violation.Property = "Test";

            Assert.AreEqual("Test", violation.Property);
        }

        [TestMethod]
        public void RuleViolation_Contains_Violation_String()
        {
            var violation = new RuleViolation();

            violation.Violation = "Test";

            Assert.AreEqual("Test", violation.Violation);
        }

        [TestMethod]
        public void RuleViolation_Constructor_Sets_Values()
        {
            var violation = new RuleViolation("prop", "viol");

            Assert.AreEqual("prop", violation.Property);

            Assert.AreEqual("viol", violation.Violation);

        }
    }
}
