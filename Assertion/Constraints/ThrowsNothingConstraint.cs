// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;

namespace TestMonkey.Assertion.Constraints
{
    /// <summary>
    ///     ThrowsNothingConstraint tests that a delegate does not
    ///     throw an exception.
    /// </summary>
    public class ThrowsNothingConstraint : Constraint
    {
        private Exception caughtException;

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True if no exception is thrown, otherwise false</returns>
        public override bool Matches(object actual)
        {
            caughtException = ExceptionInterceptor.Intercept(actual);

            return caughtException == null;
        }

#if CLR_2_0 || CLR_4_0
        //public override bool Matches<T>(ActualValueDelegate<T> del)
        //{
        //    return Matches(new GenericInvocationDescriptor<T>(del));
        //}
#else
        public override bool Matches(ActualValueDelegate del)
        {
            return Matches(new ObjectInvocationDescriptor(del));
        }
#endif

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write(string.Format("No Exception to be thrown"));
        }

        /// <summary>
        ///     Write the internalActual value for a failing constraint test to a
        ///     MessageWriter. Overridden in ThrowsNothingConstraint to write
        ///     information about the exception that was actually caught.
        /// </summary>
        /// <param name="writer">The writer on which the internalActual value is displayed</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            writer.WriteLine(" ({0})", caughtException.Message);
            writer.Write(caughtException.StackTrace);
        }
    }
}