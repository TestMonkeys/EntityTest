using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildSet
    {
        [ChildEntity]
        public TestObjectWithChildSet Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}