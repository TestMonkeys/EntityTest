// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     StartsWithConstraint can test whether a string starts
    ///     with an expected substring.
    /// </summary>
    public class StartsWithConstraint : StringConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:StartsWithConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected string</param>
        public StartsWithConstraint(string expected) : base(expected)
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
                return actual.ToLower().StartsWith(expected.ToLower());
            else
                return actual.StartsWith(expected);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String starting with");
            writer.WriteExpectedValue(MsgUtils.ClipString(expected, writer.MaxLineLength - 40, 0));
            if (caseInsensitive)
                writer.WriteModifier("ignoring case");
        }
    }
}