// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.Reflection;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     PropertyConstraint extracts a named property and uses
    ///     its value as the internalActual value for a chained constraint.
    /// </summary>
    public class PropertyConstraint : PrefixConstraint
    {
        private readonly string name;
        private object propValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PropertyConstraint" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="baseConstraint">The constraint to apply to the property.</param>
        public PropertyConstraint(string name, Constraint baseConstraint)
            : base(baseConstraint)
        {
            this.name = name;
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;
            Guard.ArgumentNotNull(actual, "internalActual");

            var actualType = actual as Type;
            if (actualType == null)
                actualType = actual.GetType();

            PropertyInfo property = actualType.GetProperty(name,
                                                           BindingFlags.Public | BindingFlags.NonPublic |
                                                           BindingFlags.Instance | BindingFlags.GetProperty);

            if (property == null)
                throw new ArgumentException(string.Format("Property {0} was not found", name), "name");

            propValue = property.GetValue(actual, null);
            return baseConstraint.Matches(propValue);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("property " + name);
            if (baseConstraint != null)
            {
                if (baseConstraint is EqualConstraint)
                    writer.WritePredicate("equal to");
                baseConstraint.WriteDescriptionTo(writer);
            }
        }

        /// <summary>
        ///     Write the internalActual value for a failing constraint test to a
        ///     MessageWriter. The default implementation simply writes
        ///     the raw value of internalActual, leaving it to the writer to
        ///     perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the internalActual value is displayed</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.WriteActualValue(propValue);
        }

        /// <summary>
        ///     Returns the string representation of the constraint.
        /// </summary>
        /// <returns></returns>
        protected override string GetStringRepresentation()
        {
            return string.Format("<property {0} {1}>", name, baseConstraint);
        }
    }
}