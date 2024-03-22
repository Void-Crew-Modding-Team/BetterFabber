using Gameplay.Factory;
using HarmonyLib;

namespace BetterFabber
{
    [HarmonyPatch(typeof(CarryableFactoryUtils), "GetRecycleAnimationDuration")]
    internal class FabricatorRecycleSpeedPatch
    {
        [HarmonyPostfix]
        static void Patch(ref float __result)
        {
            __result /= BepinPlugin.Bindings.FabRecycleSpeedMultiplier.Value;
        }
    }
    [HarmonyPatch(typeof(CarryableFactoryUtils), "GetCopierAnimationDuration")]
    internal class FabricatorPrintSpeedPatch
    {
        [HarmonyPostfix]
        static void Patch(ref float __result)
        {
            __result /= BepinPlugin.Bindings.FabPrintSpeedMultiplier.Value;
        }
    }
}
