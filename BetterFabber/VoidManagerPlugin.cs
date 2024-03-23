using VoidManager.MPModChecks;

namespace BetterFabber
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Client;

        public override string Author => "Dragon";

        public override string Description => "Increases fabricator recycle and print speed. Auto-recycles alloy.";
    }
}
