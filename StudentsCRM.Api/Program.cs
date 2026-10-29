using StudentsCRM.Api.CommandArgs;
using StudentsCRM.Api.Commands;

class Program
{
    private static void Main(string[] args)
    {
        List<ICustomCommand> commands = [];
        string? command = string.Empty;


        HelpCommand helpCommand = new("Help", "shows all available actions.");
        helpCommand.Register(new HelpCommandArgs(commands));
        AddStudentsCommand addStudentsCommand = new("Add Students", "allows you to add students and groups.");
        addStudentsCommand.Register(new AddStudentsCommandArgs(commands));

        helpCommand.Execute(new HelpCommandArgs(commands));

        addStudentsCommand.Execute(new AddStudentsCommandArgs(commands));
    }
}

static class ConsoleReader
{
    public static string OptReader(string[] options, in string prompt)
    {
        string? res;
        Console.WriteLine();

        while (true)
        {
            Console.Write(prompt + " ");
            res = Console.ReadLine();

            if (res is not null &&
                options.Contains(res.ToLower()))
            {
                return res;
            }
            else
            {
                Console.WriteLine($"Incorrect command: {res}.\n\n" +
                    $"Use \"help\" to get the list of awailable commands.\n");
            }
        }
    }
}