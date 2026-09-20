namespace Landoria.HammerFreedom
{
    // Detects whether the current world uses the Hammer modifiers.
    internal static class Mode
    {
        // Returns whether the current world has both Hammer modifiers.
        internal static bool IsHammer()
        {
            return ZoneSystem.instance != null &&
                ZoneSystem.instance.GetGlobalKey(GlobalKeys.NoBuildCost) &&
                ZoneSystem.instance.GetGlobalKey(GlobalKeys.PassiveMobs);
        }

        // Disables flight when the player leaves a Hammer world.
        internal static void Update()
        {
            if (!IsHammer())
            {
                FlyController.SetEnabled(false);
            }
        }
    }
}
