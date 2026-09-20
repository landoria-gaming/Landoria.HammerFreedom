using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Restores item durability after the local player blocks an attack.
    [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
    internal static class BlockDurabilityPatch
    {
        // Captures both hand items before blocking.
        private static void Prefix(Humanoid __instance, ItemDrop.ItemData ___m_rightItem,
            ItemDrop.ItemData ___m_leftItem, out DurabilitySnapshot[] __state)
        {
            bool preserve = DurabilityProtection.IsActive(__instance);
            __state = new[]
            {
                new DurabilitySnapshot(___m_rightItem, preserve),
                new DurabilitySnapshot(___m_leftItem, preserve)
            };
        }

        // Restores both captured items after blocking.
        private static void Postfix(DurabilitySnapshot[] __state)
        {
            foreach (DurabilitySnapshot snapshot in __state)
            {
                snapshot.Restore();
            }
        }
    }
}
