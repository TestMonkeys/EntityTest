using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkey.EntityTest.Engine.Validators
{
    public class EntityMatchResult
    {
        public bool Success { get; set; }

        public List<MatchResult> MemberResults { get; set; } 
    }
}
