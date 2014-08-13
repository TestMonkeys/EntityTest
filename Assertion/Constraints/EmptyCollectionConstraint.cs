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
    ///     EmptyCollectionConstraint tests whether a collection is empty.
    /// </summary>
    public class EmptyCollectionConstraint : CollectionConstraint
    {
        /// <summary>
        ///     Check that the collection is empty
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        protected override bool doMatch(IEnumerable collection)
        {
            return IsEmpty(collection);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("<empty>");
        }
    }
}