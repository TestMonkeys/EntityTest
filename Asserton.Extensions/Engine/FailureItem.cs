using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TestMonkey.Assertion.Extensions.Engine
{
    internal class FailureItem
    {
        private readonly string failureMessage;
        private readonly string stackTrace;

        public FailureItem(string failureMessage)
        {
            this.failureMessage = failureMessage;
            var stack = new StackTrace(Thread.CurrentThread, true);
            var stackTraceList = new List<string>();
            stackTraceList.AddRange(stack.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None));
            stackTraceList.RemoveAll(x => x.Contains("TestMonkey.Assertion"));
            stackTraceList.RemoveAll(x => !x.Contains(":line "));
            stackTrace=string.Join(Environment.NewLine, stackTraceList.ToArray());
            //var frames = stack.GetFrames().Where(x => !string.IsNullOrEmpty(x.GetFileName()));
            //stackTrace = string.Join(Environment.NewLine,stack);
           
            
        }

        public string FailureMessage
        {
            get { return failureMessage; }
        }

        public string StackTrace
        {
            get { return stackTrace; }
        }
    }
}
