// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     Tests whether a value is less than or equal to the value supplied to its constructor
    /// </summary>
    public class LessThanOrEqualConstraint : ComparisonConstraint
    {
        /// <summary>
        ///     The value against which a comparison is to be made
        /// </summary>
        private readonly object expected;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:LessThanOrEqualConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public LessThanOrEqualConstraint(object expected) : base(expected)
        {
            this.expected = expected;
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("less than or equal to");
            writer.WriteExpectedValue(expected);
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            if (expected == null || actual == null)
                throw new ArgumentException("Cannot compare using a null reference");

            return comparer.Compare(actual, expected) <= 0;
        }
    }
}