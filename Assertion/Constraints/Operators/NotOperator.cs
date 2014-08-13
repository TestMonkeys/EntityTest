// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Negates the test of the constraint it wraps.
    /// </summary>
    public class NotOperator : PrefixOperator
    {
        /// <summary>
        ///     Constructs a new NotOperator
        /// </summary>
        public NotOperator()
        {
            // Not stacks on anything and only allows other
            // prefix ops to stack on top of it.
            left_precedence = right_precedence = 1;
        }

        /// <summary>
        ///     Returns a NotConstraint applied to its argument.
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return new NotConstraint(constraint);
        }
    }
}