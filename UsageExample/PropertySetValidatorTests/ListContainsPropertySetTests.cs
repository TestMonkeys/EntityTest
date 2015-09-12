using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Exceptions;
using UsageExample.PropertySetValidatorTests.TestObjects;
using Assert = TestMonkey.Assertion.Assert;
using System.Collections.Generic;
using TestMonkey.EntityTest;
using TestMonkey.EntityTest.Engine.Validators;

namespace UsageExample.PropertySetValidatorTests
{
    [TestClass]
    public class ListContainsPropertySetTests
    {
        [TestMethod]
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
            Assert.That(list, PropertySet.List.Contains(expected));
        }

        [TestMethod]
        public void ListContains_PropertySet_Failure()
        {
            var expected = new TestObjectWithChildSet
            {
                ValidationProperty = "value",
                Child = new TestObjectWithChildSet { ValidationProperty = "cildvalue" }
            };
            var actual = new TestObjectWithChildSet
            {
                ValidationProperty = "value",
                Child = new TestObjectWithChildSet { ValidationProperty = "cildvalue1" }
            };
            List<TestObjectWithChildSet> list = new List<TestObjectWithChildSet>();
            list.Add(actual);
            Assert.That(list, PropertySet.List.Contains(expected,OnListContainsFailure.DisplayExpectedAndActualList),"List");
        }


        [TestMethod]
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
            Assert.That(list, PropertySet.List.Contains(expected, OnListContainsFailure.DisplayClosestMatch), "List");
        }
    }
}