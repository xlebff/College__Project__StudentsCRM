using StudentsCRM.Api.Commands;

namespace StudentsCRM.Api.CommandArgs
{
    internal record class HelpCommandArgs(List<ICustomCommand> activeCommands)
        : BaseCustomCommandArgs(activeCommands)
    {
    }
}
