using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Exceptions;
using TestMonkeys.EntityTest;
using UsageExample.PropertySetValidatorTests.TestObjects;
using Assert = TestMonkey.Assertion.Assert;

namespace UsageExample.PropertySetValidatorTests
{
    [TestClass]
    public class BlogPropertySetExample
    {
        [TestMethod]
        public void BlogExample_PropertySet()
        {
            var expected = new BlogExampleObject
                {
                    ChildObject = new TestObject{Value="ChildObjectValue1"},
                    ChildObjectList = new List<TestObject>{new TestObject{Value="ListObject1"}},
                    IgnoredProerty = "1",
                    ProcessedProperty = "Test"
                };
            var actual = new BlogExampleObject
                {
                    ChildObject = new TestObject { Value = "ChildObjectValue2" },
                    ChildObjectList = new List<TestObject> { new TestObject { Value = "ListObject2" } },
                    IgnoredProerty = "2",
                    Parent = null,
                    CreatedDate = DateTime.Now,
                    ProcessedProperty = "Testprocessed"
                };
            Assert.That(actual, Entity.EqualTo(expected));
        }

    }
}