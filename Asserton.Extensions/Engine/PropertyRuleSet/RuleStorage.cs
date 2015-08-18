using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet
{
    public class RuleStorage
    {
        private static RuleStorage instance;

        public static RuleStorage Instance { get { return instance ?? (instance = new RuleStorage()); } }

        private Dictionary<string, ObjectPropertyValidationModel> rules;

        public RuleStorage()
        {
            rules = new Dictionary<string, ObjectPropertyValidationModel>();
        }

        public ObjectPropertyValidationModel GetRules(Type objType)
        {
            string fullTypeName = objType.FullName;
            if (!rules.ContainsKey(fullTypeName))
                AddRule(objType);
            return GetRules(fullTypeName);
        }

        public ObjectPropertyValidationModel GetRules(string typeName)
        {
            return rules[typeName];
        }

        private void AddRule(Type objType)
        {
            var rule = new ObjectPropertyValidationModel();
            rule.TargetType = objType;

            PropertyInfo[] expectedProperties = objType.GetProperties();

            rule.ActualNotNullProperties
                .AddRange(expectedProperties.Where(x => x.GetCustomAttributes(typeof(ValidateActualNotNullAttribute), true).Any())
                .Select(prop => prop.Name)
                .ToList());

            rule.IgnoreValidationProperties
                .AddRange(expectedProperties.Where(x => x.GetCustomAttributes(typeof(IgnoreValidationAttribute), true).Any())
                .Select(prop => prop.Name)
                .ToList());

            rule.IgnoreValidationIfDefault
                .AddRange(expectedProperties.Where(x => x.GetCustomAttributes(typeof(IgnoreValidationIfDefaultAttribute), true).Any())
                .Select(prop => prop.Name)
                .ToList());

            rule.ChildSetProperty
                .AddRange(expectedProperties.Where(x => x.GetCustomAttributes(typeof(ChildPropertySetAttribute), true).Any())
                .Select(prop => prop.Name)
                .ToList());

            rule.ChildSetListProperty
                .AddRange(expectedProperties.Where(x => x.GetCustomAttributes(typeof(ChildPropertySetListAttribute), true).Any())
                .Select(prop => prop.Name)
                .ToList());

            var greater = expectedProperties.Where(x => x.GetCustomAttributes(typeof(ValidateActualGreaterThanAttribute), true).Any())
                .ToList();
            foreach (var property in greater)
            {
                var minValue = ((ValidateActualGreaterThanAttribute)
                         property.GetCustomAttributes(typeof(ValidateActualGreaterThanAttribute), true).First()).Value;
                rule.ActualGreaterProperties.Add(property.Name,minValue);
            }

            var validateWith = expectedProperties.Where(x => x.GetCustomAttributes(typeof(ValidateWithPropertyAttribute), true).Any())
                .ToList();
            foreach (var property in validateWith)
            {
                var validationProp=((ValidateWithPropertyAttribute)
                         property.GetCustomAttributes(typeof (ValidateWithPropertyAttribute), true).First()).PropertyName;
                rule.ValidateActualWithExpectedProperty.Add(property.Name,validationProp);
            }
            rules.Add(objType.FullName,rule);
        }
    }
}
