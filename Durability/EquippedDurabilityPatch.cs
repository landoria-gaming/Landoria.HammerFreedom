using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents equipped item durability loss in Hammer worlds.
    [HarmonyPatch(typeof(Humanoid), "DrainEquipedItemDurability")]
    internal static class EquippedDurabilityPatch
    {
        // Skips equipped item durability drain when protection is active.
        private static bool Prefix(Humanoid __instance)
        {
            return !DurabilityProtection.IsActive(__instance);
        }
    }
}
