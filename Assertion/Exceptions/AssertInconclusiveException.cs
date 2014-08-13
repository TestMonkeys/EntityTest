using System;
using System.Collections.Generic;

namespace TestMonkey.Assertion.Exceptions
{
    public class AssertInconclusiveException : Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException
    {
        public AssertInconclusiveException(string message) : base(message)
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