using Frutti.Game.Screens.Menu.Auth.Components;

namespace Frutti.Game.Screens.Menu.Auth;

public partial class AuthMenuScreen : FruttiScreen
{
    private RegisterForm registerForm = null!;
    private LoginForm loginForm = null!;

    [BackgroundDependencyLoader]
    private void load()
    {
        AddInternal(registerForm = new RegisterForm
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        });
    }
}
