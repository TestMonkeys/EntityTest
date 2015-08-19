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
        public Constraints.Constraint None(object expected)
        {
            return new Constraints.NotConstraint(new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing));
        }
        #endif

    }
}
