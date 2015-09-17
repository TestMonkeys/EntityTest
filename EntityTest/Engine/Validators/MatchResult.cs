﻿#region Copyright

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

using TestMonkey.EntityTest.Engine.PropertyRuleSet;

namespace TestMonkey.EntityTest.Engine.Validators
{
    public class MatchResult
    {
        public bool Success { get; set; }
        public object Expected { get; set; }
        public object Actual { get; set; }
        public ParentContext Parent { get; set; }

        public string GetMessage()
        {
            return Parent == null ? $"Expected <{Expected}> but found <{Actual}>" : $"Expected <{Expected}> but found <{Actual}> for <{Parent}>";
        }
    }
}