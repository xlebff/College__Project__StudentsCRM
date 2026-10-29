namespace StudentsCRM.Api.Models
{
    public sealed class Student
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string FullName { get; init; } = "";
        public DateOnly BirthDate { get; init; }
        public int AdmissionYear { get; init; }
        public int Course { get; init; }
        public string Group { get; init; } = "";

        public SortedDictionary<int, List<int>> GradesByYear { get; } = [];

        public static IComparer<Student> ByNameThenBirthDate { get; } =
            Comparer<Student>.Create(static (a, b) =>
            {
                var c = string.Compare(a.FullName, b.FullName,
                    StringComparison.CurrentCultureIgnoreCase);
                if (c != 0) return c;

                c = a.BirthDate.CompareTo(b.BirthDate);
                if (c != 0) return c;

                c = a.AdmissionYear.CompareTo(b.AdmissionYear);
                if (c != 0) return c;

                return a.Id.CompareTo(b.Id);
            });
    }
}
