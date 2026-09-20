using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents armor durability loss for the local player in Hammer worlds.
    [HarmonyPatch(typeof(Player), "DamageArmorDurability")]
    internal static class ArmorDurabilityPatch
    {
        // Skips armor durability damage when protection is active.
        private static bool Prefix(Player __instance)
        {
            return !DurabilityProtection.IsActive(__instance);
        }
    }
}
