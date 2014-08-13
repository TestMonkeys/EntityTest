// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     Predicate constraint wraps a Predicate in a constraint,
    ///     returning success if the predicate is true.
    /// </summary>
    public class PredicateConstraint<T> : Constraint
    {
        private readonly Predicate<T> predicate;

        /// <summary>
        ///     Construct a PredicateConstraint from a predicate
        /// </summary>
        public PredicateConstraint(Predicate<T> predicate)
        {
            this.predicate = predicate;
        }

        /// <summary>
        ///     Determines whether the predicate succeeds when applied
        ///     to the internalActual value.
        /// </summary>
        public override bool Matches(object actual)
        {
            internalActual = actual;

            if (!(actual is T))
                throw new ArgumentException("The internalActual value is not of type " + typeof (T).Name, "actual");

            return predicate((T) actual);
        }

        /// <summary>
        ///     Writes the description to a MessageWriter
        /// </summary>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("value matching");
            writer.Write(predicate.Method.Name.StartsWith("<")
                             ? "lambda expression"
                             : predicate.Method.Name);
        }
    }
}

#endif