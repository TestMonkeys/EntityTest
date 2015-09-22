using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMonkeys.EntityTest.PropertyAttributes;

namespace AliasTestPack
{
    public class Class1
    {
        [ValidateActualNotNull]
        public object TestPack2NotNull { get; set; }
    }
}
