// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     OrConstraint succeeds if either member succeeds
    /// </summary>
    public class OrConstraint : BinaryConstraint
    {
        /// <summary>
        ///     Create an OrConstraint from two other constraints
        /// </summary>
        /// <param name="left">The first constraint</param>
        /// <param name="right">The second constraint</param>
        public OrConstraint(Constraint left, Constraint right) : base(left, right)
        {
        }

        /// <summary>
        ///     Apply the member constraints to an internalActual value, succeeding
        ///     succeeding as soon as one of them succeeds.
        /// </summary>
        /// <param name="actual">The internalActual value</param>
        /// <returns>True if either constraint succeeded</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;
            return left.Matches(actual) || right.Matches(actual);
        }

        /// <summary>
        ///     Write a description for this contraint to a MessageWriter
        /// </summary>
        /// <param name="writer">The MessageWriter to receive the description</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            left.WriteDescriptionTo(writer);
            writer.WriteConnector("or");
            right.WriteDescriptionTo(writer);
        }
    }
}