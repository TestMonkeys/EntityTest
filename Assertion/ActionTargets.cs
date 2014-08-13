// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;

namespace TestMonkey.Assertion
{
    /// <summary>
    ///     The different targets a test action attribute can be applied to
    /// </summary>
    [Flags]
    public enum ActionTargets
    {
        /// <summary>
        ///     Default target, which is determined by where the action attribute is attached
        /// </summary>
        Default = 0,

        /// <summary>
        ///     Target a individual test case
        /// </summary>
        Test = 1,

        /// <summary>
        ///     Target a suite of test cases
        /// </summary>
        Suite = 2
    }
}

#endif