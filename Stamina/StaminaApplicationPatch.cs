using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents remote stamina application for the local player in Hammer worlds.
    [HarmonyPatch(typeof(Player), "RPC_UseStamina")]
    internal static class StaminaApplicationPatch
    {
        // Skips the stamina RPC when unlimited stamina is active.
        private static bool Prefix(Player __instance)
        {
            return __instance != Player.m_localPlayer || !Mode.IsHammer();
        }
    }
}
