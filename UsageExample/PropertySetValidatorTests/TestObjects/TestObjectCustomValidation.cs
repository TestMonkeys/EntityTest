using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectCustomValidation
    {
        [IgnoreValidationIfDefault]
        [ValidateWithProperty("ValidationCustomValidation")]
        public string CustomValidation { get; set; }

        [IgnoreValidation]
        public string ValidationCustomValidation
        {
            get { return CustomValidation + "Custom"; }
        }
    }
}