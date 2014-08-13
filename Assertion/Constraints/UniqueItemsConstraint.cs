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
    ///     UniqueItemsConstraint tests whether all the items in a
    ///     collection are unique.
    /// </summary>
    public class UniqueItemsConstraint : CollectionItemsEqualConstraint
    {
        /// <summary>
        ///     Check that all items are unique.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch(IEnumerable actual)
        {
            var list = new ArrayList();

            foreach (var o1 in actual)
            {
                foreach (var o2 in list)
                    if (ItemsEqual(o1, o2))
                        return false;
                list.Add(o1);
            }

            return true;
        }

        /// <summary>
        ///     Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("all items unique");
        }
    }
}