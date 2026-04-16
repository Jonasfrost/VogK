using System;
using System.Collections.Generic;
using System.Text;

namespace VogK
{
    internal class DTO
    {
        public const int RetirementAge = 71;

        public void AgeCalc(DateOnly birthDay)
        {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                int age = today.Year - birthDay.Year;
    
                if (today < birthDay.AddYears(age))
                    age--;
    
                Console.WriteLine($"Du er {age} år gammel.");
                Console.WriteLine($"Der er {RetirementAge - age} fra pension.");
            if (RetirementAge - age <= 5)
            {
                Console.WriteLine("husk at tjekke din pension opsparing");
            }
            else
            {

            }
                
        }
    }
}
