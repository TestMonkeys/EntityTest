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
using System.Text;

#if CLR_2_0 || CLR_4_0

#endif

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     CollectionOrderedConstraint is used to test whether a collection is ordered.
    /// </summary>
    public class CollectionOrderedConstraint : CollectionConstraint
    {
        private ComparisonAdapter comparer = ComparisonAdapter.Default;
        private string comparerName;
        private string propertyName;
        private bool descending;

        /// <summary>
        ///     Construct a CollectionOrderedConstraint
        /// </summary>
        public CollectionOrderedConstraint()
        {
            DisplayName = "ordered";
        }

        /// <summary>
        ///     If used performs a reverse comparison
        /// </summary>
        public CollectionOrderedConstraint Descending
        {
            get
            {
                descending = true;
                return this;
            }
        }

        /// <summary>
        ///     Modifies the constraint to use an IComparer and returns self.
        /// </summary>
        public CollectionOrderedConstraint Using(IComparer comparer)
        {
            this.comparer = ComparisonAdapter.For(comparer);
            comparerName = comparer.GetType().FullName;
            return this;
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Modifies the constraint to use an IComparer&lt;T&gt; and returns self.
        /// </summary>
        public CollectionOrderedConstraint Using<T>(IComparer<T> comparer)
        {
            this.comparer = ComparisonAdapter.For(comparer);
            comparerName = comparer.GetType().FullName;
            return this;
        }

        /// <summary>
        ///     Modifies the constraint to use a Comparison&lt;T&gt; and returns self.
        /// </summary>
        public CollectionOrderedConstraint Using<T>(Comparison<T> comparer)
        {
            this.comparer = ComparisonAdapter.For(comparer);
            comparerName = comparer.GetType().FullName;
            return this;
        }
#endif

        /// <summary>
        ///     Modifies the constraint to test ordering by the value of
        ///     a specified property and returns self.
        /// </summary>
        public CollectionOrderedConstraint By(string propertyName)
        {
            this.propertyName = propertyName;
            return this;
        }

        /// <summary>
        ///     Test whether the collection is ordered
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch(IEnumerable actual)
        {
            object previous = null;
            int index = 0;
            foreach (var obj in actual)
            {
                object objToCompare = obj;
                if (obj == null)
                    throw new ArgumentNullException("actual", "Null value at index " + index.ToString());

                if (propertyName != null)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(propertyName);
                    objToCompare = prop.GetValue(obj, null);
                    if (objToCompare == null)
                        throw new ArgumentNullException("actual", "Null property value at index " + index.ToString());
                }

                if (previous != null)
                {
                    //int comparisonResult = comparer.Compare(al[i], al[i + 1]);
                    int comparisonResult = comparer.Compare(previous, objToCompare);

                    if (descending && comparisonResult < 0)
                        return false;
                    if (!descending && comparisonResult > 0)
                        return false;
                }

                previous = objToCompare;
                index++;
            }

            return true;
        }

        /// <summary>
        ///     Write a description of the constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            if (propertyName == null)
                writer.Write("collection ordered");
            else
            {
                writer.WritePredicate("collection ordered by");
                writer.WriteExpectedValue(propertyName);
            }

            if (descending)
                writer.WriteModifier("descending");
        }

        /// <summary>
        ///     Returns the string representation of the constraint.
        /// </summary>
        /// <returns></returns>
        protected override string GetStringRepresentation()
        {
            var sb = new StringBuilder("<ordered");

            if (propertyName != null)
                sb.Append("by " + propertyName);
            if (descending)
                sb.Append(" descending");
            if (comparerName != null)
                sb.Append(" " + comparerName);

            sb.Append(">");

            return sb.ToString();
        }
    }
}