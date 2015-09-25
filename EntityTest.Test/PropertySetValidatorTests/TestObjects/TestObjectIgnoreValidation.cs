using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
{
    public class TestObjectIgnoreValidation
    {
        [IgnoreValidationIfDefault]
        public string Value { get; set; }

        [IgnoreValidation]
        public string IgnoredField { get; set; }
    }
}