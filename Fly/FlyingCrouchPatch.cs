using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Prevents crouching while the local player is flying.
    [HarmonyPatch(typeof(Player), "SetCrouch")]
    internal static class FlyingCrouchPatch
    {
        // Skips crouch activation during Hammer flight.
        private static bool Prefix(Player __instance, bool crouch)
        {
            return !crouch || FlyInput.ShouldApplyGroundAction(__instance);
        }
    }
}
