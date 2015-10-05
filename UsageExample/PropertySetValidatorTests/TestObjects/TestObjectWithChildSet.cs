using TestMonkeys.EntityTest.Framework;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildSet
    {
        [ChildEntity]
        public TestObjectWithChildSet Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}