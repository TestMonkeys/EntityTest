// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Operator that requires both it's arguments to succeed
    /// </summary>
    public class AndOperator : BinaryOperator
    {
        /// <summary>
        ///     Construct an AndOperator
        /// </summary>
        public AndOperator()
        {
            left_precedence = right_precedence = 2;
        }

        /// <summary>
        ///     Apply the operator to produce an AndConstraint
        /// </summary>
        public override Constraint ApplyOperator(Constraint left, Constraint right)
        {
            return new AndConstraint(left, right);
        }
    }
}