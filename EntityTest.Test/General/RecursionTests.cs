using EntityTest.Test.PropertySetValidatorTests;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.General
{
    public class RecursionTests
    {
        [Test]
        public void PropertySet_Compare_RecursiveReference_Level0()
        {
            var expected = new ObjectWithRecursiveRef {IntValue = 1};
            expected.RecursiveRef = expected;
            var actual = new ObjectWithRecursiveRef {IntValue = 1};
            actual.RecursiveRef = actual;
            Assert.That(actual, Entity.EqualTo(expected));
        }

        [Test]
        public void PropertySet_Compare_RecursiveReference_Negative_ExpectedNull_Level0()
        {
            var expected = new ObjectWithRecursiveRef
            {
                IntValue = 1,
                RecursiveRef = null
            };
            var actual = new ObjectWithRecursiveRef {IntValue = 1};
            actual.RecursiveRef = actual;
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("Null", typeof (ObjectWithRecursiveRef).FullName, "RecursiveRef");
            var ex = Assert.Throws(typeof (AssertionException),
                () =>
                    Assert.That(actual, Entity.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_Compare_RecursiveReference_Negative_ActualNull_Level0()
        {
            var expected = new ObjectWithRecursiveRef {IntValue = 1};
            expected.RecursiveRef = expected;
            var actual = new ObjectWithRecursiveRef
            {
                IntValue = 1,
                RecursiveRef = null
            };
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine(typeof (ObjectWithRecursiveRef).FullName, "Null", "RecursiveRef");
            var ex = Assert.Throws(typeof (AssertionException),
                () =>
                    Assert.That(actual, Entity.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_Compare_RecursiveReference_Negative_RecursiveOnlyInActual_Level0()
        {
            var expected = new ObjectWithRecursiveRef {IntValue = 1};
            expected.RecursiveRef = new ObjectWithRecursiveRef {IntValue = 2};
            var actual = new ObjectWithRecursiveRef {IntValue = 1};
            actual.RecursiveRef = actual;
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine("Null", typeof (ObjectWithRecursiveRef).FullName, "RecursiveRef.RecursiveRef");
            messageCheck.AddPropertyLine("2", "1", "RecursiveRef.IntValue");
            var ex = Assert.Throws(typeof (AssertionException),
                () =>
                    Assert.That(actual, Entity.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_Compare_RecursiveReference_Negative_RecursiveOnlyInExpected_Level0()
        {
            var expected = new ObjectWithRecursiveRef {IntValue = 1};
            expected.RecursiveRef = expected;
            var actual = new ObjectWithRecursiveRef {IntValue = 1};
            actual.RecursiveRef = new ObjectWithRecursiveRef {IntValue = 2};
            var messageCheck = new MessageCheck("Property Set is not equal");
            messageCheck.AddPropertyLine(typeof (ObjectWithRecursiveRef).FullName, "Null", "RecursiveRef.RecursiveRef");
            messageCheck.AddPropertyLine("1", "2", "RecursiveRef.IntValue");
            var ex = Assert.Throws(typeof (AssertionException),
                () =>
                    Assert.That(actual, Entity.EqualTo(expected)));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_Compare_RecursiveReference_Level1()
        {
            var expected = new ObjectWithRecursiveRef {IntValue = 1};
            expected.ChildWithRefToParent = new ChildWithRefToParent {ParentRef = expected};
            var actual = new ObjectWithRecursiveRef {IntValue = 1};
            actual.ChildWithRefToParent = new ChildWithRefToParent {ParentRef = actual};
            Assert.That(actual, Entity.EqualTo(expected));
        }

        

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

      
    }
}