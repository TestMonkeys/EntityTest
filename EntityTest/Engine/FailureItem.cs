using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TestMonkey.EntityTest.Engine
{
    internal class FailureItem
    {
        public FailureItem(string failureMessage)
        {
            FailureMessage = failureMessage;
            var stack = new StackTrace(Thread.CurrentThread, true);
            var stackTraceList = new List<string>();
            stackTraceList.AddRange(stack.ToString().Split(new[] {Environment.NewLine}, StringSplitOptions.None));
            stackTraceList.RemoveAll(x => x.Contains("TestMonkey.Assertion"));
            stackTraceList.RemoveAll(x => !x.Contains(":line "));
            StackTrace = string.Join(Environment.NewLine, stackTraceList.ToArray());
            //var frames = stack.GetFrames().Where(x => !string.IsNullOrEmpty(x.GetFileName()));
            //stackTrace = string.Join(Environment.NewLine,stack);
        }

        public string FailureMessage { get; }

        public string StackTrace { get; }
    }
}