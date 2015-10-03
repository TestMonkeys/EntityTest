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

using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace TestMonkeys.EntityTest.Engine.Constraints
{
    public class CustomConstraintResult : ConstraintResult
    {
        private readonly IConstraint constraint;

        public CustomConstraintResult(IConstraint constraint, object actualValue) : base(constraint, actualValue)
        {
            this.constraint = constraint;
        }

        public CustomConstraintResult(IConstraint constraint, object actualValue, ConstraintStatus status)
            : base(constraint, actualValue, status)
        {
            this.constraint = constraint;
        }

        public CustomConstraintResult(IConstraint constraint, object actualValue, bool isSuccess)
            : base(constraint, actualValue, isSuccess)
        {
            this.constraint = constraint;
        }

        public List<string> MessageList { get; set; }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            //base.WriteActualValueTo(writer);
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            writer.WriteMessageLine(constraint.Description);
            foreach (var message in MessageList)
                writer.WriteMessageLine(3, message);
        }
    }
}