// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion.Constraints;

namespace TestMonkey.Assertion
{
    /// <summary>
    ///     Summary description for FileAssert.
    /// </summary>
    public class FileAssert
    {
        #region Constructor

        /// <summary>
        ///     We don't actually want any instances of this object, but some people
        ///     like to inherit from it to add other static methods. Hence, the
        ///     protected constructor disallows any instances of this object.
        /// </summary>
        protected FileAssert()
        {
        }

        #endregion

        #region AreEqual

        #region Streams

        /// <summary>
        ///     Verifies that two Streams are equal.  Two Streams are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The internalActual Stream</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual(Stream expected, Stream actual, string message, params object[] args)
        {
            Assert.That(actual, new EqualConstraint(expected), message, args);
        }

        /// <summary>
        ///     Verifies that two Streams are equal.  Two Streams are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The internalActual Stream</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreEqual(Stream expected, Stream actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        ///     Verifies that two Streams are equal.  Two Streams are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The internalActual Stream</param>
        public static void AreEqual(Stream expected, Stream actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region FileInfo

        /// <summary>
        ///     Verifies that two files are equal.  Two files are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the actual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual(FileInfo expected, FileInfo actual, string message, params object[] args)
        {
            using (FileStream exStream = expected.OpenRead())
            {
                using (FileStream acStream = actual.OpenRead())
                {
                    AreEqual(exStream, acStream, message, args);
                }
            }
        }

        /// <summary>
        ///     Verifies that two files are equal.  Two files are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the internalActual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreEqual(FileInfo expected, FileInfo actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        ///     Verifies that two files are equal.  Two files are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the internalActual value</param>
        public static void AreEqual(FileInfo expected, FileInfo actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region String

        /// <summary>
        ///     Verifies that two files are equal.  Two files are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the internalActual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual(string expected, string actual, string message, params object[] args)
        {
            using (FileStream exStream = File.OpenRead(expected))
            {
                using (FileStream acStream = File.OpenRead(actual))
                {
                    AreEqual(exStream, acStream, message, args);
                }
            }
        }

        /// <summary>
        ///     Verifies that two files are equal.  Two files are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the internalActual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreEqual(string expected, string actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        ///     Verifies that two files are equal.  Two files are considered
        ///     equal if both are null, or if both have the same value byte for byte.
        ///     If they are not equal an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the internalActual value</param>
        public static void AreEqual(string expected, string actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #endregion

        #region AreNotEqual

        #region Streams

        /// <summary>
        ///     Asserts that two Streams are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The internalActual Stream</param>
        /// <param name="message">The message to be displayed when the two Stream are the same.</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual(Stream expected, Stream actual, string message, params object[] args)
        {
            Assert.That(actual, new NotConstraint(new EqualConstraint(expected)), message, args);
        }

        /// <summary>
        ///     Asserts that two Streams are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The internalActual Stream</param>
        /// <param name="message">The message to be displayed when the Streams are the same.</param>
        public static void AreNotEqual(Stream expected, Stream actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        ///     Asserts that two Streams are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The internalActual Stream</param>
        public static void AreNotEqual(Stream expected, Stream actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region FileInfo

        /// <summary>
        ///     Asserts that two files are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the internalActual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual(FileInfo expected, FileInfo actual, string message, params object[] args)
        {
            using (FileStream exStream = expected.OpenRead())
            {
                using (FileStream acStream = actual.OpenRead())
                {
                    AreNotEqual(exStream, acStream, message, args);
                }
            }
        }

        /// <summary>
        ///     Asserts that two files are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the internalActual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreNotEqual(FileInfo expected, FileInfo actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        ///     Asserts that two files are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the internalActual value</param>
        public static void AreNotEqual(FileInfo expected, FileInfo actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region String

        /// <summary>
        ///     Asserts that two files are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the internalActual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual(string expected, string actual, string message, params object[] args)
        {
            using (FileStream exStream = File.OpenRead(expected))
            {
                using (FileStream acStream = File.OpenRead(actual))
                {
                    AreNotEqual(exStream, acStream, message, args);
                }
            }
        }

        /// <summary>
        ///     Asserts that two files are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the internalActual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreNotEqual(string expected, string actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        ///     Asserts that two files are not equal. If they are equal
        ///     an <see cref="AssertFailedException" /> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the internalActual value</param>
        public static void AreNotEqual(string expected, string actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #endregion
    }
}