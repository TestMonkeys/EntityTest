extern alias TestAlias1;
extern alias TestAlias2;
using System;
using System.Linq;
using NUnit.Framework;
using TestMonkeys.EntityTest.Engine.PropertyRuleSet;


namespace EntityTest.Test.PropertySetValidatorTests
{
   
    public class AliasNamespaceTests
    {
        [Test]
        public void PropertySet_AliasTest()
        {
            RuleStorage rules=RuleStorage.Instance;
            Type type1= typeof(TestAlias1::AliasTestPack.Class1);
            var type1Rules=rules.GetValidationRules(type1);
            Assert.That(type1Rules.ValidationStrategyBuilders.Any(x=>x.Key.Name=="TestPack1NotNull"), Is.True, "type1 rule notNull");
            Type type2 = typeof(TestAlias2::AliasTestPack.Class1);
            var type2Rules = rules.GetValidationRules(type2);
            Assert.That(type2Rules.ValidationStrategyBuilders.Any(x => x.Key.Name =="TestPack1NotNull"), Is.False, "type1 rule notNull present");
            Assert.That(type2Rules.ValidationStrategyBuilders.Any(x => x.Key.Name == "TestPack2NotNull"), Is.True, "type1 rule notNull present");

        }

      
    }
}