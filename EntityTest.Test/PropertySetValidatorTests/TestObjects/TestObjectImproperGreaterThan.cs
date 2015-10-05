using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
{
    public class TestObjectImproperGreaterThan
    {
        [ValidateActualGreaterThan(0)]
        public object GreaterThanValue { get; set; }
    }
}