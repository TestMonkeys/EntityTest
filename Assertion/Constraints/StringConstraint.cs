// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     StringConstraint is the abstract base for constraints
    ///     that operate on strings. It supports the IgnoreCase
    ///     modifier for string operations.
    /// </summary>
    public abstract class StringConstraint : Constraint
    {
        /// <summary>
        ///     The expected value
        /// </summary>
        protected readonly string expected;

        /// <summary>
        ///     Indicates whether tests should be case-insensitive
        /// </summary>
        protected bool caseInsensitive;

        /// <summary>
        ///     Constructs a StringConstraint given an expected value
        /// </summary>
        /// <param name="expected">The expected value</param>
        protected StringConstraint(string expected)
            : base(expected)
        {
            this.expected = expected;
        }

        /// <summary>
        ///     Modify the constraint to ignore case in matching.
        /// </summary>
        public StringConstraint IgnoreCase
        {
            get
            {
                caseInsensitive = true;
                return this;
            }
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            var actualAsString = actual as string;
            return actualAsString != null && Matches(actualAsString);
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given string
        /// </summary>
        /// <param name="actual">The string to be tested</param>
        /// <returns>True for success, false for failure</returns>
        protected abstract bool Matches(string actual);
    }
}