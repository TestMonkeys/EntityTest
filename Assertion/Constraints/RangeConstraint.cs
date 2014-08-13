// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;

#if CLR_2_0 || CLR_4_0

#endif

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     RangeConstraint tests whether two values are within a
    ///     specified range.
    /// </summary>
#if CLR_2_0 || CLR_4_0
    public class RangeConstraint<T> : ComparisonConstraint where T : IComparable<T>
    {
        private readonly T from;
        private readonly T to;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:RangeConstraint" /> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public RangeConstraint(T from, T to)
            : base(from, to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            if (from == null || to == null || actual == null)
                throw new ArgumentException("Cannot compare using a null reference", "actual");

            return comparer.Compare(from, actual) <= 0 &&
                   comparer.Compare(to, actual) >= 0;
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("in range ({0},{1})", from, to);
        }
    }
#else
    public class RangeConstraint : ComparisonConstraint
    {
        private readonly IComparable from;
        private readonly IComparable to;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RangeConstraint"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public RangeConstraint(IComparable from, IComparable to) : base( from, to )
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="internalActual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object internalActual)
        {
            this.internalActual = internalActual;

            if ( from == null || to == null || internalActual == null)
                throw new ArgumentException( "Cannot compare using a null reference", "internalActual" );

            return comparer.Compare(from, internalActual) <= 0 &&
                   comparer.Compare(to, internalActual) >= 0;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {

            writer.Write("in range ({0},{1})", from, to);
        }
    }
#endif
}