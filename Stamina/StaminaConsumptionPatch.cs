using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents local stamina consumption in Hammer worlds.
    [HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
    internal static class StaminaConsumptionPatch
    {
        // Skips stamina use when unlimited stamina is active.
        private static bool Prefix(Player __instance)
        {
            return __instance != Player.m_localPlayer || !Mode.IsHammer();
        }
    }
}
