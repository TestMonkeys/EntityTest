using System;
using System.Collections.Generic;

namespace TestMonkey.Assertion.Exceptions
{
    public class AssertionFailedException : Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException
    {
        public AssertionFailedException(string message) : base(message)
        {
        }

        public override string StackTrace
        {
            get
            {
                var stackTrace = new List<string>();
                stackTrace.AddRange(base.StackTrace.Split(new[] {Environment.NewLine}, StringSplitOptions.None));
                stackTrace.RemoveAll(x => x.Contains("MonkeyTest"));
                return string.Join(Environment.NewLine, stackTrace.ToArray());
            }
        }
    }
}