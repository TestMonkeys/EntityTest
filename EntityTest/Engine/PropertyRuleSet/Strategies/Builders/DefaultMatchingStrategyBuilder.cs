namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Builders
{
    public class DefaultMatchingStrategyBuilder<TStrategy> : IMatchingStrategyBuilder
        where TStrategy : PropertyMatchingStrategy, new()
    {
        public PropertyMatchingStrategy GetStrategy()
        {
            return new TStrategy();
        }
    }
}