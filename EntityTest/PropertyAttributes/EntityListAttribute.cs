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

using System;
using TestMonkey.EntityTest.Engine.Validators;

namespace TestMonkey.EntityTest.PropertyAttributes
{
    /// <summary>
    ///     Will run a full properties validation for decorated List
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityListAttribute : Attribute
    {
        public ListPositionComparisonStrategy PositionMatching { get; }
        public ListValuesComparisonStrategy ValuesMatching { get; }

        public EntityListAttribute(ListPositionComparisonStrategy positionMatching,
            ListValuesComparisonStrategy valuesMatching)
        {
            this.PositionMatching = positionMatching;
            this.ValuesMatching = valuesMatching;
        }

        public EntityListAttribute()
        {
            PositionMatching = ListPositionComparisonStrategy.Strict;
            ValuesMatching = ListValuesComparisonStrategy.Strict;
        }
    }
}