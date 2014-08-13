// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Operator that tests that an exception is thrown and
    ///     optionally applies further tests to the exception.
    /// </summary>
    public class ThrowsOperator : SelfResolvingOperator
    {
        /// <summary>
        ///     Construct a ThrowsOperator
        /// </summary>
        public ThrowsOperator()
        {
            // ThrowsOperator stacks on everything but
            // it's always the first item on the stack
            // anyway. It is evaluated last of all ops.
            left_precedence = 1;
            right_precedence = 100;
        }

        /// <summary>
        ///     Reduce produces a constraint from the operator and
        ///     any arguments. It takes the arguments from the constraint
        ///     stack and pushes the resulting constraint on it.
        /// </summary>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (RightContext == null || RightContext is BinaryOperator)
                stack.Push(new ThrowsConstraint(null));
            else
                stack.Push(new ThrowsConstraint(stack.Pop()));
        }
    }
}