using StudentsCRM.Api.CommandArgs;

namespace StudentsCRM.Api.Commands
{
    internal class HelpCommand(string name, string description) : BaseCustomCommand<HelpCommandArgs>(name, description)
    {
        public override void Execute(HelpCommandArgs args)
        {
            Console.WriteLine("Available commands: ");

            foreach (var command in args.ActiveCommands)
            {
                Console.WriteLine($"\t{command.Name} – {command.Description}");
            }
        }
    }
}
