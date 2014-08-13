// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     NullConstraint tests that the actual value is null
    /// </summary>
    public class NullConstraint : BasicConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:NullConstraint" /> class.
        /// </summary>
        public NullConstraint() : base(null, "null")
        {
        }
    }
}