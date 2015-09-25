using System.Collections.Generic;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildList
    {
        public List<TestObject> Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}