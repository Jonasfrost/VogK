using System;
using System.Collections.Generic;
using System.Text;

namespace VogK
{
    internal readonly struct YearsUntilRetirement
    {
        public int yearsUntilRetirement { get; }

        public YearsUntilRetirement(int value)
        {
            yearsUntilRetirement = value;
        }
    }
}
