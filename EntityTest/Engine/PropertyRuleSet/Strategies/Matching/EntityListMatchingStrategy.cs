#region Copyright

// Copyright 2015 Constantin Pascal
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    internal class EntityListMatchingStrategy
    {
        private readonly ParentContext parentContext;

        public EntityListMatchingStrategy()
        {
            parentContext = new ParentContext {ParentName = "List"};
        }

        public EntityListMatchingStrategy(ParentContext parentContext)
        {
            this.parentContext = parentContext;
        }

        public OrderMatch PositionComparison { get; set; }
        public ItemsMatch ValuesComparison { get; set; }

        public EntityListComparisonResult Compare(IList actual, IList expected)
        {
            var comparisonResult = new EntityListComparisonResult
            {
                EntityMatchResults = new List<EntityMatchResult>(),
                ListMatchResults = new List<MatchResult>()
            };

            CheckCount(actual, expected, comparisonResult);
            if (PositionComparison == OrderMatch.Strict)
                CompareStrictOrder(actual, expected, comparisonResult);
            if (PositionComparison == OrderMatch.IgnoreOrder)
                CompareIgnoreOrder(actual, expected, comparisonResult);
            return comparisonResult;
        }

        private void CompareIgnoreOrder(IList actual, IList expected, EntityListComparisonResult comparisonResult)
        {
            var overallSuccess = true;
            for (var i = 0; i < expected.Count; i++)
            {
                var expectedItem = expected[i];
                var found = false;
                for (var j = 0; j < actual.Count; j++)
                {
                    var actualItem = actual[i];
                    var entityMatcher = new EntityMatchingStrategy(parentContext.WithIndex(i));
                    var entityMatchResults = entityMatcher.Compare(actualItem, expectedItem, expectedItem.GetType());
                    var success = entityMatchResults.All(x => x.Success);
                    //comparisonResult.EntityMatchResults.Add(new EntityMatchResult
                    //{
                    //    Success = success,
                    //    MemberResults = entityMatchResults
                    //});
                    found |= success;
                    if (found)
                        break;
                }
                overallSuccess &= found;
            }
            comparisonResult.Equal = overallSuccess;
        }

        private void CompareStrictOrder(IList actual, IList expected, EntityListComparisonResult comparisonResult)
        {
            var overallSuccess = true;
            for (var i = 0; i < expected.Count; i++)
            {
                var expectedItem = expected[i];
                object actualItem = null;
                if (i < actual.Count)
                    actualItem = actual[i];
                var entityMatcher = new EntityMatchingStrategy(parentContext.WithIndex(i));
                var entityMatchResults = entityMatcher.Compare(actualItem, expectedItem, expectedItem.GetType());
                var success = entityMatchResults.All(x => x.Success);
                comparisonResult.EntityMatchResults.Add(new EntityMatchResult
                {
                    Success = success,
                    MemberResults = entityMatchResults
                });
                overallSuccess &= success;
            }
            comparisonResult.Equal = overallSuccess;
        }

        private void CheckCount(IList actual, IList expected, EntityListComparisonResult comparisonResult)
        {
            if (ValuesComparison == ItemsMatch.Strict && expected.Count != actual.Count)
                comparisonResult.ListMatchResults.Add(new MatchResult
                {
                    Success = false,
                    Expected = expected.Count,
                    Actual = actual.Count,
                    Parent = parentContext.WithProprety("Count")
                });
            if (ValuesComparison == ItemsMatch.Contains && expected.Count > actual.Count)
                comparisonResult.ListMatchResults.Add(new MatchResult
                {
                    Success = false,
                    Expected = $"Greater or equal to {expected.Count}",
                    Actual = actual.Count,
                    Parent = parentContext.WithProprety("Count")
                });
        }
    }
}