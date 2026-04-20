using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace VogK
{
    internal class DTO
    {
        private const string FilePath = "users.txt";

        public string ReadName(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        public string ReadGender(string prompt)
        {
            Console.Write(prompt);
            return (Console.ReadLine() ?? "").ToUpper();
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
            if (!File.Exists(FilePath))
                return new List<User>();

            return File.ReadAllLines(FilePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(line =>
                {
                    var p = line.Split('|');

                    if (p.Length != 5) return null;

                    if (!int.TryParse(p[3], out int years)) return null;
                    if (!bool.TryParse(p[4], out bool reminder)) return null;

                    return new User
                    {
                        Name = p[0],
                        Surname = p[1],
                        Gender = p[2],
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

        public void CreateUser(string name, string surname, string gender, DateOnly birthDate)
        {
            var users = LoadUsers();

            int retirementAge = 67;

            int age = DateTime.Today.Year - birthDate.Year;
            if (DateOnly.FromDateTime(DateTime.Today) < birthDate.AddYears(age)) age--;

            int yearsUntilRetirement = retirementAge - age;

            bool reminder = yearsUntilRetirement <= 5;

            var newUser = new User
            {
                Name = name,
                Surname = surname,
                Gender = gender,
                BirthDate = birthDate,
                Reminder = reminder
            };

            if (users.Any(u => u.Name == newUser.Name && u.Surname == newUser.Surname))
            {
                Console.WriteLine("Bruger findes allerede!");
                return;
            }

            users.Add(newUser);

            File.WriteAllLines(FilePath,
                users.Select(u =>
                    $"{u.Name}|{u.Surname}|{u.Gender}|{u.BirthDate}|{u.Reminder}"));

            Console.WriteLine("Bruger gemt!");
        }

        public void ShowUsers()
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Ingen brugere fundet.");
                return;
            }

            var lines = File.ReadAllLines(FilePath);

            Console.WriteLine("\n=== BRUGERE ===");

            foreach (var line in lines)
            {
                var p = line.Split('|');

                if (p.Length < 5)
                    continue;

                Console.WriteLine($"{p[0]} {p[1]} ({p[2]}) - Født: {p[3]} - Reminder: {p[4]}");
            }
        }

        public void ShowSortedBySurname()
        {
            var users = LoadUsers();

            users.Sort(); // bruger CompareTo()

            Console.WriteLine("\n=== SORTERET ===");

            foreach (var u in users)
            {
                Console.WriteLine($"{u.Surname}, {u.Name}");
            }
        }
    }
}