// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     SameAsConstraint tests whether an object is identical to
    ///     the object passed to its constructor
    /// </summary>
    public class SameAsConstraint : Constraint
    {
        private readonly object expected;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:SameAsConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected object.</param>
        public SameAsConstraint(object expected) : base(expected)
        {
            this.expected = expected;
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

#if NETCF_1_0
    // TODO: THis makes it compile, now make it work.
            return expected.Equals(actual);
#else
            return ReferenceEquals(expected, actual);
#endif
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("same as");
            writer.WriteExpectedValue(expected);
        }
    }
}