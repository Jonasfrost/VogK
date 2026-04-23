internal class DTO
{
    string filePath = Path.Combine(
        Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
        "users.txt");

    public const int Fem = 5;

    // -------- INPUT --------

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

            if (input == "M") return Gender.M;
            if (input == "F") return Gender.F;

            Console.WriteLine("Ugyldigt input.");
        }
    }

    public Department ReadDepartment(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("1: Software Development");
            Console.WriteLine("2: Administration");
            Console.WriteLine("3: Service and Support");

            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1": return Department.Software_Development;
                case "2": return Department.Administration;
                case "3": return Department.Service_and_Support;
            }

            Console.WriteLine("Ugyldigt valg.");
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

    public DateOnly ReadBirthDate()
    {
        int day = ReadInt("Dag: ");
        int month = ReadInt("Måned: ");
        int year = ReadInt("År: ");

        return new DateOnly(year, month, day);
    }

    // -------- LOGIK --------

    public Return AgeCalc(DateOnly birthDay)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        int ageVal = today.Year - birthDay.Year;

        if (today < birthDay.AddYears(ageVal))
            ageVal--;

        Age age = new(ageVal);

        int retirementAge = 67;
        int yearsLeft = retirementAge - age.age;

        bool reminder = yearsLeft <= Fem;

        return new Return
        {
            Age = age,
            YearsUntilRetirement = new YearsUntilRetirement(yearsLeft),
            Reminder = reminder
        };
    }

    // -------- FIL --------

    public List<User> LoadUsers()
    {
        if (!File.Exists(filePath))
            return new List<User>();

        return File.ReadAllLines(filePath)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(line =>
            {
                var p = line.Split('|');

                if (p.Length != 6) return null;

                if (!int.TryParse(p[3], out int years)) return null;
                if (!bool.TryParse(p[4], out bool reminder)) return null;
                if (!Enum.TryParse<Department>(p[5], out var dep)) return null;

                return new User
                {
                    Name = p[0],
                    Surname = p[1],
                    Gender = p[2],
                    Age = new Age(years),
                    YearsUntilRetirement = years,
                    Reminder = reminder,
                    Department = dep
                };
            })
            .Where(u => u != null)
            .ToList()!;
    }

    public void SaveUsers(List<User> users)
    {
        File.WriteAllLines(filePath,
            users.Select(u =>
                $"{u.Name}|{u.Surname}|{u.Gender}|{u.YearsUntilRetirement}|{u.Reminder}|{u.Department}"));
    }

    // -------- CREATE --------

    public void CreateUserFlow()
    {
        string name = ReadName("Fornavn: ");
        string surname = ReadName("Efternavn: ");
        Gender gender = ReadGender("Køn (M/F): ");
        Department dep = ReadDepartment("Vælg afdeling:");

        DateOnly birth = ReadBirthDate();
        Return r = AgeCalc(birth);

        var users = LoadUsers();

        users.Add(new User
        {
            Name = name,
            Surname = surname,
            Gender = gender.ToString(),
            Age = r.Age,
            YearsUntilRetirement = r.YearsUntilRetirement.yearsUntilRetirement,
            Reminder = r.Reminder,
            Department = dep
        });

        SaveUsers(users);

        Console.WriteLine("Bruger oprettet!");
    }

    // -------- VIS --------

    public void ShowUsers()
    {
        var users = LoadUsers();

        Console.WriteLine("\n=== BRUGERE ===");

        foreach (var u in users)
        {
            Console.WriteLine($"{u.Name} {u.Surname}, {u.Gender}, {u.Age.age} år, {u.Department.ToString().Replace("_", " ")}, Reminder: {u.Reminder}");
        }
    }

    public void ShowNearRetirement()
    {
        var users = LoadUsers();

        double[,] salary =
        {
            {35000, 40000, 80000, 43750},
            {30000, 34000, 68000, 37500},
            {28000, 32000, 64000, 35000}
        };

        var result = users.Where(u => u.Reminder).ToList();

        Console.WriteLine("\n=== TÆT PÅ PENSION ===");

        foreach (var u in result)
        {
            int row = (int)u.Department;

            double bonus = u.Gender == "M"
                ? salary[row, 2]
                : salary[row, 3];

            Console.WriteLine($"{u.Surname}, {u.Name} - {u.Department.ToString().Replace("_", " ")} - Bonus: {bonus}");
        }
    }
    public void ShowByUserName()
    {
        var users = LoadUsers();

        Console.Write("Indtast fornavn: ");
        string input = (Console.ReadLine() ?? "").ToLower();

        var result = users
            .Where(u => u.Name.ToLower().Contains(input))
            .ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("Ingen brugere fundet.");
            return;
        }

        Console.WriteLine("\n=== RESULTAT ===");

        foreach (var u in result)
        {
            Console.WriteLine($"{u.Name} {u.Surname}, {u.Gender}, {u.Age.age} år, {u.Department.ToString().Replace("_", " ")}");
        }
    }
    public void ShowBySurname()
    {
        var users = LoadUsers();

        var sorted = users
            .OrderBy(u => u.Surname)
            .ThenBy(u => u.Name) // bonus: hvis samme efternavn
            .ToList();

        Console.WriteLine("\n=== SORTERET EFTER EFTERNAVN ===");

        foreach (var u in sorted)
        {
            Console.WriteLine($"{u.Surname}, {u.Name} - {u.Department.ToString().Replace("_", " ")}");
        }
    }
}
