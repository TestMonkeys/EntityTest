using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkey.EntityTest.Engine.PropertyRuleSet
{
    public class ParentContext
    {
        public string ParentName { get; set; }
        public int? Index { get; set; }
        private ParentContext parent;

        public ParentContext() { }

        public ParentContext(ParentContext parent)
        {
            this.parent = parent;
        }

        public ParentContext WithProprety(string propertyName)
        {
            return new ParentContext
            {
                ParentName = propertyName,
                Index = this.Index,
                parent = this
            };
        }

        public ParentContext WithIndex(int index)
        {
            return new ParentContext
            {
                ParentName = this.ParentName,
                Index = index
            };
        }

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder();
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
