using System.Collections.Generic;
using TestMonkey.EntityTest.PropertyAttributes;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildList
    {
        [ChildPropertySetList]
        public List<TestObject> Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}