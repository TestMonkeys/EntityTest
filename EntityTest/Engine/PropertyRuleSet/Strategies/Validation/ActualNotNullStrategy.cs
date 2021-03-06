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

using System.Reflection;
using TestMonkeys.EntityTest.Engine.HumanReadableMessages;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Validation
{
    internal class ActualNotNullStrategy : PropertyValidationStrategy
    {
        protected override MatchResult InternalValidate(PropertyInfo propertyInfo, object actualObj,
            ParentContext messagePropertyPrefix = null)
        {
            if (GetPropertyValue(propertyInfo, actualObj) == null)
                return new MatchResult
                {
                    Success = false,
                    Actual = SpecialValues.Null,
                    Expected = SpecialValues.NotNull,
                    Parent = new ParentContext(messagePropertyPrefix) {ParentName = propertyInfo.Name}
                };
            return new MatchResult {Success = true};
        }
    }
}