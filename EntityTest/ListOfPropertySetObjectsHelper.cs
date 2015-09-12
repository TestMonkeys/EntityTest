using TestMonkey.Assertion.Constraints;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest
{
    public class ListOfPropertySetObjectsHelper
    {
        public ListContainsPropertySetConstraint Contains(object expected)
        {
            return new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing);
        }

        public ListContainsPropertySetConstraint Contains(object expected, OnListContainsFailure actionOnFailure)
        {
            return new ListContainsPropertySetConstraint(expected, actionOnFailure);
        }

#if NUnit
        public NUnit.Framework.Constraints.Constraint None(object expected)
        {
            return NUnit.Framework.Constraints.NotConstraint(new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing));
        }
        #else
        public Constraint None(object expected)
        {
            return new NotConstraint(new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing));
        }
#endif
    }
}