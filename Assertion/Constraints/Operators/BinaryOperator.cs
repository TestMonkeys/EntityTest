// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Abstract base class for all binary operators
    /// </summary>
    public abstract class BinaryOperator : ConstraintOperator
    {
        /// <summary>
        ///     Gets the left precedence of the operator
        /// </summary>
        public override int LeftPrecedence
        {
            get
            {
                return RightContext is CollectionOperator
                           ? base.LeftPrecedence + 10
                           : base.LeftPrecedence;
            }
        }

        /// <summary>
        ///     Gets the right precedence of the operator
        /// </summary>
        public override int RightPrecedence
        {
            get
            {
                return RightContext is CollectionOperator
                           ? base.RightPrecedence + 10
                           : base.RightPrecedence;
            }
        }

        /// <summary>
        ///     Reduce produces a constraint from the operator and
        ///     any arguments. It takes the arguments from the constraint
        ///     stack and pushes the resulting constraint on it.
        /// </summary>
        /// <param name="stack"></param>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            Constraint right = stack.Pop();
            Constraint left = stack.Pop();
            stack.Push(ApplyOperator(left, right));
        }

        /// <summary>
        ///     Abstract method that produces a constraint by applying
        ///     the operator to its left and right constraint arguments.
        /// </summary>
        public abstract Constraint ApplyOperator(Constraint left, Constraint right);
    }
}