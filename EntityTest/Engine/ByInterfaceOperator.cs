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

using System;
using TestMonkey.Assertion.Constraints;
using TestMonkey.Assertion.Constraints.Operators;
using TestMonkeys.EntityTest.Matchers;

namespace TestMonkeys.EntityTest.Engine
{
    public class ByInterfaceOperator : SelfResolvingOperator
    {
        private readonly Type type;

        public ByInterfaceOperator(Type type)
        {
            this.type = type;
        }

        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            var top = stack.Pop();
            ((EntityComparisonMatcher) top).ByInterface(type);
            stack.Push(top);
        }
    }
}