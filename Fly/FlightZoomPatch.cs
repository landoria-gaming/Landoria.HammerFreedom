using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Locks the camera at its closest distance during Hammer flight.
    [HarmonyPatch(typeof(GameCamera), "UpdateCamera")]
    internal static class FlightZoomPatch
    {
        // Applies the closest distance before Valheim positions the camera.
        private static void Prefix(GameCamera __instance, ref float ___m_distance)
        {
            Apply(__instance, ref ___m_distance);
        }

        // Discards any zoom input handled during the camera update.
        private static void Postfix(GameCamera __instance, ref float ___m_distance)
        {
            Apply(__instance, ref ___m_distance);
        }

        // Locks camera distance only for active local Hammer flight.
        private static void Apply(GameCamera camera, ref float distance)
        {
            Player player = Player.m_localPlayer;
            if (player && player.IsDebugFlying() && Mode.IsHammer())
            {
                distance = camera.m_minDistance;
            }
        }
    }
}
