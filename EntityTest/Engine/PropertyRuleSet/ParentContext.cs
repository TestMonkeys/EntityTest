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

using System.Text;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet
{
    public class ParentContext
    {
        private ParentContext parent;

        public ParentContext()
        {
        }

        public ParentContext(ParentContext parent)
        {
            this.parent = parent;
        }

        public string ParentName { get; set; }
        public int? Index { get; set; }

        public ParentContext WithProprety(string propertyName)
        {
            return new ParentContext
            {
                ParentName = propertyName,
                Index = Index,
                parent = this
            };
        }

        public ParentContext WithIndex(int index)
        {
            return new ParentContext
            {
                ParentName = ParentName,
                Index = index
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (parent != null)
            {
                sb.Append(parent).Append(".");
            }
            sb.Append(ParentName);
            if (Index.HasValue)
                sb.Append("[").Append(Index.Value).Append("]");
            return sb.ToString();
        }
    }
}