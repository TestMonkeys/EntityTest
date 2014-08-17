using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectCustomValidationImproperAttributeUsage
    {
        [IgnoreValidationIfDefault]
        [ValidateWithProperty("ValidationCustomValidation1")]
        public string CustomValidation { get; set; }
    }
}