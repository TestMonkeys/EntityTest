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
using System.Diagnostics;
using System.Threading;

namespace TestMonkeys.EntityTest.Engine
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