// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Represents a constraint that succeeds if the specified
    ///     count of members of a collection match a base constraint.
    /// </summary>
    public class ExactCountOperator : CollectionOperator
    {
        private readonly int expectedCount;

        /// <summary>
        ///     Construct an ExactCountOperator for a specified count
        /// </summary>
        /// <param name="expectedCount">The expected count</param>
        public ExactCountOperator(int expectedCount)
        {
            this.expectedCount = expectedCount;
        }

        /// <summary>
        ///     Returns a constraint that will apply the argument
        ///     to the members of a collection, succeeding if
        ///     none of them succeed.
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return new ExactCountConstraint(expectedCount, constraint);
        }
    }
}