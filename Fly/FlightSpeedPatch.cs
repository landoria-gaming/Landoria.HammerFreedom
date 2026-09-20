using HarmonyLib;
using UnityEngine;

namespace Landoria.HammerFreedom
{
    // Limits local debug flight speed in Hammer worlds.
    [HarmonyPatch(typeof(Character), "UpdateDebugFly")]
    internal static class FlightSpeedPatch
    {
        // Clamps normal and sprint flight to their configured limits.
        private static void Postfix(
            Character __instance, bool ___m_run, ref Vector3 ___m_currentVel,
            Rigidbody ___m_body)
        {
            if (__instance != Player.m_localPlayer || !Mode.IsHammer())
            {
                return;
            }

            float maximumSpeed = Preference.FlightSpeed * (___m_run ? 2f : 1f);
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
