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
    ///     ComparisonAdapter class centralizes all comparisons of
    ///     values in NUnit, adapting to the use of any provided
    ///     IComparer, IComparer&lt;T&gt; or Comparison&lt;T&gt;
    /// </summary>
    public abstract class ComparisonAdapter
    {
        /// <summary>
        ///     Gets the default ComparisonAdapter, which wraps an
        ///     NUnitComparer object.
        /// </summary>
        public static ComparisonAdapter Default
        {
            get { return new DefaultComparisonAdapter(); }
        }

        /// <summary>
        ///     Returns a ComparisonAdapter that wraps an IComparer
        /// </summary>
        public static ComparisonAdapter For(IComparer comparer)
        {
            return new ComparerAdapter(comparer);
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Returns a ComparisonAdapter that wraps an IComparer&lt;T&gt;
        /// </summary>
        public static ComparisonAdapter For<T>(IComparer<T> comparer)
        {
            return new ComparerAdapter<T>(comparer);
        }

        /// <summary>
        ///     Returns a ComparisonAdapter that wraps a Comparison&lt;T&gt;
        /// </summary>
        public static ComparisonAdapter For<T>(Comparison<T> comparer)
        {
            return new ComparisonAdapterForComparison<T>(comparer);
        }
#endif

        /// <summary>
        ///     Compares two objects
        /// </summary>
        public abstract int Compare(object expected, object actual);

        private class DefaultComparisonAdapter : ComparerAdapter
        {
            /// <summary>
            ///     Construct a default ComparisonAdapter
            /// </summary>
            public DefaultComparisonAdapter() : base(TestThatComparer.Default)
            {
            }
        }

        private class ComparerAdapter : ComparisonAdapter
        {
            private readonly IComparer comparer;

            /// <summary>
            ///     Construct a ComparisonAdapter for an IComparer
            /// </summary>
            public ComparerAdapter(IComparer comparer)
            {
                this.comparer = comparer;
            }

            /// <summary>
            ///     Compares two objects
            /// </summary>
            /// <param name="expected"></param>
            /// <param name="actual"></param>
            /// <returns></returns>
            public override int Compare(object expected, object actual)
            {
                return comparer.Compare(expected, actual);
            }
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     ComparisonAdapter&lt;T&gt; extends ComparisonAdapter and
        ///     allows use of an IComparer&lt;T&gt; or Comparison&lt;T&gt;
        ///     to actually perform the comparison.
        /// </summary>
        private class ComparerAdapter<T> : ComparisonAdapter
        {
            private readonly IComparer<T> comparer;

            /// <summary>
            ///     Construct a ComparisonAdapter for an IComparer&lt;T&gt;
            /// </summary>
            public ComparerAdapter(IComparer<T> comparer)
            {
                this.comparer = comparer;
            }

            /// <summary>
            ///     Compare a Type T to an object
            /// </summary>
            public override int Compare(object expected, object actual)
            {
                if (!typeof (T).IsAssignableFrom(expected.GetType()))
                    throw new ArgumentException("Cannot compare " + expected);

                if (!typeof (T).IsAssignableFrom(actual.GetType()))
                    throw new ArgumentException("Cannot compare to " + actual);

                return comparer.Compare((T) expected, (T) actual);
            }
        }

        private class ComparisonAdapterForComparison<T> : ComparisonAdapter
        {
            private readonly Comparison<T> comparison;

            /// <summary>
            ///     Construct a ComparisonAdapter for a Comparison&lt;T&gt;
            /// </summary>
            public ComparisonAdapterForComparison(Comparison<T> comparer)
            {
                comparison = comparer;
            }

            /// <summary>
            ///     Compare a Type T to an object
            /// </summary>
            public override int Compare(object expected, object actual)
            {
                if (!typeof (T).IsAssignableFrom(expected.GetType()))
                    throw new ArgumentException("Cannot compare " + expected);

                if (!typeof (T).IsAssignableFrom(actual.GetType()))
                    throw new ArgumentException("Cannot compare to " + actual);

                return comparison.Invoke((T) expected, (T) actual);
            }
        }
#endif
    }
}