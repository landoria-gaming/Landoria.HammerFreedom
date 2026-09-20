using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Hides the fly command outside Hammer worlds.
    [HarmonyPatch(typeof(Terminal.ConsoleCommand), nameof(Terminal.ConsoleCommand.IsValid))]
    internal static class FlyCommandValidationPatch
    {
        // Marks the fly command invalid when Hammer mode is inactive.
        private static void Postfix(Terminal.ConsoleCommand __instance, ref bool __result)
        {
            if (FlyCommand.IsCommand(__instance) && !Mode.IsHammer())
            {
                __result = false;
            }
        }
    }
}
