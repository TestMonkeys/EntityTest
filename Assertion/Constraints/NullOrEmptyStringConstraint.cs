// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     NullEmptyStringConstraint tests whether a string is either null or empty.
    /// </summary>
    public class NullOrEmptyStringConstraint : Constraint
    {
        /// <summary>
        ///     Constructs a new NullOrEmptyStringConstraint
        /// </summary>
        public NullOrEmptyStringConstraint()
        {
            DisplayName = "nullorempty";
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            // NOTE: Do not change this to use string.IsNullOrEmpty
            // since that won't work in earlier versions of .NET

            internalActual = actual;

            if (actual == null) return true;

            var actualAsString = actual as string;

            if (actualAsString == null)
                throw new ArgumentException("Actual value must be a string", "actual");

            return actualAsString == string.Empty;
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("null or empty string");
        }
    }
}