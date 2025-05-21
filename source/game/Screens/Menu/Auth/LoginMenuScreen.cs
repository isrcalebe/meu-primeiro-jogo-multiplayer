using Frutti.Game.Screens.Menu.Auth.Components;

namespace Frutti.Game.Screens.Menu.Auth;

public partial class LoginMenuScreen : FruttiScreen
{
    private LoginForm loginForm = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        AddInternal(loginForm = new LoginForm
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        });
    }
}
