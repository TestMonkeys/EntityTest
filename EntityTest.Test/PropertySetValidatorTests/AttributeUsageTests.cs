using System;
using EntityTest.Test.PropertySetValidatorTests.TestObjects;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests
{

    public class AttributeUsageTests
    {
        [Test]
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