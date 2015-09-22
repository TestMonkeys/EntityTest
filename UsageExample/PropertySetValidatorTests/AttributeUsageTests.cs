using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Framework;
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
                                   () => Assert.That(actual, Entity.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message,
                        Is.EqualTo(
                            "ValidateWithProperty for property CustomValidation was pointing at inexisting property ValidationCustomValidation1"),
                        "Assertion message");
        }
    }
}