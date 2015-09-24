using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Builders
{
    public class MatchWithPropertyStrategyBuilder:IMatchingStrategyBuilder
    {
        private readonly string matchPropertyName;

        public MatchWithPropertyStrategyBuilder(string matchPropertyName)
        {
            this.matchPropertyName = matchPropertyName;
        }

        public PropertyMatchingStrategy GetStrategy()
        {
            // return new ;
            throw new NotImplementedException();
        }
    }
}
