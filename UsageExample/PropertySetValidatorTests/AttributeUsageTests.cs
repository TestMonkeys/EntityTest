using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Extensions;
using TestMonkey.Assertion.Extensions.Framework.Properties;
using UsageExample.PropertySetValidatorTests.TestObjects;
using Assert = TestMonkey.Assertion.Assert;

namespace UsageExample.PropertySetValidatorTests
{
    [TestClass]
    public class AttributeUsageTests
    {
        [TestMethod]
        public void PropertySet_ValidateWithProperty_IncorrectProperty()
        {
            var expected = new TestObjectCustomValidationImproperAttributeUsage {CustomValidation = "Validation"};
            var actual = new TestObjectCustomValidationImproperAttributeUsage {CustomValidation = "ValidationCustom"};
            var ex = Assert.Throws(typeof (ImproperAttributeUsageException),
                                   () => Assert.That(actual, PropertySet.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message,
                        Is.EqualTo(
                            "ValidateWithProperty for property CustomValidation was pointing at inexisting property ValidationCustomValidation1"),
                        "Assertion message");
        }

        [TestMethod]
        public void PropertySet_ChildList_ExpectedIsNotAList()
        {
            var expected = new TestObjectWithChildListImproperAttributeUsage
                {
                    Child = new TestObjectWithChildListImproperAttributeUsage()
                };
            var actual = new TestObjectWithChildListImproperAttributeUsage
                {
                    Child = new TestObjectWithChildListImproperAttributeUsage()
                };
            var ex = Assert.Throws(typeof (ImproperAttributeUsageException),
                                   () => Assert.That(actual, PropertySet.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message, Is.EqualTo("Expected property Child is not an instance of IList"),
                        "Assertion message");
        }
    }
}