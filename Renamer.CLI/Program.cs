using System;
using System.Linq;
using Fclp;
using Fclp.Internals.Extensions;
using Kenbo.Core.Events;
using Kenbo.Renamer.CLI.Events;
using Kenbo.Renamer.CLI.Renamers;

namespace Kenbo.Renamer.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupEvents();
            var parser = Setup(args);
            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                Console.WriteLine("Errors occured:");
                result.Errors.Select(x => x.Option.Description).ForEach(Console.WriteLine);
                return;
            }

            if (result.HelpCalled)
            {
                return;
            }

            var arguements = parser.Object;
            var renamer = RenamerFactory.Create(arguements.Action, arguements.Format);
            renamer.Rename(arguements.Directory);
        }

        private static void SetupEvents()
        {
            Event.Register<FileRenamed>(Console.WriteLine);
        }

        private static FluentCommandLineParser<CommandLineArguements> Setup(string[] args)
        {
            var parser = new FluentCommandLineParser<CommandLineArguements> {IsCaseSensitive = false};
            parser.Setup(x => x.Directory).As('d', "dir").Required();
            parser.Setup(x => x.Format).As('f', "format").Required();
            parser.Setup(x => x.Action).As('a', "action").SetDefault(RenamerAction.Show);
            parser.SetupHelp("h", "help").Callback(text => Console.WriteLine(text));

            return parser;
        }
    }

    internal class CommandLineArguements
    {
        public RenamerAction Action { get; set; }

        public string Directory { get; set; }

        public string Format { get; set; }
    }

    internal enum RenamerAction
    {
        Show = 1
    }
}
