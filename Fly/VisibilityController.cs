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
        private static bool wasAirborne;

        // Updates visibility while flying and until the player lands.
        internal static void Update(Player player, bool enabled, bool flying)
        {
            if (!enabled || !player)
            {
                Restore();
                return;
            }

            if (flying)
            {
                wasAirborne |= !player.IsOnGround();
                SetHidden(player);
                return;
            }

            RestoreAfterLanding(player);
        }

        // Hides the requested player's current visuals.
        private static void SetHidden(Player player)
        {
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
            wasAirborne = false;
        }

        // Restores the player after flight has ended and the ground is reached.
        private static void RestoreAfterLanding(Player player)
        {
            if (hiddenPlayer != player)
            {
                return;
            }

            if (!player.IsOnGround())
            {
                wasAirborne = true;
                return;
            }

            if (wasAirborne)
            {
                Restore();
            }
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
