using System;
using System.Text.Json;


var app = new VogK.Program();
app.Run(args);

namespace VogK
{
    internal class DTO
    {
        //public const int RetirementAge = 71;
        public const int Fem = 5;


        static string jsonFile = File.ReadAllText("data.json");
        Root data = JsonSerializer.Deserialize<Root>(jsonFile);

        public Return AgeCalc(DateOnly birthDay)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            Age age  = new(today.Year - birthDay.Year);
            if (today < birthDay.AddYears(age.age))
                age = new Age(age.age -1);

            string jsonFile = File.ReadAllText("data.json");
            Root data = JsonSerializer.Deserialize<Root>(jsonFile);

            int retirementAge = int.Parse(data.RetirementAge.Age);

            YearsUntilRetirement yearsUntilRetirement =new (retirementAge - age.age);
            bool reminder = yearsUntilRetirement.yearsUntilRetirement <= Fem;

            return new Return
            {
                Age = age.age,
                YearsUntilRetirement = yearsUntilRetirement.yearsUntilRetirement,
                Reminder = reminder
            };
        }
        

        public int ReadInt(string prompt)
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
