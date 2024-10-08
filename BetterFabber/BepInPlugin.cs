﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace BetterFabber
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.USERS_PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency(VoidManager.MyPluginInfo.PLUGIN_GUID)]
    public class BepinPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = Logger;
            Bindings.FabRecycleSpeedMultiplier = Config.Bind("General", "FabRecycleSpeedMultiplier", 10f);
            Bindings.FabPrintSpeedMultiplier = Config.Bind("General", "FabPrintSpeedMultiplier", 10f);
            Bindings.AutoRecycleAlloy = Config.Bind("General", "AutoRecycleAlloy", true);
            Bindings.AutoRecycleEverything = Config.Bind("General", "AutoRecycleEverything", false);
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID);
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        internal class Bindings
        {
            internal static ConfigEntry<float> FabRecycleSpeedMultiplier;
            internal static ConfigEntry<float> FabPrintSpeedMultiplier;
            internal static ConfigEntry<bool> AutoRecycleAlloy;
            internal static ConfigEntry<bool> AutoRecycleEverything;
        }
    }
}