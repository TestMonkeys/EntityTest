using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestMonkey.Assertion.Exceptions;

namespace TestMonkey.Assertion.Extensions.Engine
{
    internal class CummulativeHandler
    {
        private static List<FailureItem> errorLines;

        public void AddFailureLine(Exception failure)
        {
            if (errorLines == null)
            {
                errorLines = new List<FailureItem>();
            }
            errorLines.Add(new FailureItem(failure.Message));
        }

        public void ShowFailures()
        {
            if (errorLines != null)
            {
                var stackTraceBuilder = new StringBuilder();
                foreach (var failure in errorLines)
                {
                    stackTraceBuilder.AppendLine("Assert Failed at").AppendLine(failure.StackTrace);
                }
                //string stackTrace = string.Join(Environment.NewLine+"Failure"+Environment.NewLine,
                //                                errorLines.Select(x => x.StackTrace));
                string message = string.Join(Environment.NewLine, errorLines.Select(x => x.FailureMessage));
                errorLines = null;

                throw new MultilineException(message, stackTraceBuilder.ToString());
            }
        }
    }
}