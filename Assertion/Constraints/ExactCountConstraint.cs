// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.Collections;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     ExactCountConstraint applies another constraint to each
    ///     item in a collection, succeeding only if a specified
    ///     number of items succeed.
    /// </summary>
    public class ExactCountConstraint : PrefixConstraint
    {
        private readonly int expectedCount;

        /// <summary>
        ///     Construct an ExactCountConstraint on top of an existing constraint
        /// </summary>
        /// <param name="expectedCount"></param>
        /// <param name="itemConstraint"></param>
        public ExactCountConstraint(int expectedCount, Constraint itemConstraint)
            : base(itemConstraint)
        {
            DisplayName = "one";
            this.expectedCount = expectedCount;
        }

        /// <summary>
        ///     Apply the item constraint to each item in the collection,
        ///     succeeding only if the expected number of items pass.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            if (!(actual is IEnumerable))
                throw new ArgumentException("The internalActual value must be an IEnumerable", "actual");

            int count = 0;
            foreach (var item in (IEnumerable) actual)
                if (baseConstraint.Matches(item))
                    count++;

            return count == expectedCount;
        }

        /// <summary>
        ///     Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            switch (expectedCount)
            {
                case 0:
                    writer.WritePredicate("no item");
                    break;
                case 1:
                    writer.WritePredicate("exactly one item");
                    break;
                default:
                    writer.WritePredicate("exactly " + expectedCount.ToString() + " items");
                    break;
            }

            baseConstraint.WriteDescriptionTo(writer);
        }
    }
}