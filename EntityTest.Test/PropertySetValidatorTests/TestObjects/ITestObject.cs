using System.Collections.Generic;
using TestMonkeys.EntityTest.Framework;

namespace EntityTest.Test.PropertySetValidatorTests.TestObjects
{
    public interface ITestObject
    {
        string Value1 { get; set; }

        [IgnoreValidationIfDefault]
        [ValidateWithProperty("ValidationCustomValidation")]
        string CustomValidation { get; set; }

        [IgnoreValidation]
        string ValidationCustomValidation { get; }

        List<TestObjectWithChildSet> ChildList { get; set; }
    }
}