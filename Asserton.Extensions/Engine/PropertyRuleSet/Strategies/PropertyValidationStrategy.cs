using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet.Strategies;
using TestMonkey.Assertion.Extensions.Engine.Validators;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet
{
    public abstract class PropertyValidationStrategy:PropertyStrategy
    {
        public abstract MatchResult Validate(PropertyInfo propertyInfo, object actualObj,
            string messagePropertyPrefix = null);

    }
}
