namespace Landoria.HammerFreedom
{
    // Resolves flight input behavior for the local player.
    internal static class FlyInput
    {
        // Returns whether a normal ground action should proceed.
        internal static bool ShouldApplyGroundAction(Character character)
        {
            Player localPlayer = Player.m_localPlayer;
            return character != localPlayer || localPlayer == null ||
                !localPlayer.IsDebugFlying() || !Mode.IsHammer();
        }

        // Returns whether the flight shortcut can be handled.
        internal static bool IsAvailable()
        {
            return Mode.IsHammer() &&
                   Player.m_localPlayer != null &&
                   !Console.IsVisible() && !TextInput.IsVisible() && !InventoryGui.IsVisible() &&
                   !Menu.IsVisible() && (Chat.instance == null || !Chat.instance.HasFocus());
        }

        // Returns normalized movement transformed by the camera rotation.
        internal static UnityEngine.Vector3 GetCameraMovement()
        {
            if (!IsAvailable() || GameCamera.instance == null)
            {
                return UnityEngine.Vector3.zero;
            }

            UnityEngine.Vector3 movement = UnityEngine.Vector3.zero;
            movement += ZInput.GetButton("Left") ?
                -UnityEngine.Vector3.right : UnityEngine.Vector3.zero;
            movement += ZInput.GetButton("Right") ?
                UnityEngine.Vector3.right : UnityEngine.Vector3.zero;
            movement += ZInput.GetButton("Forward") ?
                UnityEngine.Vector3.forward : UnityEngine.Vector3.zero;
            movement += ZInput.GetButton("Backward") ?
                -UnityEngine.Vector3.forward : UnityEngine.Vector3.zero;
            movement += ZInput.GetButton("Jump") ?
                UnityEngine.Vector3.up : UnityEngine.Vector3.zero;
            movement += ZInput.GetButton("Crouch") ?
                -UnityEngine.Vector3.up : UnityEngine.Vector3.zero;
            movement += UnityEngine.Vector3.up * ZInput.GetJoyRTrigger();
            movement -= UnityEngine.Vector3.up * ZInput.GetJoyLTrigger();
            movement += UnityEngine.Vector3.right * ZInput.GetJoyLeftStickX();
            movement -= UnityEngine.Vector3.forward * ZInput.GetJoyLeftStickY();
            return GameCamera.instance.transform.TransformVector(
                UnityEngine.Vector3.ClampMagnitude(movement, 1f));
        }
    }
}
