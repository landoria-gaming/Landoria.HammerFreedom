using System;
using System.Collections.Generic;

namespace Landoria.HammerFreedom
{
    // Provides the local console command for Hammer flight.
    internal static class FlyCommand
    {
        private static Terminal.ConsoleCommand _command;

        // Registers or refreshes the fly console command.
        internal static void Register()
        {
            _command = new Terminal.ConsoleCommand(
                "fly",
                "[on|off] toggles vanilla flight in Hammer worlds.",
                Run,
                optionsFetcher: Options);
        }

        // Returns whether a terminal command is the registered fly command.
        internal static bool IsCommand(Terminal.ConsoleCommand command)
        {
            return ReferenceEquals(command, _command);
        }

        // Applies the requested flight state from console arguments.
        private static object Run(Terminal.ConsoleEventArgs args)
        {
            if (!Mode.IsHammer())
            {
                return "Flight is only available in Hammer worlds.";
            }

            if (args.Length == 1)
            {
                FlyController.Toggle();
                return true;
            }

            string value = args[1].ToLowerInvariant();
            if (value != "on" && value != "off")
            {
                return "Use fly, fly on, or fly off.";
            }

            FlyController.SetEnabled(value == "on");
            return true;
        }

        // Returns the supported command arguments.
        private static List<string> Options()
        {
            return new List<string> { "on", "off" };
        }
    }
}
