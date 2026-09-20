using HarmonyLib;
using UnityEngine;

namespace Landoria.HammerFreedom
{
    // Refreshes hidden player and held-item visuals after equipment changes.
    [HarmonyPatch(typeof(VisEquipment), "UpdateVisuals")]
    internal static class VisualVisibilityPatch
    {
        // Hides any renderers newly created for the local player.
        private static void Postfix(VisEquipment __instance)
        {
            Player player = __instance.GetComponentInParent<Player>();
            if (player == Player.m_localPlayer)
            {
                VisibilityController.Refresh(player);
            }
        }
    }
}
