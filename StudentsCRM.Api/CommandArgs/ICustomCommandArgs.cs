using StudentsCRM.Api.Commands;

namespace StudentsCRM.Api.CommandArgs
{
    internal interface ICustomCommandArgs
    {
        List<ICustomCommand> ActiveCommands { get; }
    }
}
