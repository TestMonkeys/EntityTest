using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildSet
    {
        [ChildPropertySet]
        public TestObjectWithChildSet Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}