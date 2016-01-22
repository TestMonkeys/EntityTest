using System;
using System.Collections.Generic;
using EntityTest.Test.PropertySetValidatorTests.TestObjects;

using NUnit.Framework;
using TestMonkeys.EntityTest;



namespace EntityTest.Test.PropertySetValidatorTests
{

    public class ComparisonTests
    {
        [Test]
        public void PropertySet_ExpectingNullObject()
        {
            TestObjectCustomValidation expected = null;
            var ex = Assert.Throws(typeof (ArgumentNullException), () => Entity.Is.EqualTo(expected));
            Console.WriteLine(ex.Message);
            Assert.That(ex.Message, Is.EqualTo("Expected can't be null\r\nParameter name: expected"),
                        "Assertion message");
        }

        [Test]
        public void PropertySet_ExpectingNotNullObject_ActualNull()
        {
            var expected = new TestObjectCustomValidation {CustomValidation = "Validation"};
            TestObjectCustomValidation actual = null;
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddObjectLine(typeof (TestObjectCustomValidation).ToString(), "Null");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }


        [Test]
        public void PropertySet_ValidateWithProperty()
        {
            var expected = new TestObjectCustomValidation {CustomValidation = "Validation"};
            var actual = new TestObjectCustomValidation {CustomValidation = "ValidationCustom"};
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ValidateWithProperty_IgnoreIfDefault()
        {
            var expected = new TestObjectCustomValidation();
            var actual = new TestObjectCustomValidation {CustomValidation = "ValidationCustom"};
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ValidateWithProperty_Negative()
        {
            var expected = new TestObjectCustomValidation {CustomValidation = "Validation"};
            var actual = new TestObjectCustomValidation {CustomValidation = "Validation"};
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine(expected.ValidationCustomValidation, actual.CustomValidation,
                                         "CustomValidation");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_IgnoreValidation()
        {
            var expected = new TestObjectIgnoreValidation {IgnoredField = "Validation"};
            var actual = new TestObjectIgnoreValidation {IgnoredField = "ValidationCustom"};
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_IgnoreValidationIfDefault()
        {
            var expected = new TestObjectIgnoreValidation();
            var actual = new TestObjectIgnoreValidation {Value = "ValidationCustom"};
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_IgnoreValidationIfDefault_StringAndDateTime()
        {
            var expected = new TestObjectIgnoreDefaultValidation {StringValue1 = "", StringValue2 = null};
            var actual = new TestObjectIgnoreDefaultValidation
                {
                    StringValue1 = "val",
                    StringValue2 = "test",
                    DateFiled = DateTime.Now
                };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ChildSet()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ChildSet_FirstLevelDifference()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value!",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("value", "value!", "ValidationProperty");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildSet_SecondLevelDifference()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue!"}
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine(expected.Child.ValidationProperty, actual.Child.ValidationProperty,
                                         "Child.ValidationProperty");

            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildSet_ExpectedChildNull_ActualNotNull()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = null
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddObjectLine("Null", typeof (TestObjectWithChildSet).ToString(), "Child");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildSet_ExpectedChildNotNull_ActualNull()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = null
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddObjectLine(typeof (TestObjectWithChildSet).ToString(), "Null", "Child");

            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildSet_ExpectedChildNull_ActualNull()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = null
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = null
                };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ChildSet_ExpectedChildPropertyNull_ActualNotNull()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = null}
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddObjectLine("Null", "cildvalue", "Child.ValidationProperty");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildSet_ExpectedChildPropertyNotNull_ActualNull()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = "cildvalue"}
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = null}
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddObjectLine("cildvalue", "Null", "Child.ValidationProperty");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildSet_ExpectedChildPropertyNull_ActualNull()
        {
            var expected = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = null}
                };
            var actual = new TestObjectWithChildSet
                {
                    ValidationProperty = "value",
                    Child = new TestObjectWithChildSet {ValidationProperty = null}
                };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ChildList_ExpectingNullList_ActualNotNull()
        {
            var expected = new TestObjectWithChildList {Child = null};
            var actual = new TestObjectWithChildList {Child = new List<TestObject> {new TestObject()}};
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddObjectLine("Null", typeof (List<TestObject>).ToString(), "Child");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);

            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildList_ExpectingNotNullList_ActualNull()
        {
            var expected = new TestObjectWithChildList {Child = new List<TestObject> {new TestObject()}};
            var actual = new TestObjectWithChildList {Child = null};
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddObjectLine(typeof (List<TestObject>).ToString(), "Null", "Child");

            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildList_ExpectingNullList_ActualNull()
        {
            var expected = new TestObjectWithChildList {Child = null};
            var actual = new TestObjectWithChildList {Child = null};
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }


        [Test]
        public void PropertySet_ChildList()
        {
            var expected = new TestObjectWithChildList
                {
                    Child = new List<TestObject> {new TestObject {Value = "value1"}, new TestObject {Value = "value2"}}
                };
            var actual = new TestObjectWithChildList
                {
                    Child = new List<TestObject> {new TestObject {Value = "value1"}, new TestObject {Value = "value2"}}
                };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ChildList_ItemDifference()
        {
            var expected = new TestObjectWithChildList {Child = new List<TestObject> {new TestObject {Value = "value"}}};
            var actual = new TestObjectWithChildList
                {
                    Child = new List<TestObject> {new TestObject {Value = "different"}}
                };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("value", "different", "Child[0].Value");
            var ex = Assert.Throws(typeof (AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            messageCheck.Check(ex);
        }
    }
}