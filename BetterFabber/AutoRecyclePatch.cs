using CG.Game;
using CG.Objects;
using CG.Ship.Hull;
using Client.Player.Interactions;
using Gameplay.Carryables;
using Gameplay.Factory;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Reflection;
using UI.Core;
using UI.Fabricator;

namespace BetterFabber
{
    [HarmonyPatch(typeof(FabricatorData))]
    class AutoRecyclePatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, new Type[] { typeof(CarryableFactoryLogic), typeof(ContextInfo), typeof(CraftableDataList) })]
        static void Constructor(FabricatorData __instance, CarryableFactoryLogic logic)
        {
            logic.PrintSocket.OnAcquireCarryable +=
                (printSocket, carryableObject, previousCarrier) => OnInsert(__instance, printSocket, carryableObject, previousCarrier);
        }

        private static void OnInsert(FabricatorData instance, ICarrier printSocket, AbstractCarryableObject carryableObject, ICarrier previousCarrier)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            if ((BepinPlugin.Bindings.AutoRecycleAlloy.Value && carryableObject.name.Contains("Item_Alloy")) ||
                (BepinPlugin.Bindings.AutoRecycleEverything.Value && previousCarrier != printSocket && !carryableObject.name.Contains("Item_BlankBuildBox")))
            {
                instance.RecycleNow(instance.SocketItem);
            }
        }
    }

    [HarmonyPatch(typeof(CarryablesSocket))]
    class CarryablesSocketPatch
    {
        private static readonly FieldInfo carrierField = AccessTools.Field(typeof(AbstractCarryableObject), "carrier");

        [HarmonyPrefix]
        [HarmonyPatch("TryInsertCarryable")]
        static void TryInsertCarryable(CarryablesSocket __instance, AbstractCarryableObject carryable)
        {
            if (__instance != ClientGame.Current?.PlayerShip?.gameObject?.GetComponentInChildren<CarryableFactoryLogic>()?.PrintSocket) return;

            if (carryable.Carrier == null)
                carrierField.SetValue(carryable, __instance);
        }
    }
}
