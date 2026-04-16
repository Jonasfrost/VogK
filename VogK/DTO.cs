using System;
using System.Collections.Generic;
using System.Text;

namespace VogK
{
    internal class DTO
    {
        public const int RetirementAge = 71;

        public object AgeCalc(DateOnly birthDay)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - birthDay.Year;
            if (today < birthDay.AddYears(age))
                age--;

            int yearsUntilRetirement = RetirementAge - age;
            bool reminder = yearsUntilRetirement <= 5;

            return new
            {
                Age = age,
                YearsUntilRetirement = yearsUntilRetirement,
                Reminder = reminder
            };
        }
    }
}
