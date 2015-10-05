using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestMonkey.Assertion.Assert;

namespace UsageExample
{
    [TestClass]
    public class MsTestCompatibility
    {
        [Ignore]
        [TestMethod]
        public void BasicFailure()
        {
            Assert.IsTrue(false, "expecting a true value");
        }

        [Ignore]
        [TestMethod]
        public void MSTestAssertion()
        {
            Assert.IsTrue(false, "ms test message");
        }
    }
}