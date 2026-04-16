using System;

namespace VogK
{
    internal class Program
    {
        public void Run(string[] args)
        {
            int day = ReadInt("Indtast din fødselsdag (dag): ");
            int month = ReadInt("Indtast din fødselsmåned (måned): ");
            int year = ReadInt("Indtast dit fødselsår (år): ");

            try
            {
                DateOnly birthDay = new DateOnly(year, month, day);
                Console.WriteLine($"Fødselsdato: {birthDay:yyyy-MM-dd}");

                var dto = new DTO();
                dto.AgeCalc(birthDay);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Ugyldig dato.");
            }
        }

        private int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                    return value;
                Console.WriteLine("Ugyldigt tal, prøv igen.");
            }
        }
    }
}

var app = new VogK.Program();
app.Run(args);