using VoidManager.MPModChecks;

namespace BetterFabber
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Client;

        public override string Author => "Dragon";

        public override string Description => "Multiple changes to overhaul the ship fabricator";
    }
}
