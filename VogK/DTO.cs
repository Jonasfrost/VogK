using System;
using System.Text.Json;


var app = new VogK.Program();
app.Run(args);

internal class DTO
{

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
        
        return File.ReadAllLines(filePath)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(line =>
            {
                var p = line.Split('|');

                if (p.Length != 6) return null;

                if (!Enum.TryParse<Gender>(p[2], out var gender))
                    return null;

                if (!int.TryParse(p[3], out int years)) return null;
                if (!bool.TryParse(p[4], out bool reminder)) return null;
                if (!Enum.TryParse<Department>(p[5], out var department)) return null;


                return new User
                {
                    Name = p[0],
                    Surname = p[1],
                    Gender = p[2],
                    Age = new Age(years),
                    YearsUntilRetirement = years,
                    Reminder = reminder,
                    Department = department
                };
            })
            .Where(u => u != null)
            .ToList()!;
    }

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

    public void CreateUser(string name, string surname, Gender gender, Return age, Department department)
    {

        public int ReadInt(string prompt)
        {
            Name = name,
            Surname = surname,
            Gender = gender.ToString(),
            Age = age.Age,
            Department = department,
            Reminder = age.Reminder
        };


        users.Add(newUser);

        File.WriteAllLines(filePath,
    users.Select(u =>
        $"{u.Name}|{u.Surname}|{u.Gender}|{u.YearsUntilRetirement}|{u.Reminder}|{u.Department}"));

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

            Console.WriteLine($"{p[0]} {p[1]}, {p[2]}, {p[3]} år, {p[5].Replace("_", " ")}, Reminder: {p[4]}");
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

        double[,] salaryData =
        {
        { 35000, 40000, 80000, 43750 },
        { 30000, 34000, 68000, 37500 },
        { 28000, 32000, 64000, 35000 }
    };

        var result = users
            .Where(u => u.Reminder)
            .OrderBy(u => u.Surname)
            .ToList();

        Console.WriteLine("\n=== TÆT PÅ PENSION (<= 5 ÅR) ===");

        foreach (var u in result)
        {
            int row = (int)u.Department;

            double bonus;

            if (u.Gender == "M")
                bonus = salaryData[row, 2]; // mand bonus
            else
                bonus = salaryData[row, 3]; // kvinde bonus

            Console.WriteLine($"{u.Surname}, {u.Name} - {u.YearsUntilRetirement} år tilbage - Bonus: {bonus}");
        }
    }
    public void CreateUserFlow(DTO dto)
    {
        string name = dto.ReadName("Indtast dit fornavn: ");
        string surname = dto.ReadName("Indtast dit efternavn: ");
        Gender gender = dto.ReadGender("Indtast dit køn (M/F): ");
        Department department = dto.ReadDepartment("Vælg afdeling:");

        DateOnly birthDate = dto.ReadBirthDate();
        Return age = dto.AgeCalc(birthDate);

        dto.CreateUser(name, surname, gender, age, department);

        Console.WriteLine("Bruger oprettet!");
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
}
