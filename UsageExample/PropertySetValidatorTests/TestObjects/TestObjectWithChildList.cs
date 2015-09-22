using System.Collections.Generic;
using TestMonkeys.EntityTest.Framework;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildList
    {
        [EntityList]
        public List<TestObject> Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}