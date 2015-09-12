using TestMonkey.EntityTest.PropertyAttributes;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildSet
    {
        [ChildPropertySet]
        public TestObjectWithChildSet Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}