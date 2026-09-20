using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace Landoria.HammerFreedom
{
    // Loads and unloads the HammerFreedom client features.
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class Plugin : BaseUnityPlugin
    {
        internal const string PluginGuid = "Landoria.HammerFreedom";
        internal const string PluginName = "Landoria.HammerFreedom";
        internal const string PluginVersion = "1.0.10";
        private static readonly KeyboardShortcut ToggleShortcut =
            new KeyboardShortcut(UnityEngine.KeyCode.Z);

        internal static ManualLogSource ModLogger { get; private set; }
        private Harmony _harmony;

        // Initializes commands and patches when the plugin loads.
        private void Awake()
        {
            ModLogger = Logger;
            Logger.LogInfo($"AssemblyVersion: {GetType().Assembly.GetName().Version}.");
            _harmony = new Harmony(PluginGuid);
            _harmony.PatchAll();
            FlyCommand.Register();
            ModLogger.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        // Updates world state and handles the flight shortcut.
        private void Update()
        {
            Mode.Update();
            HandleShortcuts();
        }

        // Toggles flight when the shortcut is available.
        private void HandleShortcuts()
        {
            if (!FlyInput.IsAvailable())
            {
                return;
            }

            if (ToggleShortcut.IsDown())
            {
                FlyController.Toggle();
            }
        }

        // Removes patches and clears shared state when the plugin unloads.
        private void OnDestroy()
        {
            ModLogger?.LogInfo($"{PluginName} {PluginVersion} is unloaded.");
            _harmony?.UnpatchSelf();
            _harmony = null;
            ModLogger = null;
        }
    }
}
