global using osu.Framework.Allocation;
global using osu.Framework.Bindables;
global using osu.Framework.Graphics;
global using osu.Framework.Graphics.Primitives;
global using osu.Framework.Logging;
global using osu.Framework.Testing;
global using osu.Framework.Utils;
global using osuTK;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Frutti.Game.Configuration;
using Frutti.Game.Resources;

namespace Frutti.Game;

public abstract partial class FruttiGameBase : osu.Framework.Game
{
    private DependencyContainer? dependencies;

    protected FruttiConfigManager? LocalConfig { get; private set; }

    protected Storage? Storage { get; set; }

    [BackgroundDependencyLoader]
    private void load()
    {
        Resources.AddStore(new DllResourceStore(FruttiResourceAssemblyProvider.Assembly));

        addFonts();

        dependencies?.CacheAs(Storage);
        dependencies?.CacheAs(LocalConfig);
    }

    private void addFonts()
    {
        foreach (var weight in new[] { "Thin", "ExtraLight", "Light", "Regular", "Medium", "SemiBold", "Bold", "Black" })
        {
            AddFont(Resources, $@"Fonts/Inter/Inter-{weight}");
            AddFont(Resources, $@"Fonts/Inter/Inter-{weight}Italic");
        }
    }

    public override void SetHost(GameHost host)
    {
        base.SetHost(host);

        Storage ??= host.Storage;
        LocalConfig = new FruttiConfigManager(Storage);
    }

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
}
