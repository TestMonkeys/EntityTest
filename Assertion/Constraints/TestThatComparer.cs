// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#if CLR_2_0 || CLR_4_0

#endif

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     NUnitComparer encapsulates NUnit's default behavior
    ///     in comparing two objects.
    /// </summary>
    public class TestThatComparer : IComparer
    {
        /// <summary>
        ///     Returns the default NUnitComparer.
        /// </summary>
        public static TestThatComparer Default
        {
            get { return new TestThatComparer(); }
        }

        /// <summary>
        ///     Compares two objects
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            if (x == null)
                return y == null ? 0 : -1;
            else if (y == null)
                return +1;

            if (Numerics.IsNumericType(x) && Numerics.IsNumericType(y))
                return Numerics.Compare(x, y);

            if (x is IComparable)
                return ((IComparable) x).CompareTo(y);

            if (y is IComparable)
                return -((IComparable) y).CompareTo(x);

            Type xType = x.GetType();
            Type yType = y.GetType();

            MethodInfo method = xType.GetMethod("CompareTo", new[] {yType});
            if (method != null)
                return (int) method.Invoke(x, new[] {y});

            method = yType.GetMethod("CompareTo", new[] {xType});
            if (method != null)
                return -(int) method.Invoke(y, new[] {x});

            throw new ArgumentException("Neither value implements IComparable or IComparable<T>");
        }
    }

#if CLR_2_0 || CLR_4_0
    /// <summary>
    ///     Generic version of NUnitComparer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestThatComparer<T> : IComparer<T>
    {
        /// <summary>
        ///     Compare two objects of the same type
        /// </summary>
        public int Compare(T x, T y)
        {
            if (x == null)
                return y == null ? 0 : -1;
            else if (y == null)
                return +1;

            if (Numerics.IsNumericType(x) && Numerics.IsNumericType(y))
                return Numerics.Compare(x, y);

            if (x is IComparable<T>)
                return ((IComparable<T>) x).CompareTo(y);

            if (x is IComparable)
                return ((IComparable) x).CompareTo(y);

            throw new ArgumentException("Neither value implements IComparable or IComparable<T>");
        }
    }
#endif
}