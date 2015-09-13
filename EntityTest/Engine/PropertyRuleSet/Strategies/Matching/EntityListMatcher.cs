using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    public class EntityListMatcher
    {
        private readonly ParentContext parentContext;

        public EntityListMatcher()
        {
            parentContext = new ParentContext {ParentName = "List"};
        }

        public EntityListMatcher(ParentContext parentContext)
        {
            this.parentContext = parentContext;
        }

        public EntityListComparisonResult Compare(IList actual, IList expected)
        {
            bool overallSuccess=true;
            var comparisonResult= new EntityListComparisonResult();
            comparisonResult.EntityMatchResults=new List<EntityMatchResult>();
            comparisonResult.ListMatchResults = new List<MatchResult>();
      
            if (expected.Count != actual.Count)
                comparisonResult.ListMatchResults.Add(new MatchResult {Success = false,
                    Expected = expected.Count, Actual = actual.Count,Parent = parentContext.WithProprety("Count")});

            for (var i = 0; i < expected.Count; i++)
            {
                
                var expectedItem = expected[i];
                object actualItem = null;
                if (i < actual.Count)
                    actualItem = actual[i];
                var entityMatcher = new EntityMatcher(parentContext.WithIndex(i));
                var entityMatchResults = entityMatcher.Compare(actualItem, expectedItem, expectedItem.GetType());
                var success = entityMatchResults.All(x => x.Success);
                comparisonResult.EntityMatchResults.Add(new EntityMatchResult {Success = success, MemberResults = entityMatchResults});
                overallSuccess &= success;
            }
            comparisonResult.Equal = overallSuccess;
            return comparisonResult;
        } 
    }
}
