using StudentsCRM.Api.CommandArgs;

namespace StudentsCRM.Api.Commands
{
    internal abstract class BaseCustomCommand<TArgs>(string name, string description)
    : ICustomCommand<TArgs>
    where TArgs : ICustomCommandArgs
    {
        public string Name { get; } = name;
        public string Description { get; } = description;

        public virtual void Register(TArgs args)
        {
            if (!args.ActiveCommands.Contains(this))
            {
                args.ActiveCommands.Add(this);
            }
        }

        public abstract void Execute(TArgs args);
    }
}
