using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Restores weapon durability after local attacks in Hammer worlds.
    [HarmonyPatch(typeof(Attack), nameof(Attack.OnAttackTrigger))]
    internal static class AttackDurabilityPatch
    {
        // Captures weapon durability before an attack.
        private static void Prefix(Humanoid ___m_character, ItemDrop.ItemData ___m_weapon,
            out DurabilitySnapshot __state)
        {
            __state = new DurabilitySnapshot(
                ___m_weapon, DurabilityProtection.IsActive(___m_character));
        }

        // Restores the captured weapon durability after an attack.
        private static void Postfix(DurabilitySnapshot __state)
        {
            __state.Restore();
        }
    }
}
