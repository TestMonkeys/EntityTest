// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     BinaryConstraint is the abstract base of all constraints
    ///     that combine two other constraints in some fashion.
    /// </summary>
    public abstract class BinaryConstraint : Constraint
    {
        /// <summary>
        ///     The first constraint being combined
        /// </summary>
        protected Constraint left;

        /// <summary>
        ///     The second constraint being combined
        /// </summary>
        protected Constraint right;

        /// <summary>
        ///     Construct a BinaryConstraint from two other constraints
        /// </summary>
        /// <param name="left">The first constraint</param>
        /// <param name="right">The second constraint</param>
        protected BinaryConstraint(Constraint left, Constraint right)
            : base(left, right)
        {
            this.left = left;
            this.right = right;
        }
    }
}