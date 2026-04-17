using System;

namespace VogK
{
    internal class Program
    {
        public void Run(string[] args)
        {
            var dto = new DTO();

            int day = dto.ReadInt("Indtast din fødselsdag (dag): ");
            int month = dto.ReadInt("Indtast din fødselsmåned (måned): ");
            int year = dto.ReadInt("Indtast dit fødselsår (år): ");

            try
            {
                DateOnly birthDay = new DateOnly(year, month, day);
                Console.WriteLine($"Fødselsdato: {birthDay:yyyy-MM-dd}");

                Return result = dto.AgeCalc(birthDay);

                Console.WriteLine($"Du er {result.Age} år gammel.");
                Console.WriteLine($"Du har {result.YearsUntilRetirement} år til pension.");
                if (result.Reminder)
                {
                    Console.WriteLine("Husk at tjekke din pensionsopsparing.");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Ugyldig dato.");
            }
        }

    }
}
