using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectImproperGreaterThan
    {
        [ValidateActualGreaterThan(0)]
        public object GreaterThanValue { get; set; }
    }
}