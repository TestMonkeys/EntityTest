// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     The ConstraintOperator class is used internally by a
    ///     ConstraintBuilder to represent an operator that
    ///     modifies or combines constraints.
    ///     Constraint operators use left and right precedence
    ///     values to determine whether the top operator on the
    ///     stack should be reduced before pushing a new operator.
    /// </summary>
    public abstract class ConstraintOperator
    {
        /// <summary>
        ///     The precedence value used when the operator
        ///     is about to be pushed to the stack.
        /// </summary>
        protected int left_precedence;

        /// <summary>
        ///     The precedence value used when the operator
        ///     is on the top of the stack.
        /// </summary>
        protected int right_precedence;

        /// <summary>
        ///     The syntax element preceding this operator
        /// </summary>
        public object LeftContext { get; set; }

        /// <summary>
        ///     The syntax element folowing this operator
        /// </summary>
        public object RightContext { get; set; }

        /// <summary>
        ///     The precedence value used when the operator
        ///     is about to be pushed to the stack.
        /// </summary>
        public virtual int LeftPrecedence
        {
            get { return left_precedence; }
        }

        /// <summary>
        ///     The precedence value used when the operator
        ///     is on the top of the stack.
        /// </summary>
        public virtual int RightPrecedence
        {
            get { return right_precedence; }
        }

        /// <summary>
        ///     Reduce produces a constraint from the operator and
        ///     any arguments. It takes the arguments from the constraint
        ///     stack and pushes the resulting constraint on it.
        /// </summary>
        /// <param name="stack"></param>
        public abstract void Reduce(ConstraintBuilder.ConstraintStack stack);
    }
}