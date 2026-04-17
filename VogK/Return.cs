using System;

namespace VogK
{
    internal class Return
    {
        public int Age { get; init; }
        public int YearsUntilRetirement { get; init; }
        public bool Reminder { get; init; }

      
    }
    public class Retirement
    {
        public string Age { get; set; }
    }

    public class Root
    {
        public Retirement RetirementAge { get; set; }
    }


}
