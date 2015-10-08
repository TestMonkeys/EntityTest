using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityTest.Test.PropertySetValidatorTests;
using EntityTest.Test.PropertySetValidatorTests.TestObjects;
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
            var validatedEntity = new ValidationEntity {Id = new object()};
            Assert.That(validatedEntity,Entity.IsValid);
        }

        [Test]
        public void PropertySet_ValidationOnly_Negative_ActualNotNull()
        {
            var validatedEntity = new ValidationEntity ();
            var messageCheck = new MessageCheck("Property Set is not valid");
            messageCheck.AddPropertyLine("Not Null", "Null", "Id");
            var ex = Assert.Throws(typeof (AssertionException),
                () =>
                    Assert.That(validatedEntity, Entity.IsValid));
            messageCheck.Check(ex);
        }

        public class ValidationEntity
        {
            [ValidateActualNotNull]
            public object Id { get; set; }
        }
    }
}
