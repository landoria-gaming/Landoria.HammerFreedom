namespace Landoria.HammerFreedom
{
    // Determines whether an item should keep its durability.
    internal static class DurabilityProtection
    {
        // Returns whether durability protection applies to this humanoid.
        internal static bool IsActive(Humanoid humanoid)
        {
            return humanoid == Player.m_localPlayer && Mode.IsHammer();
        }
    }
}
