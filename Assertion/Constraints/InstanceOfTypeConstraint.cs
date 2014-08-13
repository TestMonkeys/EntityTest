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
    ///     InstanceOfTypeConstraint is used to test that an object
    ///     is of the same type provided or derived from it.
    /// </summary>
    public class InstanceOfTypeConstraint : TypeConstraint
    {
        /// <summary>
        ///     Construct an InstanceOfTypeConstraint for the type provided
        /// </summary>
        /// <param name="type">The expected Type</param>
        public InstanceOfTypeConstraint(Type type)
            : base(type)
        {
            DisplayName = "instanceof";
        }

        /// <summary>
        ///     Test whether an object is of the specified type or a derived type
        /// </summary>
        /// <param name="actual">The object to be tested</param>
        /// <returns>True if the object is of the provided type or derives from it, otherwise false.</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;
            return actual != null && expectedType.IsInstanceOfType(actual);
        }

        /// <summary>
        ///     Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer">The MessageWriter to use</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("instance of");
            writer.WriteExpectedValue(expectedType);
        }
    }
}