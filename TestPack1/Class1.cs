using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace AliasTestPack
{
    public class Class1
    {
        [ValidateActualNotNull]
        public object TestPack1NotNull { get; set; }
    }
}
