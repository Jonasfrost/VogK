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
                        dto.CreateUserFlow(dto);
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
                        dto.ShowNearRetirement();
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        break;
                }
            }
        }
    }
}