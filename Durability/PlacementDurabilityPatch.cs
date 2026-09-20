using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Restores tool durability after local building actions.
    [HarmonyPatch(typeof(Player), "UpdatePlacement")]
    internal static class PlacementDurabilityPatch
    {
        // Captures the equipped tool before a building action.
        private static void Prefix(Player __instance, ItemDrop.ItemData ___m_rightItem,
            out DurabilitySnapshot __state)
        {
            __state = new DurabilitySnapshot(
                ___m_rightItem, DurabilityProtection.IsActive(__instance));
        }

        // Restores the captured tool after a building action.
        private static void Postfix(DurabilitySnapshot __state)
        {
            __state.Restore();
        }
    }
}
