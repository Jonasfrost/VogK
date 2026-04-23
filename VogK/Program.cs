internal class Program
{
    static void Main()
    {
        DTO dto = new();

        while (true)
        {
            Console.WriteLine("\n1: Opret bruger");
            Console.WriteLine("2: Vis brugere");
            Console.WriteLine("3: Tæt på pension");
            Console.WriteLine("4: Søg på fornavn");
            Console.WriteLine("5: Sorter efter efternavn");
            Console.WriteLine("0: Exit");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    dto.CreateUserFlow();
                    break;

                case "2":
                    dto.ShowUsers();
                    break;

                case "3":
                    dto.ShowNearRetirement();
                    break;

                case "4":
                    dto.ShowByUserName();
                    break;
                
                case "5":
                    dto.ShowBySurname();
                    break;
                default : Console.WriteLine("Ugyldigt valg, prøv igen.");
                    break;


                case "0":
                    return;
            }
        }
    }
}