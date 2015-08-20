using System;
using System.Collections.Generic;
using System.Reflection;
using TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet.Strategies;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet
{
    public class ObjectPropertyValidationModel
    {
        public Type TargetType { get; set; }

        public List<string> ActualNotNullProperties { get; private set; }

        public List<string> IgnoreValidationProperties { get; private set; }

        public List<string> IgnoreValidationIfDefault { get; private set; }

        public List<string> ChildSetProperty { get; private set; }

        public List<string> ChildSetListProperty { get; private set; }

        public Dictionary<string,int> ActualGreaterProperties { get; private set; }

        public Dictionary<string, string> ValidateActualWithExpectedProperty { get; private set; }

        public PropertyValidationStrategy GetValidationStrategy(PropertyInfo property)
        {
            if (ActualNotNullProperties.Contains(property.Name))
                return new ActualNotNullStrategy();
            if (ActualGreaterProperties.ContainsKey(property.Name))
                return new ActualGreaterThanValueStrategy(ActualGreaterProperties[property.Name]);
            return null;

        }

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
    }
}
