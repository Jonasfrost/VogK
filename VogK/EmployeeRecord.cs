namespace VogK
{
    internal record EmployeeRecord
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Gender { get; init; }
        public int YearsUntilRetirement { get; init; }
        public bool Reminder { get; init; }
    }
}
