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
using System.Reflection;
using TestMonkeys.EntityTest.Framework;

namespace TestMonkeys.EntityTest.Engine.PropertyRuleSet.Strategies
{
    internal abstract class PropertyStrategy
    {
        protected object GetPropertyValue(PropertyInfo property, object obj)
        {
            object value;
            try
            {
                value = property.GetValue(obj, null);
            }
            catch (Exception e)
            {
                throw new ImproperTypeUsageException(e, "Could not get property {0} from object of type {1}",
                    property.Name, obj.GetType().FullName);
            }

            return value;
        }
    }
}