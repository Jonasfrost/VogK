using System;

namespace VogK
{

    internal class Return
    {
        public int Age { get; init; }
        public int YearsUntilRetirement { get; set; }
        public bool Reminder { get; init; }

      
    }
    public class Retirement
    {
        public int age { get; set; }
    }

    public class Root
    {
        public Retirement RetirementAge { get; set; }
    }


}
