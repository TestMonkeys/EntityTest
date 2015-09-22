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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestMonkeys.EntityTest.Framework;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet
{
    public class RuleStorage
    {
        private static RuleStorage instance;
        private readonly Dictionary<Assembly, Dictionary<string, ObjectPropertyValidationModel>> rules;

        public RuleStorage()
        {
            rules = new Dictionary<Assembly, Dictionary<string, ObjectPropertyValidationModel>>();
        }

        public static RuleStorage Instance => instance ?? (instance = new RuleStorage());

        public void ClearRules()
        {
            rules.Clear();
        }

        public ObjectPropertyValidationModel GetValidationRules(Type objType)
        {
            var fullTypeName = objType.FullName;
            var assembly = objType.Assembly;
            if (!rules.ContainsKey(assembly))
                rules.Add(assembly, new Dictionary<string, ObjectPropertyValidationModel>());
            if (!rules[assembly].ContainsKey(fullTypeName))
                AddRule(objType);
            return GetRules(assembly, fullTypeName);
        }

        private ObjectPropertyValidationModel GetRules(Assembly assembly, string typeName)
        {
            return rules[assembly][typeName];
        }

        private void AddRule(Type objType)
        {
            var rule = new ObjectPropertyValidationModel {TargetType = objType};

            var expectedProperties = objType.GetProperties();

            rule.ActualNotNullProperties
                .AddRange(
                    expectedProperties.Where(
                        x => x.GetCustomAttributes(typeof (ValidateActualNotNullAttribute), true).Any())
                        .Select(prop => prop)
                        .ToList());

            rule.IgnoreValidationProperties
                .AddRange(
                    expectedProperties.Where(x => x.GetCustomAttributes(typeof (IgnoreValidationAttribute), true).Any())
                        .Select(prop => prop)
                        .ToList());

            rule.IgnoreValidationIfDefault
                .AddRange(
                    expectedProperties.Where(
                        x => x.GetCustomAttributes(typeof (IgnoreValidationIfDefaultAttribute), true).Any())
                        .Select(prop => prop)
                        .ToList());

            rule.ChildSetProperty
                .AddRange(
                    expectedProperties.Where(x => x.GetCustomAttributes(typeof (ChildEntityAttribute), true).Any())
                        .Select(prop => prop)
                        .ToList());

            rule.ChildSetListProperty
                .AddRange(
                    expectedProperties.Where(
                        // x => x.GetCustomAttributes(typeof (EntityListAttribute), true).Any())
                        x =>
                            x.PropertyType != typeof (string) && (typeof (IEnumerable)).IsAssignableFrom(x.PropertyType))
                        .Select(prop => prop)
                        .ToList());

            var greater =
                expectedProperties.Where(
                    x => x.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).Any())
                    .ToList();
            foreach (var property in greater)
            {
                var minValue = ((ValidateActualGreaterThanAttribute)
                    property.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).First()).Value;
                rule.ActualGreaterProperties.Add(property, minValue);
            }

            var validateWith =
                expectedProperties.Where(x => x.GetCustomAttributes(typeof (ValidateWithPropertyAttribute), true).Any())
                    .ToList();
            foreach (var property in validateWith)
            {
                var validationProp = ((ValidateWithPropertyAttribute)
                    property.GetCustomAttributes(typeof (ValidateWithPropertyAttribute), true).First()).PropertyName;
                rule.ValidateActualWithExpectedProperty.Add(property, validationProp);
            }
            rules[objType.Assembly].Add(objType.FullName, rule);
        }
    }
}