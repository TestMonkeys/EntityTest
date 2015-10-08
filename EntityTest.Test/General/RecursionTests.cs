using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityTest.Test.PropertySetValidatorTests.TestObjects;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.General
{
    public class RecursionTests
    {
        public class ObjectWithRecursiveRef
        {
            [ChildEntity]
            public ObjectWithRecursiveRef RecursiveRef { get; set; }

            [ChildEntity]
            public ChildWithRefToParent ChildWithRefToParent { get; set; }

            public int IntValue { get; set; }
        }

        public class ChildWithRefToParent
        {
            [ChildEntity]
            public ObjectWithRecursiveRef ParentRef { get; set; }
        }

        //[Ignore("Recursion Issue")]
        [Test]
        public void PropertySet_RecursiveReference_Level0()
        {
            var expected = new ObjectWithRecursiveRef {IntValue = 1};
            expected.RecursiveRef = expected;
            var actual = new ObjectWithRecursiveRef {IntValue = 1};
            actual.RecursiveRef = actual;
            Assert.That(actual,Entity.EqualTo(expected));
        }
        //[Ignore("Recursion Issue")]
        [Test]
        public void PropertySet_RecursiveReference_Level1()
        {
            var expected = new ObjectWithRecursiveRef { IntValue = 1 };
            expected.ChildWithRefToParent = new ChildWithRefToParent {ParentRef = expected};
            var actual = new ObjectWithRecursiveRef { IntValue = 1 };
            actual.ChildWithRefToParent = new ChildWithRefToParent { ParentRef = actual };
            Assert.That(actual, Entity.EqualTo(expected));
        }
    }
}
