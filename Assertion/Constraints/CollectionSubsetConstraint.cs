// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System.Collections;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     CollectionSubsetConstraint is used to determine whether
    ///     one collection is a subset of another
    /// </summary>
    public class CollectionSubsetConstraint : CollectionItemsEqualConstraint
    {
        private readonly IEnumerable expected;

        /// <summary>
        ///     Construct a CollectionSubsetConstraint
        /// </summary>
        /// <param name="expected">The collection that the internalActual value is expected to be a subset of</param>
        public CollectionSubsetConstraint(IEnumerable expected) : base(expected)
        {
            this.expected = expected;
            DisplayName = "subsetof";
        }

        /// <summary>
        ///     Test whether the internalActual collection is a subset of
        ///     the expected collection provided.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch(IEnumerable actual)
        {
            return Tally(expected).TryRemove(actual);
        }

        /// <summary>
        ///     Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("subset of");
            writer.WriteExpectedValue(expected);
        }
    }
}