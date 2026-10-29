using StudentsCRM.Api.Commands;
using StudentsCRM.Api.Models;

namespace StudentsCRM.Api.CommandArgs
{
    internal record class AddStudentsCommandArgs(List<ICustomCommand> activeCommands) : BaseCustomCommandArgs(activeCommands)
    {
        public SortedDictionary<string, SortedSet<Student>> StudentsByGroup { get; } =
            new(StringComparer.CurrentCultureIgnoreCase);
    }
}
