using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents fall damage for the local player in Hammer worlds.
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    internal static class FallDamagePatch
    {
        // Skips only local fall damage while Hammer mode is active.
        private static bool Prefix(Character __instance, HitData hit)
        {
            return __instance != Player.m_localPlayer ||
                hit.m_hitType != HitData.HitType.Fall || !Mode.IsHammer();
        }
    }
}
