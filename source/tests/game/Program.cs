using Frutti.Game.Tests;
using osu.Framework;

using var host = Host.GetSuitableDesktopHost("frutti-visual-tests", new HostOptions
{
    PortableInstallation = true
});
using var game = new FruttiTestBrowser();

host.Run(game);
