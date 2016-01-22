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
using TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Parameters;

namespace TestMonkeys.EntityTest.Framework
{
    /// <summary>
    ///     Will validate internalActual property with the defined property from the expected object
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateWithPropertyAttribute : StrategyParameterAttribute
    {
        public ValidateWithPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
        internal override StrategyParameter GetParameter => new ExpectedPropertyParameter(PropertyName);
    }
}