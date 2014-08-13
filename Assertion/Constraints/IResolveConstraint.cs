// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     The IConstraintExpression interface is implemented by all
    ///     complete and resolvable constraints and expressions.
    /// </summary>
    public interface IResolveConstraint
    {
        /// <summary>
        ///     Return the top-level constraint for this expression
        /// </summary>
        /// <returns></returns>
        Constraint Resolve();
    }
}