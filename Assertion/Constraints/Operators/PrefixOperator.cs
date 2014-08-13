// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     PrefixOperator takes a single constraint and modifies
    ///     it's action in some way.
    /// </summary>
    public abstract class PrefixOperator : ConstraintOperator
    {
        /// <summary>
        ///     Reduce produces a constraint from the operator and
        ///     any arguments. It takes the arguments from the constraint
        ///     stack and pushes the resulting constraint on it.
        /// </summary>
        /// <param name="stack"></param>
        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            stack.Push(ApplyPrefix(stack.Pop()));
        }

        /// <summary>
        ///     Returns the constraint created by applying this
        ///     prefix to another constraint.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public abstract Constraint ApplyPrefix(Constraint constraint);
    }
}