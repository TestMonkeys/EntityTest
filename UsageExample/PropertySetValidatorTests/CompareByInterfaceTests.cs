using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Exceptions;
using TestMonkey.Assertion.Extensions;
using TestMonkey.Assertion.Extensions.Framework.Properties;
using UsageExample.PropertySetValidatorTests.TestObjects;
using Assert = TestMonkey.Assertion.Assert;

namespace UsageExample.PropertySetValidatorTests
{
    [TestClass]
    public class CompareByInterfaceTests
    {
        [TestMethod]
        public void PropertySet_ByInterface_IncompatibleInterface()
        {
            var expected = new TestObjectCustomValidation {CustomValidation = "Validation"};
            var actual = new TestObjectCustomValidation {CustomValidation = "Validation"};
            var ex = Assert.Throws(typeof (ImproperTypeUsageException),
                                   () =>
                                   Assert.That(actual,
                                               PropertySet.EqualToByInterface(expected, typeof (TestObjectWithChildList))));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message,
                        Is.EqualTo(
                            "Could not get property Child from object of type UsageExample.PropertySetValidatorTests.TestObjects.TestObjectCustomValidation"),
                        "Assertion message");
        }

        [TestMethod]
        public void PropertySet_ByInterface()
        {
            var object1 = new TestObjectByInterface1
                {
                    ChildList =
                        new List<TestObjectWithChildSet>
                            {
                                new TestObjectWithChildSet
                                    {
                                        Child = new TestObjectWithChildSet {ValidationProperty = "value"}
                                    }
                            },
                    CustomValidation = "ValidationProperty",
                    Object1Value = "Object1Value",
                    Value1 = "Value1"
                };

            var object2 = new TestObjectByInterface2
                {
                    ChildList =
                        new List<TestObjectWithChildSet>
                            {
                                new TestObjectWithChildSet
                                    {
                                        Child = new TestObjectWithChildSet {ValidationProperty = "value"}
                                    }
                            },
                    CustomValidation = "ValidationPropertyCustom",
                    Object1Value = "Object2Value",
                    Value1 = "Value1",
                    Object2Value = "Value2"
                };
            Assert.That(object2, PropertySet.EqualToByInterface(object1, typeof (ITestObject)));
        }

        [TestMethod]
        public void PropertySet_ByInterface_Failure()
        {
            var object1 = new TestObjectByInterface1
                {
                    ChildList =
                        new List<TestObjectWithChildSet>
                            {
                                new TestObjectWithChildSet
                                    {
                                        Child = new TestObjectWithChildSet {ValidationProperty = "valueExp"}
                                    }
                            },
                    CustomValidation = "ValidationPropertyC",
                    Object1Value = "Object1Value",
                    Value1 = "Value1"
                };

            var object2 = new TestObjectByInterface2
                {
                    ChildList =
                        new List<TestObjectWithChildSet>
                            {
                                new TestObjectWithChildSet
                                    {
                                        Child = new TestObjectWithChildSet {ValidationProperty = "value"}
                                    }
                            },
                    CustomValidation = "ValidationProperty",
                    Object1Value = "Object2Value",
                    Value1 = "Value1",
                    Object2Value = "Value2"
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("valueExp", "value", "ChildList[0].Child.ValidationProperty");
            messageCheck.AddPropertyLine("ValidationPropertyCCustom", "ValidationProperty", "CustomValidation");
            var ex = Assert.Throws(typeof (AssertionFailedException),
                                   () =>
                                   Assert.That(object2, PropertySet.EqualToByInterface(object1, typeof (ITestObject))));

            messageCheck.Check(ex);
        }
    }
}