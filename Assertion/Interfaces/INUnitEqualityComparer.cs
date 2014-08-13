// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using TestMonkey.Assertion.Constraints;

namespace TestMonkey.Assertion.Interfaces
{
    /// <summary>
    /// </summary>
    public interface INUnitEqualityComparer
    {
        /// <summary>
        ///     Compares two objects for equality within a tolerance
        /// </summary>
        /// <param name="x">The first object to compare</param>
        /// <param name="y">The second object to compare</param>
        /// <param name="tolerance">The tolerance to use in the comparison</param>
        /// <returns></returns>
        bool AreEqual(object x, object y, ref Tolerance tolerance);
    }

#if CLR_2_0 || CLR_4_0
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INUnitEqualityComparer<T>
    {
        /// <summary>
        ///     Compares two objects of a given Type for equality within a tolerance
        /// </summary>
        /// <param name="x">The first object to compare</param>
        /// <param name="y">The second object to compare</param>
        /// <param name="tolerance">The tolerance to use in the comparison</param>
        /// <returns></returns>
        bool AreEqual(T x, T y, ref Tolerance tolerance);
    }
#endif
}