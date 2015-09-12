using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Engine.HumanReadableMessages;
using TestMonkey.Assertion.Extensions.Engine.Validators;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet.Strategies.Matching
{
    public class EntityMatcher:PropertyStrategy
    {
        private readonly RuleStorage rules = RuleStorage.Instance;
        private List<MatchResult> matchResults;
 
        public  List<MatchResult> Compare(object actualObj, object expectedObj, Type byType=null)
        {
            byType = byType ?? expectedObj.GetType();
            matchResults = new List<MatchResult>();
            ComputeMatch(expectedObj,actualObj,byType);
           

            return matchResults;
        }

        private void ComputeMatch(object expected, object actual, Type byType, string parent = "")
        {
            if (NullValidationMatchOrFail(expected, actual, parent))
                return;

            var rule = rules.GetRules(byType);
            PropertyInfo[] expectedProperties = byType.GetProperties();

            foreach (var property in expectedProperties)
            {
                var propertyName = property.Name;
                ValidateActualConstraints(property, actual, parent, rule);
                //if (property.GetCustomAttributes(typeof (IgnoreValidationAttribute), true).Any())
                if (rule.IgnoreValidationProperties.Contains(propertyName))
                    continue;
                if (!NeedsValidation(property, expected, rule))
                    continue;

                PropertyInfo expectedProperty = GetExpectedProperty(property, expectedProperties, rule);

                object expectedValue = GetPropertyValue(expectedProperty, expected);
                object actualValue = GetPropertyValue(property, actual);

                if (NullValidationMatchOrFail(expectedValue, actualValue, parent + property.Name))
                    continue;

                //if (property.GetCustomAttributes(typeof (ChildPropertySetAttribute), true).Any())
                if (rule.ChildSetProperty.Contains(propertyName))
                {
                    ComputeMatch(expectedValue, actualValue, expectedValue.GetType(), parent + property.Name + ".");
                }
                //else if (property.GetCustomAttributes(typeof (ChildPropertySetListAttribute), true).Any())
                else if (rule.ChildSetListProperty.Contains(propertyName))
                {
                    ComputeListMatch(expectedValue, actualValue, parent + property.Name);
                }
                else if (!expectedValue.Equals(actualValue))
                {
                    PropertyDifferenceFound(expectedValue, actualValue, parent, property.Name);
                }
            }
        }

        private bool NullValidationMatchOrFail(object expected, object actual, string parent)
        {
            if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                ObjectDifferenceFound(expected, actual, parent);
                return true;
            }
            return expected == null;
        }

        private void ValidateActualConstraints(PropertyInfo property, object actual, string parent, ObjectPropertyValidationModel rule)
        {
            var strategy = rule.GetValidationStrategy(property);
            if (strategy == null)
                return;

            var result = strategy.Validate(property, actual, parent);
            if (!result.Success)
                PropertyDifferenceFound(result.Expected, result.Actual, result.Parent, result.PropertyName);
        }

        private bool NeedsValidation(PropertyInfo property, object obj, ObjectPropertyValidationModel rule)
        {
            var propertyName = property.Name;
            object value = GetPropertyValue(property, obj);
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
            if (value is int && ((int)value) == 0) return true;
            var potentialString = value as string;
            if (potentialString != null && string.IsNullOrEmpty(potentialString)) return true;
            if (value is DateTime && ((DateTime)value).Equals(DateTime.MinValue)) return true;
            return false;
        }

        private PropertyInfo GetExpectedProperty(PropertyInfo currentProperty, IEnumerable<PropertyInfo> allProperties, ObjectPropertyValidationModel rule)
        {
            var currentPropertyName = currentProperty.Name;
            PropertyInfo expectedProperty = currentProperty;
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
                                                              rule.ValidateActualWithExpectedProperty[currentPropertyName]);
            }
            return expectedProperty;
        }

        private void ComputeListMatch(object expectedValue, object actualValue, string parent)
        {
            if (!(expectedValue is IList))
                throw new ImproperAttributeUsageException("Expected property " + parent + " is not an instance of IList");

            var expectedList = (IList)expectedValue;
            var actualList = (IList)actualValue;

            if (expectedList.Count != actualList.Count)
                PropertyDifferenceFound(expectedList.Count, actualList.Count, parent + ".", "Count");

            for (int i = 0; i < expectedList.Count; i++)
            {
                object expectedItem = expectedList[i];
                object actualItem = null;
                if (i < actualList.Count)
                    actualItem = actualList[i];

                ComputeMatch(expectedItem, actualItem, expectedItem.GetType(), parent + "[" + i + "].");
            }
        }

        private void ObjectDifferenceFound(object expectedValue, object actualValue, string parent)
        {
            var result= new MatchResult
            {
                Success = false,
                Expected = expectedValue ?? SpecialValues.Null,
                Actual = actualValue ?? SpecialValues.Null
            };
            string diffMessage;
            if (!string.IsNullOrEmpty(parent))
            {
                result.Parent=parent;
            }
            matchResults.Add(result);
        }

        private void PropertyDifferenceFound(object expectedValue, object actualValue, string parent,
                                             string propertyName)
        {
            var result = new MatchResult
            {
                Success = false,
                Expected = expectedValue ?? SpecialValues.Null,
                Actual = actualValue ?? SpecialValues.Null,
                Parent = parent,
                PropertyName = propertyName
            };
            matchResults.Add(result);
        }
        
    }
}
