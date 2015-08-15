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
    }
}
