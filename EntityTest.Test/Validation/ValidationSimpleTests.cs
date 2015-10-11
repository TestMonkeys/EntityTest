using System;
using EntityTest.Test.PropertySetValidatorTests;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.Validation
{
    public class ValidationSimpleTests
    {
        [Test]
        public void PropertySet_ValidationOnly_ActualNotNull()
        {
            var validatedEntity = new ValidationEntityNotNull {Id = new object()};
            Assert.That(validatedEntity, Entity.IsValid);
        }

        [Test]
        public void PropertySet_ValidationOnly_Negative_ActualNotNull()
        {
            var validatedEntity = new ValidationEntityNotNull();
            var messageCheck = new MessageCheck("Property Set is not valid");
            messageCheck.AddPropertyLine("Not Null", "Null", "Id");
            var ex = Assert.Throws(typeof (AssertionException),
                () =>
                    Assert.That(validatedEntity, Entity.IsValid));
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ValidationOnly_ActualGreaterThan()
        {
            var validatedEntity = new ValidationEntityGreaterThan { Id = 6 };
            Assert.That(validatedEntity, Entity.IsValid);
        }

        [Test]
        public void PropertySet_ValidationOnly_Negative_ActualGreaterThan()
        {
            var validatedEntity = new ValidationEntityGreaterThan();
            var messageCheck = new MessageCheck("Property Set is not valid");
            messageCheck.AddPropertyLine("Greater than 5", "0", "Id");
            var ex = Assert.Throws(typeof(AssertionException),
                () =>
                    Assert.That(validatedEntity, Entity.IsValid));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        [Test]
        public void PropertySet_ValidationOnly_ChildObjects()
        {
            var validatedEntity = new ValidationWithChild
            {
                NotNullProp = new ValidationEntityNotNull {Id = new object()},
                GreaterThanProp = new ValidationEntityGreaterThan {Id = 6}
            };
        
            Assert.That(validatedEntity, Entity.IsValid);
        }

        [Test]
        public void PropertySet_ValidationOnly_Negative_ChildObjects()
        {
            var validatedEntity = new ValidationWithChild
            {
                NotNullProp = new ValidationEntityNotNull { Id =null },
                GreaterThanProp = new ValidationEntityGreaterThan { Id = 4 }
            };
            var messageCheck = new MessageCheck("Property Set is not valid");
            messageCheck.AddPropertyLine("Greater than 5", "4", "GreaterThanProp.Id");
            messageCheck.AddPropertyLine("Not Null", "Null", "NotNullProp.Id");
            var ex = Assert.Throws(typeof(AssertionException),
                () =>
                    Assert.That(validatedEntity, Entity.IsValid));
            Console.WriteLine(ex.Message);
            messageCheck.Check(ex);
        }

        public class ValidationEntityNotNull
        {
            [ValidateActualNotNull]
            public object Id { get; set; }
        }

        public class ValidationEntityGreaterThan
        {
            [ValidateActualGreaterThan(5)]
            public int Id { get; set; }
        }

        public class ValidationWithChild
        {
            [ChildEntity]
            public ValidationEntityNotNull NotNullProp { get; set; }
            [ChildEntity]
            public ValidationEntityGreaterThan GreaterThanProp { get; set; }
        }
    }
}