using System;
using System.Collections.Generic;
using TestMonkeys.EntityTest.PropertyAttributes;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class BlogExampleObject
    {
        [ValidateActualGreaterThan(0)]
        public int Id { get; set; }

        [ValidateActualNotNull]
        public BlogExampleObject Parent { get; set; }

        [IgnoreValidationIfDefault]
        public DateTime CreatedDate { get; set; }

        [ChildEntity]
        public TestObject ChildObject { get; set; }

        [EntityList]
        public List<TestObject> ChildObjectList { get; set; }

        [IgnoreValidation]
        public string IgnoredProerty { get; set; }

        [ValidateWithProperty("ValidationProcessedProperty")]
        public string ProcessedProperty { get; set; }

        [IgnoreValidation]
        public string ValidationProcessedProperty {
            get { return ProcessedProperty + "processed"; }
        }
    }
}
