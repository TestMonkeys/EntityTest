// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     TrueConstraint tests that the internalActual value is true
    /// </summary>
    public class TrueConstraint : BasicConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TrueConstraint" /> class.
        /// </summary>
        public TrueConstraint() : base(true, "True")
        {
        }
    }
}