using Frutti.Game.Screens.Menu.Auth;
using osu.Framework.Screens;

namespace Frutti.Game;

public partial class FruttiGame : FruttiGameBase
{
    private DependencyContainer? dependencies;

    private ScreenStack? screens;

    [BackgroundDependencyLoader]
    private void load()
    {
        Add(screens = new ScreenStack());

        screens.Push(new AuthMenuScreen());
    }

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
}
