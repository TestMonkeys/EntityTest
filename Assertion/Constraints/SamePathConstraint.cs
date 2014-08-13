// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     Summary description for SamePathConstraint.
    /// </summary>
    public class SamePathConstraint : PathConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:SamePathConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected path</param>
        public SamePathConstraint(string expected) : base(expected)
        {
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="expectedPath">The expected path</param>
        /// <param name="actualPath">The internalActual path</param>
        /// <returns>True for success, false for failure</returns>
        protected override bool IsMatch(string expectedPath, string actualPath)
        {
            return string.Compare(Canonicalize(expectedPath), Canonicalize(actualPath), caseInsensitive) == 0;
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("Path matching");
            writer.WriteExpectedValue(expectedPath);
        }
    }
}