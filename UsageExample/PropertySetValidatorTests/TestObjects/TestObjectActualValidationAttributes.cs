using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectActualValidationAttributes
    {
        [ValidateActualNotNull]
        [IgnoreValidationIfDefault]
        public object IdNotNull { get; set; }

        [ValidateActualGreaterThan(0)]
        public int IdGreaterThanZero { get; set; }

        //[ValidateActualGreaterThan(1)]
        //public float FloatGreaterThanZero { get; set; }
    }
}