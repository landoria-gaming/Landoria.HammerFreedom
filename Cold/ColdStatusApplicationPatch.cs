using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents cold statuses from being applied to the local player in Hammer worlds.
    [HarmonyPatch(typeof(SEMan), nameof(SEMan.AddStatusEffect), typeof(int), typeof(bool), typeof(int), typeof(float), typeof(short))]
    internal static class ColdStatusApplicationPatch
    {
        // Rejects cold and freezing before their status messages can appear.
        private static bool Prefix(Character ___m_character, int nameHash)
        {
            if (___m_character != Player.m_localPlayer || !Mode.IsHammer())
            {
                return true;
            }

            return nameHash != SEMan.s_statusEffectCold && nameHash != SEMan.s_statusEffectFreezing;
        }
    }
}
