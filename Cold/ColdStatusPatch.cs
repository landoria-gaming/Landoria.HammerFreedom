using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents environmental cold and freezing in Hammer worlds.
    [HarmonyPatch(typeof(Player), "UpdateEnvStatusEffects")]
    internal static class ColdStatusPatch
    {
        // Removes cold statuses after Valheim updates the local environment.
        private static void Postfix(Player __instance)
        {
            if (__instance != Player.m_localPlayer || !Mode.IsHammer())
            {
                return;
            }

            SEMan statusEffects = __instance.GetSEMan();
            statusEffects.RemoveStatusEffect(SEMan.s_statusEffectCold, quiet: true);
            statusEffects.RemoveStatusEffect(SEMan.s_statusEffectFreezing, quiet: true);
        }
    }
}
