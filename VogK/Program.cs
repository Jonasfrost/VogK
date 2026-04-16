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
                var result = dto.AgeCalc(birthDay);

                dynamic r = result;
                Console.WriteLine($"Du er {r.Age} år gammel.");
                Console.WriteLine($"Der er {r.YearsUntilRetirement} år til pension.");
                if (r.Reminder)
                {
                    Console.WriteLine("Husk at tjekke din pensionsopsparing.");
                }
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

//VogK.Program program = new();

//VogK.Program app = program;
//app.Run(args);