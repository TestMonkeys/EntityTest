using TestMonkeys.EntityTest.Framework;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildListImproperAttributeUsage
    {
        [EntityList]
        public TestObjectWithChildListImproperAttributeUsage Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}