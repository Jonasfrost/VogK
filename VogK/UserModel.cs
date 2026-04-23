using VogK;

internal class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public Age Age { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool Reminder { get; set; }
    public int YearsUntilRetirement { get; set; }
    public Department Department { get; set; }
}
