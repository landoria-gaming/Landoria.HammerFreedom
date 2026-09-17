using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace Landoria.HammerFreedom
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class HammerFreedomPlugin : BaseUnityPlugin
    {
        internal const string PluginGuid = "Landoria.HammerFreedom";
        internal const string PluginName = "Landoria.HammerFreedom";
        internal const string PluginVersion = "1.0.9";
        private static readonly KeyboardShortcut ToggleShortcut =
            new KeyboardShortcut(UnityEngine.KeyCode.Z);

        internal static ManualLogSource ModLogger { get; private set; }
        internal static HammerFreedomSettings Settings { get; private set; }
        private static bool settingsInitialized;


        private Harmony _harmony;

        private void RegisterPatches0()
        {
            _harmony.CreateClassProcessor(typeof(HammerFreedomAuthorizationOnSpawnPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FlyCommandRegistrationPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FlyCommandValidationPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FallDamagePatch)).Patch();
            _harmony.CreateClassProcessor(typeof(StaminaConsumptionPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(StaminaApplicationPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(AttackDurabilityPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(PlacementDurabilityPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(BlockDurabilityPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(EquippedDurabilityPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(ArmorDurabilityPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FlightSpeedPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FlyingJumpPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FlyingCrouchPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(HammerFreedomDisconnectPatch)).Patch();
        }

        private void Awake()
        {
            ModLogger = Logger;
            Logger.LogInfo($"AssemblyVersion: {GetType().Assembly.GetName().Version}.");
            _harmony = new Harmony(PluginGuid);
            RegisterPatches0();
            Settings = new HammerFreedomSettings();
            FlyCommand.Register();
            ModLogger.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        internal static void InitializeDedicatedServerSettings()
        {
            if (settingsInitialized || !ServerRole.IsDedicatedServer) return;
            Settings = HammerFreedomSettings.FromArguments(
                System.Environment.GetCommandLineArgs(), ModLogger);
            settingsInitialized = true;
        }

        private void Update()
        {
            HammerFreedomAuthorization.Update();
            HandleShortcuts();
        }

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

        private void OnDestroy()
        {
            HammerFreedomAuthorization.ResetSession();
            ModLogger?.LogInfo($"{PluginName} {PluginVersion} is unloaded.");
            _harmony?.UnpatchSelf();
            _harmony = null;
            Settings = null;
            settingsInitialized = false;
            ModLogger = null;
        }
    }
}
