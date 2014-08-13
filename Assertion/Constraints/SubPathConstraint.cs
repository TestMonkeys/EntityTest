// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     SubPathConstraint tests that the internalActual path is under the expected path
    /// </summary>
    public class SubPathConstraint : PathConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:SubPathConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected path</param>
        public SubPathConstraint(string expected) : base(expected)
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
            return IsSubPath(Canonicalize(expectedPath), Canonicalize(actualPath), caseInsensitive);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("Path under");
            writer.WriteExpectedValue(expectedPath);
        }
    }
}