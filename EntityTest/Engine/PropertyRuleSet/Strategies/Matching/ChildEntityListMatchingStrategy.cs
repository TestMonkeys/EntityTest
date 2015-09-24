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
using System.Reflection;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies.Matching
{
    internal class ChildEntityListMatchingStrategy : PropertyMatchingStrategy
    {
        public override List<MatchResult> Validate(PropertyInfo actualProperty, object actualObj, object expectedObj,
            PropertyInfo expectedProperty = null,
            ParentContext parentContext = null)
        {
            var expectedValue = GetPropertyValue(actualProperty, expectedObj);
            var actualValue = GetPropertyValue(actualProperty, actualObj);

            var listMatcher = new EntityListMatchingStrategy(parentContext);
            var entityListResult = listMatcher.Compare((IList) actualValue, (IList) expectedValue);
            var resultList = entityListResult.ListMatchResults;
            resultList.AddRange(entityListResult.EntityMatchResults.SelectMany(x => x.MemberResults).ToList());
            return resultList;
        }
    }
}