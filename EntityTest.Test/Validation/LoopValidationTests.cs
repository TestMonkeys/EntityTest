using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TestMonkeys.EntityTest;
using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.Validation
{
    public class LoopValidationTests
    {
        [Test]
        public void PropertySet_LoopValidation_ActualNotNull()
        {
            var validatedEntity = new LoopValidationEntityNotNull();
            validatedEntity.LoopProp = validatedEntity;
            Assert.That(validatedEntity, Entity.Is.Valid);
        }

        public class LoopValidationEntityNotNull
        {
            [ValidateActualNotNull]
            [ChildEntity]
            public LoopValidationEntityNotNull LoopProp { get; set; }
        }
    }
}
