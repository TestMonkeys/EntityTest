using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Exceptions;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.PropertyAttributes;
using UsageExample.PropertySetValidatorTests.TestObjects;
using Assert = TestMonkey.Assertion.Assert;

namespace UsageExample.PropertySetValidatorTests
{
    [TestClass]
    public class ActualOnlyValidationTests
    {
        [TestMethod]
        public void PropertySet_ActualAttributes_NotNull()
        {
            var expected = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 0
                };
            var actual = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 5,
                    IdNotNull = new object()
                };
            Assert.That(actual, PropertySet.EqualTo(expected));
        }

        [TestMethod]
        public void PropertySet_ActualAttributes_NotNull_Failure()
        {
            var expected = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 5
                };
            var actual = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 5
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("Not Null", "Null", "IdNotNull");
            var ex = Assert.Throws(typeof (AssertionFailedException),
                                   () => Assert.That(actual, PropertySet.EqualTo(expected)));

            messageCheck.Check(ex);
        }

        [TestMethod]
        public void PropertySet_ActualAttributes_GreaterThan()
        {
            var expected = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 5
                };
            var actual = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 5,
                    IdNotNull = new object()
                };
            Assert.That(actual, PropertySet.EqualTo(expected));
        }

        [TestMethod]
        public void PropertySet_ActualAttributes_GreaterThan_Failure()
        {
            var expected = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = -1
                };
            var actual = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = -1,
                    IdNotNull = new object()
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("Greater than 0", "-1", "IdGreaterThanZero");
            var ex = Assert.Throws(typeof (AssertionFailedException),
                                   () => Assert.That(actual, PropertySet.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [TestMethod]
        public void PropertySet_ActualAttributes_GreaterThan_Edge_Failure()
        {
            var expected = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 0
                };
            var actual = new TestObjectActualValidationAttributes
                {
                    IdGreaterThanZero = 0,
                    IdNotNull = new object()
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("Greater than 0", "0", "IdGreaterThanZero");
            var ex = Assert.Throws(typeof (AssertionFailedException),
                                   () => Assert.That(actual, PropertySet.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [TestMethod]
        public void PropertySet_ActualAttributes_GreaterThan_ImproperUsage()
        {
            var expected = new TestObjectImproperGreaterThan
                {
                    GreaterThanValue = new object()
                };
            var actual = new TestObjectImproperGreaterThan
                {
                    GreaterThanValue = new object()
                };
            var ex = Assert.Throws(typeof (ImproperAttributeUsageException),
                                   () => Assert.That(actual, PropertySet.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message,
                        Is.EqualTo("ValidateActualGreaterThanAttribute should be defined only on numeric properties"),
                        "Assertion message");
        }

        [TestMethod]
        public void PropertySet_ActualAttributes_GreaterThan_ImproperUsage_Null()
        {
            var expected = new TestObjectImproperGreaterThan
                {
                    GreaterThanValue = null
                };
            var actual = new TestObjectImproperGreaterThan
                {
                    GreaterThanValue = null
                };
            var ex = Assert.Throws(typeof (ImproperAttributeUsageException),
                                   () => Assert.That(actual, PropertySet.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message,
                        Is.EqualTo("ValidateActualGreaterThanAttribute should be defined only on numeric properties"),
                        "Assertion message");
        }
    }
}