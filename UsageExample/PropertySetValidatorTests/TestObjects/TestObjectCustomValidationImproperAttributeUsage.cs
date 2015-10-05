using TestMonkeys.EntityTest.Framework;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectCustomValidationImproperAttributeUsage
    {
        [IgnoreValidationIfDefault]
        [ValidateWithProperty("ValidationCustomValidation1")]
        public string CustomValidation { get; set; }
    }
}