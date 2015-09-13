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
using System.Reflection;
using TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies;
using TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Matching;
using TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Validation;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet
{
    public class ObjectPropertyValidationModel
    {
        public ObjectPropertyValidationModel()
        {
            ActualNotNullProperties = new List<string>();
            IgnoreValidationProperties = new List<string>();
            IgnoreValidationIfDefault = new List<string>();
            ChildSetProperty = new List<string>();
            ChildSetListProperty = new List<string>();
            ActualGreaterProperties = new Dictionary<string, int>();
            ValidateActualWithExpectedProperty = new Dictionary<string, string>();
        }

        public Type TargetType { get; set; }
        public List<string> ActualNotNullProperties { get; }
        public List<string> IgnoreValidationProperties { get; private set; }
        public List<string> IgnoreValidationIfDefault { get; private set; }
        public List<string> ChildSetProperty { get; }
        public List<string> ChildSetListProperty { get; }
        public Dictionary<string, int> ActualGreaterProperties { get; }
        public Dictionary<string, string> ValidateActualWithExpectedProperty { get; private set; }

        public PropertyValidationStrategy GetValidationStrategy(PropertyInfo property)
        {
            if (ActualNotNullProperties.Contains(property.Name))
                return new ActualNotNullStrategy();
            if (ActualGreaterProperties.ContainsKey(property.Name))
                return new ActualGreaterThanValueStrategy(ActualGreaterProperties[property.Name]);
            return null;
        }

        public PropertyMatchingStrategy GetPropertyMatchingStrategy(PropertyInfo property)
        {
            if (ChildSetProperty.Contains(property.Name))
                return new ChildEnitityMatchingStrategy();
            if (ChildSetListProperty.Contains(property.Name))
                return new ChildEntityListMatchingStrategy();
            return new DefaultMatchingSrategy();
        }
    }
}