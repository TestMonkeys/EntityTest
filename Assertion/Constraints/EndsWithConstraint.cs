// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     EndsWithConstraint can test whether a string ends
    ///     with an expected substring.
    /// </summary>
    public class EndsWithConstraint : StringConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:EndsWithConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected string</param>
        public EndsWithConstraint(string expected) : base(expected)
        {
        }

        /// <summary>
        ///     Test whether the constraint is matched by the internalActual value.
        ///     This is a template method, which calls the IsMatch method
        ///     of the derived class.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool Matches(string actual)
        {
            if (caseInsensitive)
                return actual.ToLower().EndsWith(expected.ToLower());
            else
                return actual.EndsWith(expected);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String ending with");
            writer.WriteExpectedValue(expected);
            if (caseInsensitive)
                writer.WriteModifier("ignoring case");
        }
    }
}