// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     SubstringConstraint can test whether a string contains
    ///     the expected substring.
    /// </summary>
    public class SubstringConstraint : StringConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:SubstringConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        public SubstringConstraint(string expected) : base(expected)
        {
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        protected override bool Matches(string actual)
        {
            if (caseInsensitive)
                return actual.ToLower().IndexOf(expected.ToLower()) >= 0;
            else
                return actual.IndexOf(expected) >= 0;
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String containing");
            writer.WriteExpectedValue(expected);
            if (caseInsensitive)
                writer.WriteModifier("ignoring case");
        }
    }
}