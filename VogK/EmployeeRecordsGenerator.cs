using System;
using System.Collections.Generic;
using System.Linq;


internal class EmployeeRecordGenerator
{

    public List<EmployeeRecord> Generate(List<User> users, string gender)
    {
        return users
            .Where(u => u.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase))
            .Select(u => ToRecord(u))
            .ToList();
    }

    public List<EmployeeRecord> GenerateNearRetirement(List<User> users)
    {
        return users
            .Select(u => ToRecord(u))
            .Where(r => r.YearsUntilRetirement <= 5)
            .ToList();
    }

    private EmployeeRecord ToRecord(User u)
    {
        int age = DateOnly.FromDateTime(DateTime.Today).Year - u.BirthDate.Year;

        if (DateOnly.FromDateTime(DateTime.Today) < u.BirthDate.AddYears(age))
            age--;



        return new EmployeeRecord
        {
            Name = u.Name,
            Surname = u.Surname,
            Gender = u.Gender,
            Age = u.Age,
            YearsUntilRetirement = u.YearsUntilRetirement,
            Reminder = u.Reminder
        };
    }
}
