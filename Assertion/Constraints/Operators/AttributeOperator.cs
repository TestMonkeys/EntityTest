// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Operator that tests for the presence of a particular attribute
    ///     on a type and optionally applies further tests to the attribute.
    /// </summary>
    public class AttributeOperator : SelfResolvingOperator
    {
        private readonly Type type;

        /// <summary>
        ///     Construct an AttributeOperator for a particular Type
        /// </summary>
        /// <param name="type">The Type of attribute tested</param>
        public AttributeOperator(Type type)
        {
            this.type = type;

            // Attribute stacks on anything and allows only 
            // prefix operators to stack on it.
            left_precedence = right_precedence = 1;
        }

        /// <summary>
        ///     Reduce produces a constraint from the operator and
        ///     any arguments. It takes the arguments from the constraint
        ///     stack and pushes the resulting constraint on it.
        /// </summary>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (RightContext == null || RightContext is BinaryOperator)
                stack.Push(new AttributeExistsConstraint(type));
            else
                stack.Push(new AttributeConstraint(type, stack.Pop()));
        }
    }
}