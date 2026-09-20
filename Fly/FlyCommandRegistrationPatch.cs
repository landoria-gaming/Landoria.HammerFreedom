using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Registers the fly command whenever the terminal is initialized.
    [HarmonyPatch(typeof(Terminal), "InitTerminal")]
    internal static class FlyCommandRegistrationPatch
    {
        // Adds the fly command after terminal initialization.
        private static void Postfix()
        {
            FlyCommand.Register();
        }
    }
}
