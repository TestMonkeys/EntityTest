using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityTest.Test.PropertySetValidatorTests.TestObjects;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Engine.Validators;
using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests
{
    public class EntityChildListCountTests
    {
        private class EntityWithChildListContains
        {
            [EnumerableValuesComparison(ItemsMatch.Contains)]
            public List<TestObject> Child { get; set; } 
        }

        

        [Test]
        public void PropertySet_ChildList_ExpectingMoreThanActual_ItemsMatch_Contains()
        {
            var expected = new EntityWithChildListContains
            {
                Child = new List<TestObject> { new TestObject(), new TestObject() }
            };
            var actual = new EntityWithChildListContains { Child = new List<TestObject> { new TestObject() } };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("Greater or equal to 2", "1", "Child.Count");
            messageCheck.AddObjectLine(typeof(TestObject).ToString(), "Null", "Child[1]");
            var ex = Assert.Throws(typeof(AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildList_ExpectingLessThanActual_ItemsMatch_Contains()
        {
            var expected = new EntityWithChildListContains
            {
                Child = new List<TestObject> { new TestObject() {Value = "obj 1"} }
            };
            var actual = new EntityWithChildListContains { Child = new List<TestObject> { new TestObject() {Value = "obj 1"}, new TestObject() } };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        private class EntityWithChildListIgnoreOrder
        {
            [EnumerableOrderComparison(OrderMatch.IgnoreOrder)]
            public List<TestObject> Child { get; set; }
        }

        [Test]
        public void PropertySet_ChildList_ItemsMatch_IgnoreOrder()
        {
            var expected = new EntityWithChildListIgnoreOrder
            {
                Child = new List<TestObject> { new TestObject { Value = "obj 1" }, new TestObject {Value = "obj 2"} }
            };
            var actual = new EntityWithChildListIgnoreOrder { Child = new List<TestObject> { new TestObject { Value = "obj 2" }, new TestObject {Value = "obj 1"} } };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        private class EntityWithChildListIgnoreOrderContains
        {
            [EnumerableValuesComparison(ItemsMatch.Contains)]
            [EnumerableOrderComparison(OrderMatch.IgnoreOrder)]
            public List<TestObject> Child { get; set; }
        }

        [Test]
        public void PropertySet_ChildList_ExpectingLessThanActual_ItemsMatch_IgnoreOrder_Contains()
        {
            var expected = new EntityWithChildListIgnoreOrderContains
            {
                Child = new List<TestObject> { new TestObject() { Value = "obj 1" } }
            };
            var actual = new EntityWithChildListIgnoreOrderContains { Child = new List<TestObject> { new TestObject(), new TestObject { Value = "obj 1" } } };
            Assert.That(actual, Entity.Is.EqualTo(expected));
        }

        [Test]
        public void PropertySet_ChildList_ExpectingMoreThanActual()
        {
            var expected = new TestObjectWithChildList
            {
                Child = new List<TestObject> { new TestObject(), new TestObject() }
            };
            var actual = new TestObjectWithChildList { Child = new List<TestObject> { new TestObject() } };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("2", "1", "Child.Count");
            messageCheck.AddObjectLine(typeof(TestObject).ToString(), "Null", "Child[1]");
            var ex = Assert.Throws(typeof(AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ChildList_ExpectingLessThanActual()
        {
            var expected = new TestObjectWithChildList { Child = new List<TestObject> { new TestObject() } };
            var actual = new TestObjectWithChildList { Child = new List<TestObject> { new TestObject(), new TestObject() } };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("1", "2", "Child.Count");
            var ex = Assert.Throws(typeof(AssertionException),
                                   () => Assert.That(actual, Entity.Is.EqualTo(expected)));

            messageCheck.Check(ex);
        }
    }
}
