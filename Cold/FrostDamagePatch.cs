using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents frost damage for the local player in Hammer worlds.
    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    internal static class FrostDamagePatch
    {
        // Removes only the frost component from incoming local damage.
        private static void Prefix(Character __instance, HitData hit)
        {
            if (__instance == Player.m_localPlayer && Mode.IsHammer())
            {
                hit.m_damage.m_frost = 0f;
            }
        }
    }
}
