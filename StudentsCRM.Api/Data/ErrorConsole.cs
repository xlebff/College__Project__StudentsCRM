namespace StudentsCRM.Api.Data
{
    internal static class ErrorConsole
    {
        public static void Write(string message) =>
            WriteInRed(() => Console.Write(message));

        public static void WriteLine(string message) =>
            WriteInRed(() => Console.WriteLine(message));

        private static void WriteInRed(Action write)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            try
            {
                write();
            }
            finally
            {
                Console.ForegroundColor = previousColor;
            }
        }
    }
}