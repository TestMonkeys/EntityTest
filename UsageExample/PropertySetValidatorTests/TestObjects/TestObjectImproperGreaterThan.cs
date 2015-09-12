using TestMonkey.EntityTest.PropertyAttributes;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectImproperGreaterThan
    {
        [ValidateActualGreaterThan(0)]
        public object GreaterThanValue { get; set; }
    }
}