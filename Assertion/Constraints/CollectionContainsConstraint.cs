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
    ///     CollectionContainsConstraint is used to test whether a collection
    ///     contains an expected object as a member.
    /// </summary>
    public class CollectionContainsConstraint : CollectionItemsEqualConstraint
    {
        private readonly object expected;

        /// <summary>
        ///     Construct a CollectionContainsConstraint
        /// </summary>
        /// <param name="expected"></param>
        public CollectionContainsConstraint(object expected)
            : base(expected)
        {
            this.expected = expected;
            DisplayName = "contains";
        }

        /// <summary>
        ///     Test whether the expected item is contained in the collection
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch(IEnumerable actual)
        {
            foreach (var obj in actual)
                if (ItemsEqual(obj, expected))
                    return true;

            return false;
        }

        /// <summary>
        ///     Write a descripton of the constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("collection containing");
            writer.WriteExpectedValue(expected);
        }
    }
}