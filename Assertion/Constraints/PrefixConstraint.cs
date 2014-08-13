// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     Abstract base class used for prefixes
    /// </summary>
    public abstract class PrefixConstraint : Constraint
    {
        /// <summary>
        ///     The base constraint
        /// </summary>
        protected Constraint baseConstraint;

        /// <summary>
        ///     Construct given a base constraint
        /// </summary>
        /// <param name="resolvable"></param>
        protected PrefixConstraint(IResolveConstraint resolvable) : base(resolvable)
        {
            if (resolvable != null)
                baseConstraint = resolvable.Resolve();
        }
    }
}