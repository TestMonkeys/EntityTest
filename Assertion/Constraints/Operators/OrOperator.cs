// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Operator that requires at least one of it's arguments to succeed
    /// </summary>
    public class OrOperator : BinaryOperator
    {
        /// <summary>
        ///     Construct an OrOperator
        /// </summary>
        public OrOperator()
        {
            left_precedence = right_precedence = 3;
        }

        /// <summary>
        ///     Apply the operator to produce an OrConstraint
        /// </summary>
        public override Constraint ApplyOperator(Constraint left, Constraint right)
        {
            return new OrConstraint(left, right);
        }
    }
}