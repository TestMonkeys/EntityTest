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
            ActualNotNullProperties = new List<PropertyInfo>();
            IgnoreValidationProperties = new List<PropertyInfo>();
            IgnoreValidationIfDefault = new List<PropertyInfo>();
            ChildSetProperty = new List<PropertyInfo>();
            ChildSetListProperty = new List<PropertyInfo>();
            ActualGreaterProperties = new Dictionary<PropertyInfo, int>();
            ValidateActualWithExpectedProperty = new Dictionary<PropertyInfo, string>();
        }

        public Type TargetType { get; set; }
        public List<PropertyInfo> ActualNotNullProperties { get; }
        public List<PropertyInfo> IgnoreValidationProperties { get; private set; }
        public List<PropertyInfo> IgnoreValidationIfDefault { get; private set; }
        public List<PropertyInfo> ChildSetProperty { get; }
        public List<PropertyInfo> ChildSetListProperty { get; }
        public Dictionary<PropertyInfo, int> ActualGreaterProperties { get; }
        public Dictionary<PropertyInfo, string> ValidateActualWithExpectedProperty { get; private set; }

        public PropertyValidationStrategy GetValidationStrategy(PropertyInfo property)
        {
            if (ActualNotNullProperties.Contains(property))
                return new ActualNotNullStrategy();
            if (ActualGreaterProperties.ContainsKey(property))
                return new ActualGreaterThanValueStrategy(ActualGreaterProperties[property]);
            return null;
        }

        public PropertyMatchingStrategy GetPropertyMatchingStrategy(PropertyInfo property)
        {
            if (ChildSetProperty.Contains(property))
                return new ChildEnitityMatchingStrategy();
            if (ChildSetListProperty.Contains(property))
                return new ChildEntityListMatchingStrategy();
            return new DefaultMatchingSrategy();
        }
    }
}