using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TestMonkey.Assertion.Extensions.Engine.Validators;

namespace TestMonkey.Assertion.Extensions
{
    public class ListOfPropertySetObjectsHelper
    {
        public ListContainsPropertySetConstraint Contains(object expected)
        {
            return new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing);
        }

        public ListContainsPropertySetConstraint Contains(object expected, OnListContainsFailure actionOnFailure)
        {
            return new ListContainsPropertySetConstraint(expected,actionOnFailure);
        }

        #if NUnit
        public NUnit.Framework.Constraints.Constraint None(object expected)
        {
            return NUnit.Framework.Constraints.NotConstraint(new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing));
        }
        #else
        public TestMonkey.Assertion.Constraints.Constraint None(object expected)
        {
            return new TestMonkey.Assertion.Constraints.NotConstraint(new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing));
        }
        #endif

    }
}
