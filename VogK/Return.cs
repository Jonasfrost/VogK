

internal class Return
{
    public Age Age { get; init; }
    public YearsUntilRetirement YearsUntilRetirement { get; set; }
    public bool Reminder { get; init; }

}
internal class Retirement
{
    public Age Age { get; set; }
}

internal class Root
{
    public Retirement RetirementAge { get; set; }
}


