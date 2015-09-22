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

using TestMonkey.Assertion.Constraints;
using TestMonkeys.EntityTest.Engine.Validators;

namespace TestMonkeys.EntityTest
{
    public class ListOfPropertySetObjectsHelper
    {
        public ListContainsPropertySetConstraint Contains(object expected)
        {
            return new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing);
        }

        public ListContainsPropertySetConstraint Contains(object expected, OnListContainsFailure actionOnFailure)
        {
            return new ListContainsPropertySetConstraint(expected, actionOnFailure);
        }

#if NUnit
        public NUnit.Framework.Constraints.Constraint None(object expected)
        {
            return NUnit.Framework.Constraints.NotConstraint(new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing));
        }
#else
        public Constraint None(object expected)
        {
            return new NotConstraint(new ListContainsPropertySetConstraint(expected, OnListContainsFailure.DoNothing));
        }
#endif
    }
}