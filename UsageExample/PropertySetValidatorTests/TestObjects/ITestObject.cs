using System.Collections.Generic;
using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace UsageExample.PropertySetValidatorTests.TestObjects
{
    public interface ITestObject
    {
        string Value1 { get; set; }

        [IgnoreValidationIfDefault]
        [ValidateWithProperty("ValidationCustomValidation")]
        string CustomValidation { get; set; }

        [IgnoreValidation]
        string ValidationCustomValidation { get; }

        [ChildPropertySetList]
        List<TestObjectWithChildSet> ChildList { get; set; }
    }
}