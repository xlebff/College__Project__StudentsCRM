using StudentsCRM.Api.Commands;

namespace StudentsCRM.Api.CommandArgs
{
    internal record class BaseCustomCommandArgs(List<ICustomCommand> activeCommands) : ICustomCommandArgs
    {
        public List<ICustomCommand> ActiveCommands { get; } = activeCommands;
    }
}
