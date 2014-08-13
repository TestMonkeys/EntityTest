// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;
using System.Collections;
using System.Collections.Generic;

#if CLR_2_0 || CLR_4_0

#endif

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     EqualityAdapter class handles all equality comparisons
    ///     that use an IEqualityComparer, IEqualityComparer&lt;T&gt;
    ///     or a ComparisonAdapter.
    /// </summary>
    public abstract class EqualityAdapter
    {
        /// <summary>
        ///     Compares two objects, returning true if they are equal
        /// </summary>
        public abstract bool AreEqual(object x, object y);

        /// <summary>
        ///     Returns true if the two objects can be compared by this adapter.
        ///     The base adapter cannot handle IEnumerables except for strings.
        /// </summary>
        public virtual bool CanCompare(object x, object y)
        {
            if (x is string && y is string)
                return true;

            if (x is IEnumerable || y is IEnumerable)
                return false;

            return true;
        }

        #region Nested IComparer Adapter

        /// <summary>
        ///     Returns an EqualityAdapter that wraps an IComparer.
        /// </summary>
        public static EqualityAdapter For(IComparer comparer)
        {
            return new ComparerAdapter(comparer);
        }

        /// <summary>
        ///     EqualityAdapter that wraps an IComparer.
        /// </summary>
        private class ComparerAdapter : EqualityAdapter
        {
            private readonly IComparer comparer;

            public ComparerAdapter(IComparer comparer)
            {
                this.comparer = comparer;
            }

            public override bool AreEqual(object x, object y)
            {
                return comparer.Compare(x, y) == 0;
            }
        }

        #endregion

#if CLR_2_0 || CLR_4_0

        #region Nested IEqualityComparer Adapter

        /// <summary>
        ///     Returns an EqualityAdapter that wraps an IEqualityComparer.
        /// </summary>
        public static EqualityAdapter For(IEqualityComparer comparer)
        {
            return new EqualityComparerAdapter(comparer);
        }

        private class EqualityComparerAdapter : EqualityAdapter
        {
            private readonly IEqualityComparer comparer;

            public EqualityComparerAdapter(IEqualityComparer comparer)
            {
                this.comparer = comparer;
            }

            public override bool AreEqual(object x, object y)
            {
                return comparer.Equals(x, y);
            }
        }

        #endregion

        #region Nested GenericEqualityAdapter<T>

        private abstract class GenericEqualityAdapter<T> : EqualityAdapter
        {
            /// <summary>
            ///     Returns true if the two objects can be compared by this adapter.
            ///     Generic adapter requires objects of the specified type.
            /// </summary>
            public override bool CanCompare(object x, object y)
            {
                return typeof (T).IsAssignableFrom(x.GetType())
                       && typeof (T).IsAssignableFrom(y.GetType());
            }

            protected void ThrowIfNotCompatible(object x, object y)
            {
                if (!typeof (T).IsAssignableFrom(x.GetType()))
                    throw new ArgumentException("Cannot compare " + x);

                if (!typeof (T).IsAssignableFrom(y.GetType()))
                    throw new ArgumentException("Cannot compare " + y);
            }
        }

        #endregion

        #region Nested IEqualityComparer<T> Adapter

        /// <summary>
        ///     Returns an EqualityAdapter that wraps an IEqualityComparer&lt;T&gt;.
        /// </summary>
        public static EqualityAdapter For<T>(IEqualityComparer<T> comparer)
        {
            return new EqualityComparerAdapter<T>(comparer);
        }

        private class EqualityComparerAdapter<T> : GenericEqualityAdapter<T>
        {
            private readonly IEqualityComparer<T> comparer;

            public EqualityComparerAdapter(IEqualityComparer<T> comparer)
            {
                this.comparer = comparer;
            }

            public override bool AreEqual(object x, object y)
            {
                ThrowIfNotCompatible(x, y);
                return comparer.Equals((T) x, (T) y);
            }
        }

        #endregion

        #region Nested IComparer<T> Adapter

        /// <summary>
        ///     Returns an EqualityAdapter that wraps an IComparer&lt;T&gt;.
        /// </summary>
        public static EqualityAdapter For<T>(IComparer<T> comparer)
        {
            return new ComparerAdapter<T>(comparer);
        }

        /// <summary>
        ///     EqualityAdapter that wraps an IComparer.
        /// </summary>
        private class ComparerAdapter<T> : GenericEqualityAdapter<T>
        {
            private readonly IComparer<T> comparer;

            public ComparerAdapter(IComparer<T> comparer)
            {
                this.comparer = comparer;
            }

            public override bool AreEqual(object x, object y)
            {
                ThrowIfNotCompatible(x, y);
                return comparer.Compare((T) x, (T) y) == 0;
            }
        }

        #endregion

        #region Nested Comparison<T> Adapter

        /// <summary>
        ///     Returns an EqualityAdapter that wraps a Comparison&lt;T&gt;.
        /// </summary>
        public static EqualityAdapter For<T>(Comparison<T> comparer)
        {
            return new ComparisonAdapter<T>(comparer);
        }

        private class ComparisonAdapter<T> : GenericEqualityAdapter<T>
        {
            private readonly Comparison<T> comparer;

            public ComparisonAdapter(Comparison<T> comparer)
            {
                this.comparer = comparer;
            }

            public override bool AreEqual(object x, object y)
            {
                ThrowIfNotCompatible(x, y);
                return comparer.Invoke((T) x, (T) y) == 0;
            }
        }

        #endregion

#endif
    }

    /// <summary>
    ///     EqualityAdapterList represents a list of EqualityAdapters
    ///     in a common class across platforms.
    /// </summary>
#if CLR_2_0 || CLR_4_0
    internal class EqualityAdapterList : System.Collections.Generic.List<EqualityAdapter>
    {
    }
#else
    class EqualityAdapterList : ArrayList { }
#endif
}