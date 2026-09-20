using BepInEx.Configuration;
using Landoria.Shared;

namespace Landoria.HammerFreedom
{
    // Reads and saves the player's HammerFreedom settings.
    internal static class Preference
    {
        internal const float DefaultFlightSpeed = 5f;
        private const float MinimumFlightSpeed = 1f;
        private const float MaximumFlightSpeed = 10f;

        private static ConfigEntry<KeyboardShortcut> toggleShortcut;
        private static ConfigEntry<float> flightSpeed;

        internal static KeyboardShortcut ToggleShortcut => toggleShortcut.Value;
        internal static float FlightSpeed => flightSpeed.Value;

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
                "\nOn QWERTZ keyboards, use Y for the physical Z key.\n" +
                "\nhttps://docs.unity3d.com/ScriptReference/KeyCode.html");
            flightSpeed = config.Bind(
                "Flight", "Speed", DefaultFlightSpeed,
                new ConfigDescription(
                    "Maximum flight speed in metres per second.",
                    new AcceptableValueRange<float>(
                        MinimumFlightSpeed, MaximumFlightSpeed)));
        }

        // Restores the default shortcut and recreates the configuration file.
        internal static void RestoreDefaults(ConfigFile config)
        {
            toggleShortcut.Value = new KeyboardShortcut(UnityEngine.KeyCode.Z);
            flightSpeed.Value = DefaultFlightSpeed;
            config.Save();
            ConfigWatcher.IgnoreCurrentFileVersion();
        }
    }
}
