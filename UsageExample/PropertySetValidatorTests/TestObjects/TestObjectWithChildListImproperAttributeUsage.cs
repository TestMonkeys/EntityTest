using TestMonkeys.EntityTest.Framework;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildListImproperAttributeUsage
    {
        public TestObjectWithChildListImproperAttributeUsage Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}