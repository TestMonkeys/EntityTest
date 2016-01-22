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
using System.Reflection;
using TestMonkeys.EntityTest.Engine.HumanReadableMessages;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Validation
{
    internal class IsDefaultValueStrategy : PropertyValidationStrategy
    {
        protected override MatchResult InternalValidate(PropertyInfo propertyInfo, object actualObj,
            ParentContext messagePropertyPrefix = null)
        {
            if (!IsDefault(GetPropertyValue(propertyInfo, actualObj)))
                return new MatchResult
                {
                    Success = false,
                    Actual = SpecialValues.Null,
                    Expected = SpecialValues.NotNull,
                    Parent = new ParentContext(messagePropertyPrefix) {ParentName = propertyInfo.Name}
                };
            return new MatchResult {Success = true};
        }

        private bool IsDefault(object value)
        {
            if (value == null) return true;
            if (value is int && ((int) value) == 0) return true;
            var potentialString = value as string;
            if (potentialString != null && string.IsNullOrEmpty(potentialString)) return true;
            if (value is DateTime && ((DateTime) value).Equals(DateTime.MinValue)) return true;
            return false;
        }
    }
}