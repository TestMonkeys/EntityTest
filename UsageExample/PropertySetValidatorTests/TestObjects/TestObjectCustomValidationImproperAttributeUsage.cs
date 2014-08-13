using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectCustomValidationImproperAttributeUsage
    {
        [IgnoreValidationIfDefault]
        [ValidateWithProperty("ValidationCustomValidation1")]
        public string CustomValidation { get; set; }
    }
}