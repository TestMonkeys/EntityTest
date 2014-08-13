using System.Collections.Generic;
using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public class TestObjectWithChildList
    {
        [ChildPropertySetList]
        public List<TestObject> Child { get; set; }

        public string ValidationProperty { get; set; }
    }
}