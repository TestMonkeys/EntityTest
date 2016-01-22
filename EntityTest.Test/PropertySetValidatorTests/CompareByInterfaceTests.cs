using System;
using System.Collections.Generic;
using EntityTest.Test.PropertySetValidatorTests.TestObjects;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Framework;
using Assert = NUnit.Framework.Assert;


namespace EntityTest.Test.PropertySetValidatorTests
{

    public class CompareByInterfaceTests
    {
        [Test]
        public void PropertySet_ByInterface_IncompatibleInterface()
        {
            var expected = new TestObjectCustomValidation {CustomValidation = "Validation"};
            var actual = new TestObjectCustomValidation {CustomValidation = "Validation"};
            var ex = Assert.Throws(typeof (ImproperTypeUsageException),
                                   () =>
                                   Assert.That(actual,
                                               Entity.Is.EqualTo(expected).ByInterface(typeof(TestObjectWithChildList))));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message,
                        Is.EqualTo(
                            "Could not get property Child from object of type EntityTest.Test.PropertySetValidatorTests.TestObjects.TestObjectCustomValidation"),
                        "Assertion message");
        }

        [Test]
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
            Assert.That(object2, Entity.Is.EqualTo(object1).ByInterface(typeof (ITestObject)));
        }

        [Test]
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
            var ex = Assert.Throws(typeof (AssertionException),
                                   () =>
                                   Assert.That(object2, Entity.Is.EqualTo(object1).ByInterface(typeof (ITestObject))));

            messageCheck.Check(ex);
        }
    }
}