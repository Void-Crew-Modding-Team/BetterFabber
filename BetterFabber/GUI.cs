using UnityEngine;
using VoidManager.CustomGUI;
using static UnityEngine.GUILayout;

namespace BetterFabber
{
    internal class BFGUI : ModSettingsMenu
    {
        string FabRecycleSpeed;
        string FabPrintSpeed;
        string errorMessage;

        public override void Draw()
        {
            //UI
            Label("Better Fabber menu");
            FlexibleSpace();
            Label("Fabricator Recycle Speed");
            FabRecycleSpeed = TextField(FabRecycleSpeed);
            FlexibleSpace();
            Label("Fabricator Print Speed");
            FabPrintSpeed = TextField(FabPrintSpeed);
            FlexibleSpace();
            if(Button($"Auto Recycle Alloy: {(BepinPlugin.Bindings.AutoRecycleAlloy.Value ? "Enabled" : "Disabled")}"))
            {
                BepinPlugin.Bindings.AutoRecycleAlloy.Value = !BepinPlugin.Bindings.AutoRecycleAlloy.Value;
            }


            //Value validation
            float result;
            if (float.TryParse(FabRecycleSpeed, out result))
            {
                if(result < 0.25f)
                {
                    errorMessage = "Recycle Speed cannot go below 0.25x";
                }
                else if (BepinPlugin.Bindings.FabRecycleSpeedMultiplier.Value != result)
                {
                    BepinPlugin.Log.LogInfo("Updating Fab Recycle Speed to " + result);
                    BepinPlugin.Bindings.FabRecycleSpeedMultiplier.Value = result;
                }
            }
            if (float.TryParse(FabPrintSpeed, out result))
            {
                if (result < 0.25f)
                {
                    errorMessage = "Print Speed cannot go below 0.25x";
                }
                else if (BepinPlugin.Bindings.FabPrintSpeedMultiplier.Value != result)
                {
                    BepinPlugin.Log.LogInfo("Updating Fab Print Speed to " + result);
                    BepinPlugin.Bindings.FabPrintSpeedMultiplier.Value = result;
                }
            }

            if (errorMessage != null)
            {
                GUI.color = Color.red;
                Label(errorMessage);
                errorMessage = null;
                GUI.color = Color.white;
            }
        }

        public override string Name()
        {
            return "Better Fabber";
        }

        public override void OnOpen()
        {
            if (FabRecycleSpeed == null)
            {
                FabRecycleSpeed = BepinPlugin.Bindings.FabRecycleSpeedMultiplier.Value.ToString();
            }
            if (FabPrintSpeed == null)
            {
                FabPrintSpeed = BepinPlugin.Bindings.FabPrintSpeedMultiplier.Value.ToString();
            }
        }
    }
}
