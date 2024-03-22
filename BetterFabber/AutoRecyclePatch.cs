using CG.Objects;
using HarmonyLib;
using Photon.Pun;
using UI.Fabricator;

namespace BetterFabber
{
    [HarmonyPatch(typeof(FabricatorData), "TryInsert")]
    class AutoRecyclePatch
    {
        [HarmonyPostfix]
        static void OnChanged(FabricatorData __instance, AbstractCarryableObject x)
        {
            if(!BepinPlugin.Bindings.AutoRecycleAlloy.Value || !PhotonNetwork.IsMasterClient)
            {
                return;
            }

            //All alloy objects should have a naming scheme like Item_Alloy_1. 
            //This is one of a few ways to check the object is alloy, the other being to use GUIDs, which would be more reliable assuming GUIDs and specified alloys never change.
            if (x.name.Contains("Item_Alloy"))
            {
                __instance.RecycleNow(__instance.SocketItem);
            }
        }
    }
}
