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
    // TODO Needs tests
    /// <summary>
    ///     ContainsConstraint tests a whether a string contains a substring
    ///     or a collection contains an object. It postpones the decision of
    ///     which test to use until the type of the internalActual argument is known.
    ///     This allows testing whether a string is contained in a collection
    ///     or as a substring of another string using the same syntax.
    /// </summary>
    public class ContainsConstraint : Constraint
    {
        private readonly object expected;
        private Constraint realConstraint;
        private bool ignoreCase;

#if CLR_2_0 || CLR_4_0
        private readonly List<EqualityAdapter> equalityAdapters = new List<EqualityAdapter>();
#else
        private ArrayList equalityAdapters = new ArrayList();
#endif

        private Constraint RealConstraint
        {
            get
            {
                if (realConstraint == null)
                {
                    if (internalActual is string)
                    {
                        StringConstraint constraint = new SubstringConstraint((string) expected);
                        if (ignoreCase)
                            constraint = constraint.IgnoreCase;
                        realConstraint = constraint;
                    }
                    else
                    {
                        CollectionItemsEqualConstraint constraint = new CollectionContainsConstraint(expected);

                        foreach (var adapter in equalityAdapters)
                            constraint = constraint.Using(adapter);

                        realConstraint = constraint;
                    }
                }

                return realConstraint;
            }
            set { realConstraint = value; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainsConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        public ContainsConstraint(object expected) : base(expected)
        {
            this.expected = expected;
        }

        /// <summary>
        ///     Flag the constraint to ignore case and return self.
        /// </summary>
        public ContainsConstraint IgnoreCase
        {
            get
            {
                ignoreCase = true;
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
            return RealConstraint.Matches(actual);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            RealConstraint.WriteDescriptionTo(writer);
        }

        /// <summary>
        ///     Flag the constraint to use the supplied IComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using(IComparer comparer)
        {
            return AddAdapter(EqualityAdapter.For(comparer));
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Flag the constraint to use the supplied IComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using<T>(IComparer<T> comparer)
        {
            return AddAdapter(EqualityAdapter.For(comparer));
        }

        /// <summary>
        ///     Flag the constraint to use the supplied Comparison object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using<T>(Comparison<T> comparer)
        {
            return AddAdapter(EqualityAdapter.For(comparer));
        }

        /// <summary>
        ///     Flag the constraint to use the supplied IEqualityComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using(IEqualityComparer comparer)
        {
            return AddAdapter(EqualityAdapter.For(comparer));
        }

        /// <summary>
        ///     Flag the constraint to use the supplied IEqualityComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public ContainsConstraint Using<T>(IEqualityComparer<T> comparer)
        {
            return AddAdapter(EqualityAdapter.For(comparer));
        }
#endif

        #region Helper Methods

        private ContainsConstraint AddAdapter(EqualityAdapter adapter)
        {
            equalityAdapters.Add(adapter);
            return this;
        }

        #endregion
    }
}