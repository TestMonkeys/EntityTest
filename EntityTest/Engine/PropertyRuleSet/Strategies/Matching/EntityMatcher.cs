using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestMonkey.EntityTest.Engine.HumanReadableMessages;
using TestMonkey.EntityTest.Engine.Validators;
using TestMonkey.EntityTest.PropertyAttributes;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    public class EntityMatcher : PropertyStrategy
    {
        private readonly ParentContext parentContext;
        private readonly RuleStorage rules = RuleStorage.Instance;
        private List<MatchResult> matchResults;

        public EntityMatcher()
        {
          
        }

        internal EntityMatcher(ParentContext parentContext)
        {
            this.parentContext = parentContext;
        }

        public List<MatchResult> Compare(object actualObj, object expectedObj, Type byType = null)
        {
            
            matchResults = new List<MatchResult>();
            ComputeMatch(expectedObj, actualObj, byType, parentContext);


            return matchResults;
        }

        private void ComputeMatch(object expected, object actual, Type byType, ParentContext parent)
        {
            if (NullValidationMatchOrFail(expected, actual, parent))
                return;
            byType = byType ?? expected?.GetType();

            var rule = rules.GetValidationRules(byType);
            var expectedProperties = byType.GetProperties();

            foreach (var property in expectedProperties)
            {
                var propertyName = property.Name;
                ValidateActualConstraints(property, actual, parent, rule);
                //if (property.GetCustomAttributes(typeof (IgnoreValidationAttribute), true).Any())
                if (rule.IgnoreValidationProperties.Contains(propertyName))
                    continue;
                if (!NeedsValidation(property, expected, rule))
                    continue;

                var expectedProperty = GetExpectedProperty(property, expectedProperties, rule);

                var expectedValue = GetPropertyValue(expectedProperty, expected);
                var actualValue = GetPropertyValue(property, actual);

                if (NullValidationMatchOrFail(expectedValue, actualValue, new ParentContext(parent) {ParentName = propertyName}))
                    continue;

                var matchingRule = rule.GetPropertyMatchingStrategy(expectedProperty);
                if (matchingRule != null)
                    matchResults.AddRange(matchingRule.Validate(expectedProperty, actual, expected, property,
                        new ParentContext(parent) {ParentName = property.Name}));

            }
        }

        private bool NullValidationMatchOrFail(object expected, object actual, ParentContext parent)
        {
            if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                ObjectDifferenceFound(expected, actual, parent);
                return true;
            }
            return expected == null;
        }

        private void ValidateActualConstraints(PropertyInfo property, object actual, ParentContext parent,
            ObjectPropertyValidationModel rule)
        {
            var strategy = rule.GetValidationStrategy(property);
            if (strategy == null)
                return;

            var result = strategy.Validate(property, actual, parent);
            if (!result.Success)
                PropertyDifferenceFound(result.Expected, result.Actual, result.Parent);//, result.PropertyName);
        }

        private bool NeedsValidation(PropertyInfo property, object obj, ObjectPropertyValidationModel rule)
        {
            var propertyName = property.Name;
            var value = GetPropertyValue(property, obj);
            //return !((property.GetCustomAttributes(typeof (IgnoreValidationIfDefaultAttribute), true).Any() ||
            //          property.GetCustomAttributes(typeof (ValidateActualNotNullAttribute), true).Any() ||
            //          property.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).Any()) &&
            //         IsDefault(value));
            return !((rule.IgnoreValidationIfDefault.Contains(propertyName) ||
                      rule.ActualNotNullProperties.Contains(propertyName) ||
                      rule.ActualGreaterProperties.ContainsKey(propertyName)) &&
                     IsDefault(value));
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

        private PropertyInfo GetExpectedProperty(PropertyInfo currentProperty, IEnumerable<PropertyInfo> allProperties,
            ObjectPropertyValidationModel rule)
        {
            var currentPropertyName = currentProperty.Name;
            var expectedProperty = currentProperty;
            //object validateWithPropertyAttr =
            //    currentProperty.GetCustomAttributes(typeof (ValidateWithPropertyAttribute), true).FirstOrDefault();
            //if (validateWithPropertyAttr != null)
            if (rule.ValidateActualWithExpectedProperty.ContainsKey(currentPropertyName))
            {
                expectedProperty = allProperties.FirstOrDefault(
                    //x => x.Name.Equals(((ValidateWithPropertyAttribute) validateWithPropertyAttr).PropertyName));
                    x => x.Name.Equals(rule.ValidateActualWithExpectedProperty[currentPropertyName]));
                if (expectedProperty == null)
                    throw new ImproperAttributeUsageException("ValidateWithProperty for property " +
                                                              currentProperty.Name +
                                                              " was pointing at inexisting property " +
                                                              //((ValidateWithPropertyAttribute) validateWithPropertyAttr)
                                                              //    .PropertyName);
                                                              rule.ValidateActualWithExpectedProperty[
                                                                  currentPropertyName]);
            }
            return expectedProperty;
        }

       
        private void ObjectDifferenceFound(object expectedValue, object actualValue, ParentContext parent)
        {
            var result = new MatchResult
            {
                Success = false,
                Expected = expectedValue ?? SpecialValues.Null,
                Actual = actualValue ?? SpecialValues.Null
            };
            
            if (parent!=null)
            {
                result.Parent = parent;
            }
            matchResults.Add(result);
        }

        private void PropertyDifferenceFound(object expectedValue, object actualValue, ParentContext parent)//,
            //string propertyName)
        {
            var result = new MatchResult
            {
                Success = false,
                Expected = expectedValue ?? SpecialValues.Null,
                Actual = actualValue ?? SpecialValues.Null,
                Parent = parent,
                //PropertyName = propertyName
            };
            matchResults.Add(result);
        }
    }
}