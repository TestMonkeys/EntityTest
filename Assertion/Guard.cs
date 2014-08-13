// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;

namespace TestMonkey.Assertion
{
    /// <summary>
    ///     Class used to guard against unexpected argument values
    ///     by throwing an appropriate exception.
    /// </summary>
    public class Guard
    {
        /// <summary>
        ///     Throws an exception if an argument is null
        /// </summary>
        /// <param name="value">The value to be tested</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentNotNull(object value, string name)
        {
            if (value == null)
                throw new ArgumentNullException("Argument " + name + " must not be null", name);
        }

        /// <summary>
        ///     Throws an exception if a string argument is null or empty
        /// </summary>
        /// <param name="value">The value to be tested</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentNotNullOrEmpty(string value, string name)
        {
            ArgumentNotNull(value, name);

            if (value == string.Empty)
                throw new ArgumentException("Argument " + name + " must not be the empty string", name);
        }
    }
}