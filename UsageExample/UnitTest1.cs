using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestMonkey.Assertion.Assert;

namespace UsageExample
{
    [TestClass]
    public class MsTestCompatibility
    {
        [TestMethod]
        public void BasicFailure()
        {
            Assert.IsTrue(false, "expecting a true value");
        }

        [TestMethod]
        public void MSTestAssertion()
        {
            Assert.IsTrue(false, "ms test message");
        }
    }
}