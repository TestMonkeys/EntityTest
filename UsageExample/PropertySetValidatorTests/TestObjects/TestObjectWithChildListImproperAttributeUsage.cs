using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildListImproperAttributeUsage
    {
        [ChildPropertySetList]
        public TestObjectWithChildListImproperAttributeUsage Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}