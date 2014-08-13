// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

#if !NETCF
using System.Text.RegularExpressions;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     RegexConstraint can test whether a string matches
    ///     the pattern provided.
    /// </summary>
    public class RegexConstraint : StringConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:RegexConstraint" /> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public RegexConstraint(string pattern) : base(pattern)
        {
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        protected override bool Matches(string actual)
        {
            return Regex.IsMatch(
                actual,
                expected,
                caseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("String matching");
            writer.WriteExpectedValue(expected);
            if (caseInsensitive)
                writer.WriteModifier("ignoring case");
        }
    }
}

#endif