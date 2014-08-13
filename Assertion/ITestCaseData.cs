// ****************************************************************
// License Info:
// Portions Copyright © 2002-2009 Charlie Poole or 
// Copyright © 2002-2004 James W. Newkirk, Michael C. Two,
// Alexei A. Vorontsov or Copyright © 2000-2002 Philip A. Craig 
// ****************************************************************

using System;

namespace TestMonkey.Assertion
{
    /// <summary>
    ///     The ITestCaseData interface is implemented by a class
    ///     that is able to return complete testcases for use by
    ///     a parameterized test method.
    ///     NOTE: This interface is used in both the framework
    ///     and the core, even though that results in two different
    ///     types. However, sharing the source code guarantees that
    ///     the various implementations will be compatible and that
    ///     the core is able to reflect successfully over the
    ///     framework implementations of ITestCaseData.
    /// </summary>
    public interface ITestCaseData
    {
        /// <summary>
        ///     Gets the argument list to be provided to the test
        /// </summary>
        object[] Arguments { get; }

        /// <summary>
        ///     Gets the expected result
        /// </summary>
        object Result { get; }

        /// <summary>
        ///     Indicates whether a result has been specified.
        ///     This is necessary because the result may be
        ///     null, so it's value cannot be checked.
        /// </summary>
        bool HasExpectedResult { get; }

        /// <summary>
        ///     Gets the expected exception Type
        /// </summary>
        Type ExpectedException { get; }

        /// <summary>
        ///     Gets the FullName of the expected exception
        /// </summary>
        string ExpectedExceptionName { get; }

        /// <summary>
        ///     Gets the name to be used for the test
        /// </summary>
        string TestName { get; }

        /// <summary>
        ///     Gets the description of the test
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ITestCaseData" /> is ignored.
        /// </summary>
        /// <value>
        ///     <c>true</c> if ignored; otherwise, <c>false</c>.
        /// </value>
        bool Ignored { get; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ITestCaseData" /> is explicit.
        /// </summary>
        /// <value>
        ///     <c>true</c> if explicit; otherwise, <c>false</c>.
        /// </value>
        bool Explicit { get; }

        /// <summary>
        ///     Gets the ignore reason.
        /// </summary>
        /// <value>The ignore reason.</value>
        string IgnoreReason { get; }
    }
}