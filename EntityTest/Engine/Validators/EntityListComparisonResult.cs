using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkey.EntityTest.Engine.Validators
{
    public class EntityListComparisonResult
    {
        public bool Equal { get; set; }
        public List<MatchResult> ListMatchResults;
        public List<EntityMatchResult> EntityMatchResults;

    }
}
