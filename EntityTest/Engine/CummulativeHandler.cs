#region Copyright

// Copyright 2015 Constantin Pascal
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkeys.EntityTest.Engine
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
                var message = string.Join(Environment.NewLine, errorLines.Select(x => x.FailureMessage));
                errorLines = null;

                throw new MultilineException(message, stackTraceBuilder.ToString());
            }
        }
    }
}