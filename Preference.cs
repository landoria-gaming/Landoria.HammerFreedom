using BepInEx.Configuration;
using Landoria.Shared;

namespace Landoria.HammerFreedom
{
    // Reads and saves the player's HammerFreedom settings.
    internal static class Preference
    {
        private static ConfigEntry<KeyboardShortcut> toggleShortcut;

        internal static KeyboardShortcut ToggleShortcut => toggleShortcut.Value;

        // Creates the saved configuration entries used by the mod.
        internal static void Initialize(ConfigFile config)
        {
            toggleShortcut = config.Bind(
                "Controls", "ToggleShortcut",
                new KeyboardShortcut(UnityEngine.KeyCode.Z),
                "Shortcut used to enable or disable flight.\n" +
                "\nExamples: Mouse2 for the middle mouse button.\n" +
                "\nMouse3/Mouse4 for the Forward/Back side button.\n" +
                "\nSpace + LeftControl for Left Ctrl + Space.\n" +
                "\nhttps://docs.unity3d.com/ScriptReference/KeyCode.html");
        }

        // Restores the default shortcut and recreates the configuration file.
        internal static void RestoreDefaults(ConfigFile config)
        {
            toggleShortcut.Value = new KeyboardShortcut(UnityEngine.KeyCode.Z);
            config.Save();
            ConfigWatcher.IgnoreCurrentFileVersion();
        }
    }
}
