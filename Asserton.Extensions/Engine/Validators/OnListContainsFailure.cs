using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMonkey.Assertion.Extensions.Engine.Validators
{
    public enum OnListContainsFailure
    {
        DoNothing,
        DisplayExpectedAndActualList,
        DisplayClosestMatch
    }
}
