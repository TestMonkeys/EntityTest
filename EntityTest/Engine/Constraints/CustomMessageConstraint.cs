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
using TestMonkey.Assertion.Constraints;

namespace TestMonkey.EntityTest.Engine.Constraints
{
#if NUnit
    public abstract class CustomMessageConstraint : NUnit.Framework.Constraints.Constraint
#else
    public abstract class CustomMessageConstraint : Constraint
#endif
    {
        protected readonly StringBuilder messageBuilder;

        protected CustomMessageConstraint()
        {
            messageBuilder = new StringBuilder();
        }

        protected abstract string DescriptionLine { get; }

#if NUnit
        public override void WriteDescriptionTo(NUnit.Framework.Constraints.MessageWriter writer)
        {
            writer.WriteMessageLine(DescriptionLine);
        }

        public override void WriteMessageTo(NUnit.Framework.Constraints.MessageWriter writer)
        {
            WriteDescriptionTo(writer);
            writer.WriteMessageLine(3, messageBuilder.ToString());
        }
#else
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteMessageLine(DescriptionLine);
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            WriteDescriptionTo(writer);
            writer.WriteMessageLine(0, messageBuilder.ToString());
        }
#endif
    }
}