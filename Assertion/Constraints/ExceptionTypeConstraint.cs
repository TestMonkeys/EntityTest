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
    ///     ExceptionTypeConstraint is a special version of ExactTypeConstraint
    ///     used to provided detailed info about the exception thrown in
    ///     an error message.
    /// </summary>
    public class ExceptionTypeConstraint : ExactTypeConstraint
    {
        /// <summary>
        ///     Constructs an ExceptionTypeConstraint
        /// </summary>
        public ExceptionTypeConstraint(Type type) : base(type)
        {
        }

        /// <summary>
        ///     Write the internalActual value for a failing constraint test to a
        ///     MessageWriter. Overriden to write additional information
        ///     in the case of an Exception.
        /// </summary>
        /// <param name="writer">The MessageWriter to use</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            var ex = internalActual as Exception;
            base.WriteActualValueTo(writer);

            if (ex != null)
            {
                writer.WriteLine(" ({0})", ex.Message);
                writer.Write(ex.StackTrace);
            }
        }
    }
}