using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Constraints;
using TestMonkey.EntityTest.Engine;
using Assert = TestMonkey.Assertion.Assert;

namespace TestMonkey.EntityTest
{
    public class CumulativeAssert
    {
        private static Dictionary<string, CummulativeHandler> cummulativeHandlers;

        protected CumulativeAssert()
        {
        }

        protected static void AddFailureLine(Exception failure)
        {
            if (cummulativeHandlers == null)
            {
                cummulativeHandlers = new Dictionary<string, CummulativeHandler>();
            }
            var callingframe =
                new StackTrace().GetFrames()
                    .FirstOrDefault(
                        x => x.GetMethod().GetCustomAttributes(typeof (TestMethodAttribute), true).Any());
            var currentCallingMethod = callingframe.GetMethod().DeclaringType + " " + callingframe.GetMethod().Name;
            if (!cummulativeHandlers.ContainsKey(currentCallingMethod))
            {
                cummulativeHandlers.Add(currentCallingMethod, new CummulativeHandler());
            }
            cummulativeHandlers[currentCallingMethod].AddFailureLine(failure);
        }

        public static void ShowFailures()
        {
            if (cummulativeHandlers != null)
            {
                var callingframe =
                    new StackTrace().GetFrames()
                        .FirstOrDefault(
                            x => x.GetMethod().GetCustomAttributes(typeof (TestMethodAttribute), true).Any());
                var currentCallingMethod = callingframe.GetMethod().DeclaringType + " " + callingframe.GetMethod().Name;
                if (cummulativeHandlers.ContainsKey(currentCallingMethod))
                    cummulativeHandlers[currentCallingMethod].ShowFailures();
            }
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The internalActual value to test</param>
        /// <param name="expression">A Constraint to be applied</param>
        public static void That(object actual, IResolveConstraint expression)
        {
            That(actual, expression, null, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The internalActual value to test</param>
        /// <param name="expression">A Constraint to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void That(object actual, IResolveConstraint expression, string message)
        {
            That(actual, expression, message, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The internalActual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That(object actual, IResolveConstraint expression, string message, params object[] args)
        {
            try
            {
                Assert.That(actual, expression, message, args);
            }
            catch (Exception e)
            {
                AddFailureLine(e);
            }
        }

        /// <summary>
        ///     Asserts that a condition is true.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That(bool condition, string message, params object[] args)
        {
            That(condition, Is.True, message, args);
        }

        /// <summary>
        ///     Asserts that a condition is true.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        public static void That(bool condition, string message)
        {
            That(condition, Is.True, message, null);
        }

        /// <summary>
        ///     Asserts that a condition is true.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        public static void That(bool condition)
        {
            That(condition, Is.True, null, null);
        }

        /// <summary>
        ///     Apply a constraint to a referenced boolean, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="internalActual">The internalActual value to test</param>
        /// <param name="constraint">A Constraint to be applied</param>
        public static void That(ref bool internalActual, IResolveConstraint constraint)
        {
            That(ref internalActual, constraint.Resolve(), null, null);
        }

        /// <summary>
        ///     Apply a constraint to a referenced value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="internalActual">The internalActual value to test</param>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void That(ref bool internalActual, IResolveConstraint constraint, string message)
        {
            That(ref internalActual, constraint.Resolve(), message, null);
        }

        /// <summary>
        ///     Apply a constraint to a referenced value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="internalActual">The internalActual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That(ref bool internalActual, IResolveConstraint expression, string message,
            params object[] args)
        {
            try
            {
                Assert.That(ref internalActual, expression, message, args);
            }
            catch (Exception e)
            {
                AddFailureLine(e);
            }
        }


        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        public static void That<T>(ActualValueDelegate<T> del, IResolveConstraint expr)
        {
            That(del, expr.Resolve(), null, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void That<T>(ActualValueDelegate<T> del, IResolveConstraint expr, string message)
        {
            That(del, expr.Resolve(), message, null);
        }

        /// <summary>
        ///     Apply a constraint to an internalActual value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="del">An ActualValueDelegate returning the value to be tested</param>
        /// <param name="expr">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That<T>(ActualValueDelegate<T> del, IResolveConstraint expr, string message,
            params object[] args)
        {
            try
            {
                Assert.That(del, expr, message, args);
            }
            catch (Exception e)
            {
                AddFailureLine(e);
            }
        }


        /// <summary>
        ///     Asserts that the code represented by a delegate throws an exception
        ///     that satisfies the constraint provided.
        /// </summary>
        /// <param name="code">A TestDelegate to be executed</param>
        /// <param name="constraint">A ThrowsConstraint used in the test</param>
        public static void That(TestDelegate code, IResolveConstraint constraint)
        {
            That((object) code, constraint);
        }
    }
}