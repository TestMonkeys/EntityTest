// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Represents a constraint that succeeds if all the
    ///     members of a collection match a base constraint.
    /// </summary>
    public class AllOperator : CollectionOperator
    {
        /// <summary>
        ///     Returns a constraint that will apply the argument
        ///     to the members of a collection, succeeding if
        ///     they all succeed.
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return new AllItemsConstraint(constraint);
        }
    }
}