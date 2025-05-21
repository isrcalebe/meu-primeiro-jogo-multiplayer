using System;
using Frutti.Desktop;
using osu.Framework;

Environment.SetEnvironmentVariable("OSU_SDL3", "1");

using var host = Host.GetSuitableDesktopHost("frutti", new HostOptions
{
    FriendlyGameName = "Frutti"
});
using var game = new FruttiDesktopGame();

host.Run(game);
