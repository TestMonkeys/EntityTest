using System;
using System.Collections.Generic;
using EntityTest.Test.PropertySetValidatorTests.TestObjects;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Engine.Validators;


namespace EntityTest.Test.PropertySetValidatorTests
{

    public class ListContainsPropertySetTests
    {
        [Test]
        public void ListContains_PropertySet()
        {
            var expected = new TestObjectWithChildSet
            {
                ValidationProperty = "value",
                Child = new TestObjectWithChildSet { ValidationProperty = "cildvalue" }
            };
            var actual = new TestObjectWithChildSet
            {
                ValidationProperty = "value",
                Child = new TestObjectWithChildSet { ValidationProperty = "cildvalue" }
            };
            List<TestObjectWithChildSet> list = new List<TestObjectWithChildSet>();
            for (int i=0;i<100000;i++)
            {
                var rand = new TestObjectWithChildSet
                {
                    ValidationProperty = Guid.NewGuid().ToString("n"),
                    Child = new TestObjectWithChildSet { ValidationProperty = Guid.NewGuid().ToString() }
                };
                list.Add(rand);
            }
            
            list.Add(actual);
            Assert.That(list, Entity.List.Contains(expected));
        }

      


        [Ignore("In progress")]
        [Test]
        public void ListContains_PropertySet_Failure_Closestmatch()
        {
            var expected = new SimpleTestObject
            {
                Id=5,
                FirstName="John",
                LastName="Doe"
            };
            var actual1 = new SimpleTestObject
            {
                Id=5,
                FirstName="John",
                LastName="Foo"
            };
            var actual2 = new SimpleTestObject
            {
                Id = 5,
                FirstName = "Arabella",
                LastName = "Monte"
            };
            List<SimpleTestObject> list = new List<SimpleTestObject>();
            list.Add(actual1);
            list.Add(actual2);
            Assert.That(list, Entity.List.Contains(expected, OnListContainsFailure.DisplayClosestMatch), "List");
        }
    }
}