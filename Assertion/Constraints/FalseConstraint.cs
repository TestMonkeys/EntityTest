// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     FalseConstraint tests that the internalActual value is false
    /// </summary>
    public class FalseConstraint : BasicConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:FalseConstraint" /> class.
        /// </summary>
        public FalseConstraint() : base(false, "False")
        {
        }
    }
}