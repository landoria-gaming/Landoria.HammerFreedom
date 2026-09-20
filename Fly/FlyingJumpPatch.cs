using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents jumping while the local player is flying.
    [HarmonyPatch(typeof(Character), nameof(Character.Jump))]
    internal static class FlyingJumpPatch
    {
        // Skips jump handling during Hammer flight.
        private static bool Prefix(Character __instance)
        {
            return FlyInput.ShouldApplyGroundAction(__instance);
        }
    }
}
