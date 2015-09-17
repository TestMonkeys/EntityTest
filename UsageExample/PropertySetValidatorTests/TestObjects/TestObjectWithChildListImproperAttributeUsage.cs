using TestMonkey.EntityTest.PropertyAttributes;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildListImproperAttributeUsage
    {
        [EntityList]
        public TestObjectWithChildListImproperAttributeUsage Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}