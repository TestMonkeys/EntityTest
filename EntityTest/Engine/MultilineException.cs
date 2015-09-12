using System;
using System.Collections.Generic;
using TestMonkey.Assertion.Exceptions;

namespace TestMonkey.EntityTest.Engine
{
#if NUnit
    public class MultilineException : NUnit.Framework.AssertionException
#else
    public class MultilineException : AssertionFailedException
#endif

    {
        private readonly string customStackTrace;

        public MultilineException(string message, string customStackTrace) : base(message)
        {
            this.customStackTrace = customStackTrace;
        }

        public override string StackTrace
        {
            get
            {
                if (!string.IsNullOrEmpty(customStackTrace))
                {
                    return customStackTrace;
                }
                var stackTrace = new List<string>();
                stackTrace.AddRange(base.StackTrace.Split(new[] {Environment.NewLine}, StringSplitOptions.None));
#if NUnit
#else
                stackTrace.RemoveAll(x => x.Contains("TestMonkey.Assertion"));
#endif

                return string.Join(Environment.NewLine, stackTrace.ToArray());
            }
        }
    }
}