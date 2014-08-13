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
    ///     AttributeConstraint tests that a specified attribute is present
    ///     on a Type or other provider and that the value of the attribute
    ///     satisfies some other constraint.
    /// </summary>
    public class AttributeConstraint : PrefixConstraint
    {
        private readonly Type expectedType;
        private Attribute attrFound;

        /// <summary>
        ///     Constructs an AttributeConstraint for a specified attriute
        ///     Type and base constraint.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseConstraint"></param>
        public AttributeConstraint(Type type, Constraint baseConstraint)
            : base(baseConstraint)
        {
            expectedType = type;

            if (!typeof (Attribute).IsAssignableFrom(expectedType))
                throw new ArgumentException(string.Format(
                    "Type {0} is not an attribute", expectedType), "type");
        }

        /// <summary>
        ///     Determines whether the Type or other provider has the
        ///     expected attribute and if its value matches the
        ///     additional constraint specified.
        /// </summary>
        public override bool Matches(object actual)
        {
            internalActual = actual;
            var attrProvider =
                actual as System.Reflection.ICustomAttributeProvider;

            if (attrProvider == null)
                throw new ArgumentException(
                    string.Format("Actual value {0} does not implement ICustomAttributeProvider", actual), "actual");

            var attrs = (Attribute[]) attrProvider.GetCustomAttributes(expectedType, true);
            if (attrs.Length == 0)
                throw new ArgumentException(string.Format("Attribute {0} was not found", expectedType), "actual");

            attrFound = attrs[0];
            return baseConstraint.Matches(attrFound);
        }

        /// <summary>
        ///     Writes a description of the attribute to the specified writer.
        /// </summary>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("attribute " + expectedType.FullName);
            if (baseConstraint != null)
            {
                if (baseConstraint is EqualConstraint)
                    writer.WritePredicate("equal to");
                baseConstraint.WriteDescriptionTo(writer);
            }
        }

        /// <summary>
        ///     Writes the internalActual value supplied to the specified writer.
        /// </summary>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.WriteActualValue(attrFound);
        }

        /// <summary>
        ///     Returns a string representation of the constraint.
        /// </summary>
        protected override string GetStringRepresentation()
        {
            return string.Format("<attribute {0} {1}>", expectedType, baseConstraint);
        }
    }
}