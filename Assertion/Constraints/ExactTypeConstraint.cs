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
    ///     ExactTypeConstraint is used to test that an object
    ///     is of the exact type provided in the constructor
    /// </summary>
    public class ExactTypeConstraint : TypeConstraint
    {
        /// <summary>
        ///     Construct an ExactTypeConstraint for a given Type
        /// </summary>
        /// <param name="type">The expected Type.</param>
        public ExactTypeConstraint(Type type)
            : base(type)
        {
            DisplayName = "typeof";
        }

        /// <summary>
        ///     Test that an object is of the exact type specified
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <returns>True if the tested object is of the exact type provided, otherwise false.</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;
            return actual != null && actual.GetType() == expectedType;
        }

        /// <summary>
        ///     Write the description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer">The MessageWriter to use</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteExpectedValue(expectedType);
        }
    }
}