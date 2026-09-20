using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents the debug camera shortcut from changing Hammer flight speed.
    [HarmonyPatch(typeof(GameCamera), "UpdateCamera")]
    internal static class FlightScrollSpeedPatch
    {
        // Captures the vanilla speed before the camera handles scroll input.
        private static void Prefix(out int __state)
        {
            Player player = Player.m_localPlayer;
            __state = player != null && player.IsDebugFlying() && Mode.IsHammer() ?
                Character.m_debugFlySpeed : -1;
        }

        // Restores the captured speed after the camera handles scroll input.
        private static void Postfix(int __state)
        {
            if (__state >= 0)
            {
                Character.m_debugFlySpeed = __state;
            }
        }
    }
}
