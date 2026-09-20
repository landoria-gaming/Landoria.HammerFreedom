namespace Landoria.HammerFreedom
{
    // Controls debug flight for the local player.
    internal static class FlyController
    {
        // Toggles the current local flight state.
        internal static void Toggle()
        {
            Player player = Player.m_localPlayer;
            if (player != null && Mode.IsHammer())
            {
                SetEnabled(!player.IsDebugFlying());
            }
        }

        // Sets local flight when Hammer mode permits it.
        internal static void SetEnabled(bool enabled)
        {
            Player player = Player.m_localPlayer;
            if (player == null || enabled && !Mode.IsHammer())
            {
                return;
            }

            if (player.IsDebugFlying() != enabled)
            {
                player.ToggleDebugFly();
            }
        }
    }
}
