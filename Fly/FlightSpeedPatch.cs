using HarmonyLib;
using UnityEngine;

namespace Landoria.HammerFreedom
{
    // Limits local debug flight speed in Hammer worlds.
    [HarmonyPatch(typeof(Character), "UpdateDebugFly")]
    internal static class FlightSpeedPatch
    {
        // Enables sprint speed for vertical-only flight input.
        private static void Prefix(Character __instance, ref bool ___m_run)
        {
            if (__instance != Player.m_localPlayer || !Mode.IsHammer())
            {
                return;
            }

            bool verticalInput = ZInput.GetButton("Jump") || ZInput.GetButton("JoyJump") ||
                ZInput.GetKey(KeyCode.LeftControl) || ZInput.GetButtonPressedTimer("JoyCrouch") > 0.33f;
            if (verticalInput && (ZInput.GetButton("Run") || ZInput.GetButton("JoyRun")))
            {
                ___m_run = true;
            }
        }

        // Clamps normal and sprint flight to their configured limits.
        private static void Postfix(
            Character __instance, bool ___m_run, ref Vector3 ___m_currentVel,
            Rigidbody ___m_body)
        {
            if (__instance != Player.m_localPlayer || !Mode.IsHammer())
            {
                return;
            }

            float maximumSpeed = Preference.FlightSpeed * (___m_run ? 4f : 1f);
            float currentSpeed = ___m_currentVel.magnitude;
            float scale = currentSpeed > maximumSpeed ? maximumSpeed / currentSpeed : 1f;
            if (scale < 1f)
            {
                ___m_currentVel *= scale;
                ___m_body.linearVelocity = ___m_currentVel;
            }
        }
    }
}
