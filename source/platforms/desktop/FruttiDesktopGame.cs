using System.Reflection;
using Frutti.Game;
using osu.Framework.Platform;

namespace Frutti.Desktop;

internal sealed partial class FruttiDesktopGame : FruttiGame
{
    public override void SetHost(GameHost host)
    {
        base.SetHost(host);

        var iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "Frutti.ico");
        if (iconStream != null)
            host.Window?.SetIconFromStream(iconStream);
    }
}
