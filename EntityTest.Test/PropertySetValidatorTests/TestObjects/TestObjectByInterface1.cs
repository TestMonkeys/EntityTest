using System.Collections.Generic;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
{
    public class TestObjectByInterface1 : ITestObject
    {
        public string Object1Value { get; set; }
        public string Value1 { get; set; }
        public string CustomValidation { get; set; }

        public string ValidationCustomValidation
        {
            get { return CustomValidation + "Custom"; }
        }

        public List<TestObjectWithChildSet> ChildList { get; set; }
    }
}