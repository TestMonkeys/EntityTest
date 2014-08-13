﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestMonkey.Assertion.Extensions.Framework.Constraints;
using TestMonkey.Assertion.Extensions.Framework.Properties;

namespace TestMonkey.Assertion.Extensions.Framework.Validators
{
    public class PropertySetValidator : CustomMessageConstraint
    {
        private const string Null = "Null";
        private readonly object internalExpected;
        private readonly Type validationType;
        private bool isMatch;

        internal PropertySetValidator(object expected)
        {
            if (expected == null)
                throw new ArgumentNullException("expected", "Expected can't be null");
            internalExpected = expected;
            isMatch = true;
            validationType = expected.GetType();
        }

        public PropertySetValidator(object expected, Type validationType) : this(expected)
        {
            this.validationType = validationType;
        }

        protected override string DescriptionLine
        {
            get { return "Property Set is not equal"; }
        }

        public override bool Matches(object actual)
        {
            ComputeMatch(internalExpected, actual, validationType);
            return isMatch;
        }

        private void ComputeMatch(object expected, object actual, Type byType, string parent = "")
        {
            if (NullValidationMatchOrFail(expected, actual, parent))
                return;

            PropertyInfo[] expectedProperties = byType.GetProperties();

            foreach (var property in expectedProperties)
            {
                ValidateActualConstraints(property, actual, parent);
                if (property.GetCustomAttributes(typeof (IgnoreValidationAttribute), true).Any())
                    continue;
                if (!NeedsValidation(property, expected))
                    continue;

                PropertyInfo expectedProperty = GetExpectedProperty(property, expectedProperties);

                object expectedValue = GetPropertyValue(expectedProperty, expected);
                object actualValue = GetPropertyValue(property, actual);

                if (NullValidationMatchOrFail(expectedValue, actualValue, parent + property.Name))
                    continue;

                if (property.GetCustomAttributes(typeof (ChildPropertySetAttribute), true).Any())
                {
                    ComputeMatch(expectedValue, actualValue, expectedValue.GetType(), parent + property.Name + ".");
                }
                else if (property.GetCustomAttributes(typeof (ChildPropertySetListAttribute), true).Any())
                {
                    ComputeListMatch(expectedValue, actualValue, parent + property.Name);
                }
                else if (!expectedValue.Equals(actualValue))
                {
                    PropertyDifferenceFound(expectedValue, actualValue, parent, property.Name);
                }
            }
        }

        private void ValidateActualConstraints(PropertyInfo property, object actual, string parent)
        {
            var propertyValue = GetPropertyValue(property, actual);
            if (property.GetCustomAttributes(typeof (ValidateActualNotNullAttribute), true).Any() &&
                propertyValue == null)
                PropertyDifferenceFound("NotNull", "Null", parent, property.Name);
            if (property.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).Any())
            {
                if (propertyValue == null)
                    throw new ImproperAttributeUsageException(
                        "ValidateActualGreaterThanAttribute should be defined only on numeric properties");
                try
                {
                    var doubleValue = (int) propertyValue;
                    var expectedMinimum =
                        ((ValidateActualGreaterThanAttribute)
                         property.GetCustomAttributes(typeof (ValidateActualGreaterThanAttribute), true).First()).Value;
                    if (doubleValue <= expectedMinimum)
                        PropertyDifferenceFound("Greater than " + expectedMinimum, propertyValue, parent, property.Name);
                }
                catch (Exception e)
                {
                    throw new ImproperAttributeUsageException(e,
                                                              "ValidateActualGreaterThanAttribute should be defined only on numeric properties");
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

        private object GetPropertyValue(PropertyInfo property, object obj)
        {
            object value;
            try
            {
                value = property.GetValue(obj, null);
            }
            catch (Exception e)
            {
                throw new ImproperTypeUsageException(e, "Could not get property {0} from object of type {1}",
                                                     property.Name, obj.GetType().FullName);
            }

            return value;
        }

        private void ComputeListMatch(object expectedValue, object actualValue, string parent)
        {
            if (!(expectedValue is IList))
                throw new ImproperAttributeUsageException("Expected property " + parent + " is not an instance of IList");

            var expectedList = (IList) expectedValue;
            var actualList = (IList) actualValue;

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

        private bool NeedsValidation(PropertyInfo property, object obj)
        {
            object value = GetPropertyValue(property, obj);
            return !(property.GetCustomAttributes(typeof (IgnoreValidationIfDefaultAttribute), true).Any() &&
                     IsDefault(value));
        }

        private bool IsDefault(object value)
        {
            if (value == null) return true;
            if (value is string && string.IsNullOrEmpty((string) value)) return true;
            if (value is DateTime && ((DateTime) value).Equals(DateTime.MinValue)) return true;
            return false;
        }

        private PropertyInfo GetExpectedProperty(PropertyInfo currentProperty, IEnumerable<PropertyInfo> allProperties)
        {
            PropertyInfo expectedProperty = currentProperty;
            object validateWithPropertyAttr =
                currentProperty.GetCustomAttributes(typeof (ValidateWithPropertyAttribute), true).FirstOrDefault();
            if (validateWithPropertyAttr != null)
            {
                expectedProperty = allProperties.FirstOrDefault(
                    x => x.Name.Equals(((ValidateWithPropertyAttribute) validateWithPropertyAttr).PropertyName));
                if (expectedProperty == null)
                    throw new ImproperAttributeUsageException("ValidateWithProperty for property " +
                                                              currentProperty.Name +
                                                              " was pointing at inexisting property " +
                                                              ((ValidateWithPropertyAttribute) validateWithPropertyAttr)
                                                                  .PropertyName);
            }
            return expectedProperty;
        }

        private void ObjectDifferenceFound(object expectedValue, object actualValue, string parent)
        {
            isMatch = false;
            if (string.IsNullOrEmpty(parent))
            {
                messageBuilder.AppendFormat("Expected Object <{0}> but found <{1}>",
                                            expectedValue ?? Null,
                                            actualValue ?? Null)
                              .Append(Environment.NewLine);
            }
            else
            {
                messageBuilder.AppendFormat("Expected Object <{0}> but found <{1}> for property <{2}>",
                                            expectedValue ?? Null,
                                            actualValue ?? Null, parent)
                              .Append(Environment.NewLine);
            }
        }

        private void PropertyDifferenceFound(object expectedValue, object actualValue, string parent,
                                             string propertyName)
        {
            isMatch = false;
            messageBuilder.AppendFormat("Expected <{0}> but found <{1}> for property <{2}{3}>",
                                        expectedValue ?? Null,
                                        actualValue ?? Null, parent, propertyName)
                          .Append(Environment.NewLine);
        }
    }
}