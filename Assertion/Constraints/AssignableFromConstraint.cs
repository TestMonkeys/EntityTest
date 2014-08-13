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
    ///     AssignableFromConstraint is used to test that an object
    ///     can be assigned from a given Type.
    /// </summary>
    public class AssignableFromConstraint : TypeConstraint
    {
        /// <summary>
        ///     Construct an AssignableFromConstraint for the type provided
        /// </summary>
        /// <param name="type"></param>
        public AssignableFromConstraint(Type type) : base(type)
        {
        }

        /// <summary>
        ///     Test whether an object can be assigned from the specified type
        /// </summary>
        /// <param name="actual">The object to be tested</param>
        /// <returns>True if the object can be assigned a value of the expected Type, otherwise false.</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;
            return actual != null && actual.GetType().IsAssignableFrom(expectedType);
        }

        /// <summary>
        ///     Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer">The MessageWriter to use</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("assignable from");
            writer.WriteExpectedValue(expectedType);
        }
    }
}