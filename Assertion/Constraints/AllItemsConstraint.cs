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
    ///     AllItemsConstraint applies another constraint to each
    ///     item in a collection, succeeding if they all succeed.
    /// </summary>
    public class AllItemsConstraint : PrefixConstraint
    {
        /// <summary>
        ///     Construct an AllItemsConstraint on top of an existing constraint
        /// </summary>
        /// <param name="itemConstraint"></param>
        public AllItemsConstraint(Constraint itemConstraint)
            : base(itemConstraint)
        {
            DisplayName = "all";
        }

        /// <summary>
        ///     Apply the item constraint to each item in the collection,
        ///     failing if any item fails.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            if (!(actual is IEnumerable))
                throw new ArgumentException("The internalActual value must be an IEnumerable", "actual");

            foreach (var item in (IEnumerable) actual)
                if (!baseConstraint.Matches(item))
                    return false;

            return true;
        }

        /// <summary>
        ///     Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("all items");
            baseConstraint.WriteDescriptionTo(writer);
        }
    }
}