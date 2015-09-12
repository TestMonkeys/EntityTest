using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestMonkey.EntityTest.PropertyAttributes;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet
{
    public class RuleStorage
    {
        private static RuleStorage instance;

        private readonly Dictionary<Assembly, Dictionary<string, ObjectPropertyValidationModel>> rules;

        public RuleStorage()
        {
            rules = new Dictionary<Assembly, Dictionary<string, ObjectPropertyValidationModel>>();
        }

        public static RuleStorage Instance
        {
            get { return instance ?? (instance = new RuleStorage()); }
        }

        public void ClearRules()
        {
            rules.Clear();
        }

        public ObjectPropertyValidationModel GetRules(Type objType)
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
                        .Select(prop => prop.Name)
                        .ToList());

            rule.IgnoreValidationProperties
                .AddRange(
                    expectedProperties.Where(x => x.GetCustomAttributes(typeof (IgnoreValidationAttribute), true).Any())
                        .Select(prop => prop.Name)
                        .ToList());

            rule.IgnoreValidationIfDefault
                .AddRange(
                    expectedProperties.Where(
                        x => x.GetCustomAttributes(typeof (IgnoreValidationIfDefaultAttribute), true).Any())
                        .Select(prop => prop.Name)
                        .ToList());

            rule.ChildSetProperty
                .AddRange(
                    expectedProperties.Where(x => x.GetCustomAttributes(typeof (ChildPropertySetAttribute), true).Any())
                        .Select(prop => prop.Name)
                        .ToList());

            rule.ChildSetListProperty
                .AddRange(
                    expectedProperties.Where(
                        x => x.GetCustomAttributes(typeof (ChildPropertySetListAttribute), true).Any())
                        .Select(prop => prop.Name)
                        .ToList());

            var greater =
                expectedProperties.Where(
                    x => x.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).Any())
                    .ToList();
            foreach (var property in greater)
            {
                var minValue = ((ValidateActualGreaterThanAttribute)
                    property.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).First()).Value;
                rule.ActualGreaterProperties.Add(property.Name, minValue);
            }

            var validateWith =
                expectedProperties.Where(x => x.GetCustomAttributes(typeof (ValidateWithPropertyAttribute), true).Any())
                    .ToList();
            foreach (var property in validateWith)
            {
                var validationProp = ((ValidateWithPropertyAttribute)
                    property.GetCustomAttributes(typeof (ValidateWithPropertyAttribute), true).First()).PropertyName;
                rule.ValidateActualWithExpectedProperty.Add(property.Name, validationProp);
            }
            rules[objType.Assembly].Add(objType.FullName, rule);
        }
    }
}