// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     FailurePoint class represents one point of failure
    ///     in an equality test.
    /// </summary>
    public class FailurePoint
    {
        /// <summary>
        ///     Indicates whether the internalActual value is valid
        /// </summary>
        public bool ActualHasData;

        /// <summary>
        ///     The internalActual value
        /// </summary>
        public object ActualValue;

        /// <summary>
        ///     Indicates whether the expected value is valid
        /// </summary>
        public bool ExpectedHasData;

        /// <summary>
        ///     The expected value
        /// </summary>
        public object ExpectedValue;

        /// <summary>
        ///     The location of the failure
        /// </summary>
        public int Position;
    }

    /// <summary>
    ///     FailurePointList represents a set of FailurePoints
    ///     in a cross-platform way.
    /// </summary>
#if CLR_2_0 || CLR_4_0
    internal class FailurePointList : System.Collections.Generic.List<FailurePoint>
    {
    }
#else
    class FailurePointList : System.Collections.ArrayList { }
#endif
}