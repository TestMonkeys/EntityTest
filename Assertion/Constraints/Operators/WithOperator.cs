// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints.Operators
{
    /// <summary>
    ///     Represents a constraint that simply wraps the
    ///     constraint provided as an argument, without any
    ///     further functionality, but which modifes the
    ///     order of evaluation because of its precedence.
    /// </summary>
    public class WithOperator : PrefixOperator
    {
        /// <summary>
        ///     Constructor for the WithOperator
        /// </summary>
        public WithOperator()
        {
            left_precedence = 1;
            right_precedence = 4;
        }

        /// <summary>
        ///     Returns a constraint that wraps its argument
        /// </summary>
        public override Constraint ApplyPrefix(Constraint constraint)
        {
            return constraint;
        }
    }
}