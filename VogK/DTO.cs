

using VogK;

internal class DTO
{
    string projectFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

    string filePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "users.txt");

    public const int Fem = 5;


    public string ReadName(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }

    public Gender ReadGender(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "M")
                return Gender.M;

            if (input == "F")
                return Gender.F;

            Console.WriteLine("Ugyldigt input. Skriv M eller F.");
        }
    }
    public int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Ugyldigt tal");
        }
    }

    public bool ReadBool(string prompt)
    {
        while (true)
        {
            Console.Write(prompt + " (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool value))
                return value;

            Console.WriteLine("Ugyldigt input");
        }
    }

    public List<User> LoadUsers()
    {
        if (!File.Exists(filePath))
            return new List<User>();

        return File.ReadAllLines(filePath)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(line =>
            {
                var p = line.Split('|');

                if (p.Length != 5) return null;
                if (!Enum.TryParse<Gender>(p[2], out var gender))
                    return null;
                if (!int.TryParse(p[3], out int years)) return null;
                if (!bool.TryParse(p[4], out bool reminder)) return null;

                return new User
                {
                    Name = p[0],
                    Surname = p[1],
                    Gender = p[2],
                    Age = new Age(int.Parse(p[3])),
                    YearsUntilRetirement = years,
                    Reminder = reminder
                };
            })
            .Where(u => u != null)
            .ToList()!;
    }

    public DateOnly ReadBirthDate()
    {
        int day = ReadInt("Indtast fødselsdag (1-31): ");
        int month = ReadInt("Indtast fødselsmåned (1-12): ");
        int year = ReadInt("Indtast fødselsår: ");

        return new DateOnly(year, month, day);


    }
    public Return AgeCalc(DateOnly birthDay)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        Age age = new(today.Year - birthDay.Year);
        if (today < birthDay.AddYears(age.age))
            age = new Age(age.age - 1);

        string jsonFile = File.ReadAllText("AppSettings.json");
        Root data = JsonSerializer.Deserialize<Root>(jsonFile);

        Age retirementAge = new Age(data.RetirementAge.Age.age);

        YearsUntilRetirement yearsUntilRetirement = new(retirementAge.age - age.age);
        bool reminder = false;
        if (yearsUntilRetirement.yearsUntilRetirement <= 5)
        {
            reminder = true;
        }
        return new Return
        {
            Age = age,
            YearsUntilRetirement = yearsUntilRetirement,
            Reminder = reminder
        };
    }

    public void CreateUser(string name, string surname, Gender gender, Return age)
    {

        
        var users = LoadUsers();

        var newUser = new User
        {
            Name = name,
            Surname = surname,
            Gender = gender.ToString(),
            Age = age.Age,

            Reminder = age.Reminder
        };


        users.Add(newUser);

        File.WriteAllLines(filePath,
            users.Select(u =>
                $"{u.Name}|{u.Surname}|{u.Gender}|{u.Age.age}|{u.Reminder}"));

        Console.WriteLine("Bruger gemt!");
    }

    public void ShowUsers()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Ingen brugere fundet.");
            return;
        }

        var lines = File.ReadAllLines(filePath);

        Console.WriteLine("\n=== BRUGERE ===");

        foreach (var line in lines)
        {
            var p = line.Split('|');

            if (p.Length < 5)
                continue;

            Console.WriteLine($"{p[0]} {p[1]}, {p[2]}, {p[3]} år, Påmindelse: {p[4]}");
        }
    }

    public void ShowSortedBySurname()
    {
        var users = LoadUsers();

        var sortedUsers = users
            .OrderBy(u => u.Surname)
            .ToList();

        Console.WriteLine("\n=== SORTERET ===");

        foreach (var u in sortedUsers)
        {
            Console.WriteLine($"{u.Surname}, {u.Name}");
        }
    }
    public void ShowNearRetirement()
    {
        var users = LoadUsers();

        var result = users
            .Where(u => u.Reminder == true)
            .OrderBy(u => u.Surname)
            .ToList();

        Console.WriteLine("\n=== TÆT PÅ PENSION (<= 5 ÅR) ===");

        foreach (var u in result)
        {
            Console.WriteLine($"{u.Surname}, {u.Name} - {u.YearsUntilRetirement} år tilbage");
        }
    }
    public void CreateUserFlow(DTO dto)
    {
        string name = dto.ReadName("Indtast dit fornavn: ");
        string surname = dto.ReadName("Indtast dit efternavn: ");
        Gender gender = dto.ReadGender("Indtast dit køn (M/F): ");

        DateOnly birthDate = dto.ReadBirthDate();
        Return age = dto.AgeCalc(birthDate);

        dto.CreateUser(name, surname, gender, age);

        Console.WriteLine("Bruger oprettet!");
    }
}
