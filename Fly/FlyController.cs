using UnityEngine;

namespace Landoria.HammerFreedom
{
    // Controls debug flight for the local player.
    internal static class FlyController
    {
        private const float InitialLift = 5f;

        // Toggles the current local flight state.
        internal static void Toggle()
        {
            Player player = Player.m_localPlayer;
            if (player != null && Mode.IsHammer())
            {
                SetEnabled(!player.IsDebugFlying());
            }
        }

        // Sets local flight when Hammer mode permits it.
        internal static void SetEnabled(bool enabled)
        {
            Player player = Player.m_localPlayer;
            if (player == null || enabled && !Mode.IsHammer())
            {
                return;
            }

            if (player.IsDebugFlying() != enabled)
            {
                player.ToggleDebugFly();
                if (enabled)
                {
                    Lift(player);
                }
            }
        }

        // Moves the player clear of the ground when flight starts.
        private static void Lift(Player player)
        {
            Vector3 position = player.transform.position + Vector3.up * InitialLift;
            Rigidbody body = player.GetComponent<Rigidbody>();
            if (body)
            {
                body.position = position;
                body.linearVelocity = Vector3.zero;
            }

            player.transform.position = position;
        }
    }
}
