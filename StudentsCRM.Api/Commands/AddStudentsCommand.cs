using StudentsCRM.Api.CommandArgs;
using StudentsCRM.Api.Data;
using StudentsCRM.Api.Models;
using System.Globalization;

namespace StudentsCRM.Api.Commands
{
    internal class AddStudentsCommand(string name, string description)
        : BaseCustomCommand<AddStudentsCommandArgs>(name, description)
    {
        public override void Execute(AddStudentsCommandArgs args)
        {
            Console.WriteLine(
                "\n" +
                "Empty group — finish input.\n" +
                "Empty full name — go to the next group.\n");

            while (true)
            {
                Console.Write("Group (empty — finish): ");
                var group = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(group))
                    break;

                if (!args.StudentsByGroup.TryGetValue(group, out var students))
                {
                    students = new SortedSet<Student>(Student.ByNameThenBirthDate);
                    args.StudentsByGroup[group] = students;
                }

                while (true)
                {
                    Console.Write("\n\tStudent full name (empty — next group): ");
                    var fullName = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(fullName))
                        break;

                    var birthDate = ReadDate("\tBirth date (dd.MM.yyyy): ");
                    var admissionYear = ReadAdmissionYear(birthDate);
                    var course = ReadInt("\tCourse: ", 1, 6);

                    var student = new Student
                    {
                        FullName = fullName,
                        BirthDate = birthDate,
                        AdmissionYear = admissionYear,
                        Course = course,
                        Group = group
                    };

                    for (var year = 1; year <= course; year++)
                    {
                        Console.Write(
                            $"\tGrades for year {year} (numbers 2–5 separated by spaces, empty — none): ");
                        student.GradesByYear[year] = ReadGrades();
                    }

                    if (!students.Add(student))
                    {
                        ErrorConsole.WriteLine(
                            "\tSuch a student already exists in the group — record not added.");
                    }
                }
            }

            static DateOnly ReadDate(string prompt)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var minBirthDate = today.AddYears(-14);

                while (true)
                {
                    Console.Write(prompt);
                    var input = Console.ReadLine();

                    if (DateOnly.TryParseExact(
                            input,
                            "dd.MM.yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var date)
                        && date <= minBirthDate)
                    {
                        return date;
                    }

                    ErrorConsole.WriteLine(
                        "\tEnter a valid birth date in dd.MM.yyyy format; the student must be at least 14 years old.");
                }
            }

            static int ReadAdmissionYear(DateOnly birthDate)
            {
                while (true)
                {
                    var admissionYear = ReadInt(
                        "\tAdmission year: ",
                        1900,
                        DateTime.Today.Year);

                    if (admissionYear >= birthDate.Year)
                        return admissionYear;

                    ErrorConsole.WriteLine(
                        "\tAdmission year cannot be earlier than the birth year.");
                }
            }

            static int ReadInt(string prompt, int min, int max)
            {
                while (true)
                {
                    Console.Write(prompt);
                    var input = Console.ReadLine();

                    if (int.TryParse(input, out var value) && value >= min && value <= max)
                        return value;

                    ErrorConsole.WriteLine($"\tEnter an integer from {min} to {max}.");
                }
            }

            static List<int> ReadGrades()
            {
                while (true)
                {
                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                        return new List<int>();

                    var parts = input.Split(
                        ' ',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    var grades = new List<int>(parts.Length);
                    var ok = true;

                    foreach (var part in parts)
                    {
                        if (int.TryParse(part, out var grade) && grade is >= 2 and <= 5)
                        {
                            grades.Add(grade);
                        }
                        else
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok)
                        return grades;

                    ErrorConsole.Write(
                        "\tGrades must be numbers 2–5 separated by spaces. Try again: ");
                }
            }
        }
    }
}