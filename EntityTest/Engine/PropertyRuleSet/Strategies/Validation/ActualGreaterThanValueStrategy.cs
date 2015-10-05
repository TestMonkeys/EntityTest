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
using TestMonkeys.EntityTest.Engine.Validators;
using TestMonkeys.EntityTest.Framework;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Validation
{
    public class ActualGreaterThanValueStrategy : PropertyValidationStrategy
    {
        private readonly int number;

        public ActualGreaterThanValueStrategy(int number)
        {
            this.number = number;
        }

        public override MatchResult Validate(PropertyInfo propertyInfo, object actualObj,
            ParentContext messagePropertyPrefix = null)
        {
            var propertyValue = GetPropertyValue(propertyInfo, actualObj);


            if (propertyValue == null)
                throw new ImproperAttributeUsageException(
                    "ValidateActualGreaterThanAttribute should be defined only on numeric properties");
            var result = new MatchResult
            {
                Success = true,
                Expected = "Greater than " + number,
                Actual = propertyValue,
                Parent = new ParentContext(messagePropertyPrefix) {ParentName = propertyInfo.Name}
            };
            try
            {
                var doubleValue = (int) propertyValue;

                if (doubleValue <= number)
                    result.Success = false;
            }
            catch (Exception e)
            {
                throw new ImproperAttributeUsageException(e,
                    "ValidateActualGreaterThanAttribute should be defined only on numeric properties");
            }
            return result;
        }
    }
}