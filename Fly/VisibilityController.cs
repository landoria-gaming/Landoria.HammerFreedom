using System.Collections.Generic;
using UnityEngine;

namespace Landoria.HammerFreedom
{
    // Hides the local player and equipped visuals during flight.
    internal static class VisibilityController
    {
        private static readonly HashSet<Renderer> HiddenRenderers =
            new HashSet<Renderer>();
        private static Player hiddenPlayer;

        // Applies the visibility required by the current flight state.
        internal static void SetHidden(Player player, bool hidden)
        {
            if (!hidden || !player)
            {
                Restore();
                return;
            }

            if (hiddenPlayer != player)
            {
                Restore();
                hiddenPlayer = player;
                HideCurrentVisuals();
            }
        }

        // Hides visuals created by an equipment refresh during flight.
        internal static void Refresh(Player player)
        {
            if (player && player == hiddenPlayer)
            {
                HideCurrentVisuals();
            }
        }

        // Restores every renderer changed by the mod.
        internal static void Restore()
        {
            foreach (Renderer renderer in HiddenRenderers)
            {
                if (renderer)
                {
                    renderer.forceRenderingOff = false;
                }
            }

            HiddenRenderers.Clear();
            hiddenPlayer = null;
        }

        // Hides the body and every equipped item under the player hierarchy.
        private static void HideCurrentVisuals()
        {
            foreach (Renderer renderer in hiddenPlayer.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer && !renderer.forceRenderingOff)
                {
                    renderer.forceRenderingOff = true;
                    HiddenRenderers.Add(renderer);
                }
            }
        }
    }
}
