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
    ///     Interface implemented by a user fixture in order to
    ///     validate any expected exceptions. It is only called
    ///     for test methods marked with the ExpectedException
    ///     attribute.
    /// </summary>
    public interface IExpectException
    {
        /// <summary>
        ///     Method to handle an expected exception
        /// </summary>
        /// <param name="ex">The exception to be handled</param>
        void HandleException(Exception ex);
    }
}