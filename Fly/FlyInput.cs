namespace Landoria.HammerFreedom
{
    // Resolves flight input behavior for the local player.
    internal static class FlyInput
    {
        // Returns whether a normal ground action should proceed.
        internal static bool ShouldApplyGroundAction(Character character)
        {
            Player localPlayer = Player.m_localPlayer;
            return character != localPlayer || localPlayer == null ||
                !localPlayer.IsDebugFlying() || !Mode.IsHammer();
        }

        // Returns whether the flight shortcut can be handled.
        internal static bool IsAvailable()
        {
            return Mode.IsHammer() &&
                   Player.m_localPlayer != null &&
                   !Console.IsVisible() && !TextInput.IsVisible() && !InventoryGui.IsVisible() &&
                   !Menu.IsVisible() && (Chat.instance == null || !Chat.instance.HasFocus());
        }

    }
}
