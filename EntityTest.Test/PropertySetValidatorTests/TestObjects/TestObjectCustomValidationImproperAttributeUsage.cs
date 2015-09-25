using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
{
    public class TestObjectCustomValidationImproperAttributeUsage
    {
        [IgnoreValidationIfDefault]
        [ValidateWithProperty("ValidationCustomValidation1")]
        public string CustomValidation { get; set; }
    }
}