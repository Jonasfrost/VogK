namespace VogK
{
    internal class User : IEquatable<User>, IComparable<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool Reminder { get; set; }
        public int YearsUntilRetirement { get; set; }

        public bool Equals(User? other)
        {
            if (other is null) return false;

            return Name == other.Name &&
                   Surname == other.Surname &&
                   Gender == other.Gender &&
                   BirthDate == other.BirthDate;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Gender, BirthDate);
        }

        public int CompareTo(User? other)
        {
            if (other == null) return 1;

            return string.Compare(Surname, other.Surname);
        }
    }
}