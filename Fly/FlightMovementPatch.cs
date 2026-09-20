using HarmonyLib;
using UnityEngine;

namespace Landoria.HammerFreedom
{
    // Applies camera-relative movement and configured speed to Hammer flight.
    [HarmonyPatch(typeof(Character), "UpdateDebugFly")]
    internal static class FlightMovementPatch
    {
        // Captures velocity before Valheim updates debug flight.
        private static void Prefix(
            Character __instance, Vector3 ___m_currentVel, out Vector3 __state)
        {
            __state = __instance == Player.m_localPlayer && Mode.IsHammer() ?
                ___m_currentVel : Vector3.positiveInfinity;
        }

        // Replaces vanilla movement with camera-relative flight.
        private static void Postfix(
            Character __instance, bool ___m_run, ref Vector3 ___m_currentVel,
            Rigidbody ___m_body, Vector3 __state)
        {
            if (float.IsInfinity(__state.x))
            {
                return;
            }

            float speed = Preference.FlightSpeed * (___m_run ? 2f : 1f);
            Vector3 targetVelocity = FlyInput.GetCameraMovement() * speed;
            ___m_currentVel = Vector3.Lerp(__state, targetVelocity, 0.5f);
            ___m_body.linearVelocity = ___m_currentVel;
        }
    }
}
