using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
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