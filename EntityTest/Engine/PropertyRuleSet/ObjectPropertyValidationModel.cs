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

        public List<string> ChildSetProperty { get; private set; }

        public List<string> ChildSetListProperty { get; private set; }

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
            return null;
        }


    }
}