﻿extern alias TestAlias1;
extern alias TestAlias2;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMonkey.Assertion;
using TestMonkey.Assertion.Exceptions;
using TestMonkey.Assertion.Extensions;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;
using UsageExample.PropertySetValidatorTests.TestObjects;
using Assert = TestMonkey.Assertion.Assert;
using TestMonkey.Assertion.Extensions.Engine.PropertyRuleSet;



namespace UsageExample.PropertySetValidatorTests
{
    [TestClass]
    public class AliasNamespaceTests
    {
        [TestMethod]
        public void PropertySet_AliasTest()
        {
            RuleStorage rules=RuleStorage.Instance;
            Type type1= typeof(TestAlias1::AliasTestPack.Class1);
            var type1Rules=rules.GetRules(type1);
            Assert.That(type1Rules.ActualNotNullProperties.Contains("TestPack1NotNull"), Is.True, "type1 rule notNull");
            Type type2 = typeof(TestAlias2::AliasTestPack.Class1);
            var type2Rules = rules.GetRules(type2);
            Assert.That(type2Rules.ActualNotNullProperties.Contains("TestPack1NotNull"), Is.False, "type1 rule notNull present");
            Assert.That(type2Rules.ActualNotNullProperties.Contains("TestPack2NotNull"), Is.True, "type1 rule notNull present");

        }

      
    }
}