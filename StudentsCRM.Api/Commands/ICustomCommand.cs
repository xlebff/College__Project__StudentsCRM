using StudentsCRM.Api.CommandArgs;

namespace StudentsCRM.Api.Commands
{
    internal interface ICustomCommand
    {
        string Name { get; }
        string Description { get; }
    }

    internal interface ICustomCommand<in TArgs> : ICustomCommand
                where TArgs : ICustomCommandArgs
    {
        void Register(TArgs args);
        void Execute(TArgs args);
    }
}
