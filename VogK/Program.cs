using System;

namespace VogK
{
    internal class Program
    {
        public static void Main()
        {
            new Program().Run();
        }

        public void Run()
        {
            var dto = new DTO();

            while (true)
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("1. Opret bruger");
                Console.WriteLine("2. Vis brugere");
                Console.WriteLine("3. Exit");
                Console.WriteLine("4. Sorter efter efternavn");
                Console.WriteLine("5. Vis medarbejdere nær pension");
                Console.Write("Vælg: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateUserFlow(dto);
                        break;

                    case "2":
                        dto.ShowUsers();
                        break;

                    case "3":
                        return;

                    case "4":
                        dto.ShowSortedBySurname();
                        break;

                    case "5":
                        var gen = new EmployeeRecordGenerator();
                        var users = dto.LoadUsers();


                        var result = gen.GenerateNearRetirement(users);
                        Console.WriteLine("\n=== MEDARBEJDERE NÆR PENSION ===");
                        dto.ShowSortedBySurname();

                        foreach (var e in result)
                        {
                            Console.WriteLine($"{e.Name} {e.Surname} - {e.YearsUntilRetirement} år tilbage");
                        }
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        break;
                }
            }
        }

        private void CreateUserFlow(DTO dto)
        {
            string name = dto.ReadName("Indtast dit fornavn: ");
            string surname = dto.ReadName("Indtast dit efternavn: ");
            string gender = dto.ReadGender("Indtast dit køn (M/F): ");

            DateOnly birthDate = dto.ReadBirthDate();

            dto.CreateUser(name, surname, gender, birthDate);

            Console.WriteLine("Bruger oprettet!");
        }
    }
}