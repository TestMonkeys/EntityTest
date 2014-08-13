// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion.Constraints;

namespace TestMonkey.Assertion
{
    /// <summary>
    ///     Provides static methods to express the assumptions
    ///     that must be met for a test to give a meaningful
    ///     result. If an assumption is not met, the test
    ///     should produce an inconclusive result.
    /// </summary>
    public class Assume
    {
        #region Equals and ReferenceEquals

        /// <summary>
        ///     The Equals method throws an AssertionException. This is done
        ///     to make sure there is no mistake by calling this function.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool Equals(object a, object b)
        {
            // TODO: This should probably be InvalidOperationException
            throw new AssertFailedException("Assert.Equals should not be used for Assertions");
        }

        /// <summary>
        ///     override the default ReferenceEquals to throw an AssertionException. This
        ///     implementation makes sure there is no mistake in calling this function
        ///     as part of Assert.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public new static void ReferenceEquals(object a, object b)
        {
            throw new AssertFailedException("Assert.ReferenceEquals should not be used for Assertions");
        }

        #endregion

        #region Assume.That

        #region Object

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="actual">The internalActual value to test</param>
        public static void That(object actual, IResolveConstraint expression)
        {
            Assume.That(actual, expression, null, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="actual">The internalActual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void That(object actual, IResolveConstraint expression, string message)
        {
            Assume.That(actual, expression, message, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="actual">The internalActual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That(object actual, IResolveConstraint expression, string message, params object[] args)
        {
            Constraint constraint = expression.Resolve();

            if (!constraint.Matches(actual))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertInconclusiveException(writer.ToString());
            }
        }

        #endregion

        #region Boolean

        /// <summary>
        ///     Asserts that a condition is true. If the condition is false the method throws
        ///     an <see cref="AssertInconclusiveException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That(bool condition, string message, params object[] args)
        {
            Assume.That(condition, Is.True, message, args);
        }

        /// <summary>
        ///     Asserts that a condition is true. If the condition is false the method throws
        ///     an <see cref="AssertInconclusiveException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        public static void That(bool condition, string message)
        {
            Assume.That(condition, Is.True, message, null);
        }

        /// <summary>
        ///     Asserts that a condition is true. If the condition is false the
        ///     method throws an <see cref="AssertInconclusiveException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        public static void That(bool condition)
        {
            Assume.That(condition, Is.True, null, null);
        }

        #endregion

        #region ref Boolean

#if !CLR_2_0 && !CLR_4_0
    /// <summary>
    /// Apply a constraint to a referenced boolean, succeeding if the constraint
    /// is satisfied and throwing an InconclusiveException on failure.
    /// </summary>
    /// <param name="expression">A Constraint expression to be applied</param>
    /// <param name="internalActual">The internalActual value to test</param>
        static public void That(ref bool internalActual, IResolveConstraint expression)
        {
            Assume.That(ref internalActual, expression.Resolve(), null, null);
        }

        /// <summary>
        /// Apply a constraint to a referenced boolean, succeeding if the constraint
        /// is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="internalActual">The internalActual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        static public void That(ref bool internalActual, IResolveConstraint expression, string message)
        {
            Assume.That(ref internalActual, expression.Resolve(), message, null);
        }

        /// <summary>
        /// Apply a constraint to a referenced boolean, succeeding if the constraint
        /// is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="internalActual">The internalActual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void That(ref bool internalActual, IResolveConstraint expression, string message, params object[] args)
        {
            Constraint constraint = expression.Resolve();

            if (!constraint.Matches(ref internalActual))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertInconclusiveException(writer.ToString());
            }
        }
#endif

        #endregion

        #region ActualValueDelegate

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        public static void That<T>(ActualValueDelegate<T> del, IResolveConstraint expr)
        {
            Assume.That(del, expr.Resolve(), null, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void That<T>(ActualValueDelegate<T> del, IResolveConstraint expr, string message)
        {
            Assume.That(del, expr.Resolve(), message, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That<T>(ActualValueDelegate<T> del, IResolveConstraint expr, string message,
                                   params object[] args)
        {
            Constraint constraint = expr.Resolve();

            if (!constraint.Matches(del))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertInconclusiveException(writer.ToString());
            }
        }
#else
    /// <summary>
    /// Apply a constraint to an internalActual value, succeeding if the constraint
    /// is satisfied and throwing an InconclusiveException on failure.
    /// </summary>
    /// <param name="expr">A Constraint expression to be applied</param>
    /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        static public void That(ActualValueDelegate del, IResolveConstraint expr)
        {
            Assume.That(del, expr.Resolve(), null, null);
        }

        /// <summary>
        /// Apply a constraint to an internalActual value, succeeding if the constraint
        /// is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="message">The message that will be displayed on failure</param>
        static public void That(ActualValueDelegate del, IResolveConstraint expr, string message)
        {
            Assume.That(del, expr.Resolve(), message, null);
        }

        /// <summary>
        /// Apply a constraint to an internalActual value, succeeding if the constraint
        /// is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void That(ActualValueDelegate del, IResolveConstraint expr, string message, params object[] args)
        {
            Constraint constraint = expr.Resolve();

            if (!constraint.Matches(del))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertInconclusiveException(writer.ToString());
            }
        }
#endif

        #endregion

        #region ref Object

#if CLR_2_0 || CLR_4_0
        /// <summary>
        ///     Apply a constraint to a referenced value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="actual">The internalActual value to test</param>
        public static void That<T>(ref T actual, IResolveConstraint expression)
        {
            Assume.That(ref actual, expression.Resolve(), null, null);
        }

        /// <summary>
        ///     Apply a constraint to a referenced value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="actual">The internalActual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void That<T>(ref T actual, IResolveConstraint expression, string message)
        {
            Assume.That(ref actual, expression.Resolve(), message, null);
        }

        /// <summary>
        ///     Apply a constraint to a referenced value, succeeding if the constraint
        ///     is satisfied and throwing an InconclusiveException on failure.
        /// </summary>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="actual">The internalActual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That<T>(ref T actual, IResolveConstraint expression, string message, params object[] args)
        {
            Constraint constraint = expression.Resolve();

            if (!constraint.Matches(ref actual))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertInconclusiveException(writer.ToString());
            }
        }
#endif

        #endregion

        #region TestDelegate

        /// <summary>
        ///     Asserts that the code represented by a delegate throws an exception
        ///     that satisfies the constraint provided.
        /// </summary>
        /// <param name="code">A TestDelegate to be executed</param>
        /// <param name="constraint">A ThrowsConstraint used in the test</param>
        public static void That(TestDelegate code, IResolveConstraint constraint)
        {
            Assume.That((object) code, constraint);
        }

        #endregion

        #endregion
    }
}