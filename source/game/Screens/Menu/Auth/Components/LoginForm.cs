using Frutti.Game.Extensions.FontExtensions;
using Frutti.Game.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace Frutti.Game.Screens.Menu.Auth.Components;

public partial class LoginForm : CompositeDrawable
{
    private readonly TextBox usernameTextBox;
    private readonly TextBox passwordTextBox;
    private readonly Button submitButton;
    private readonly SpriteText userStatusText;

    private readonly Box backgroundBox;

    private readonly Bindable<string> usernameState;
    private readonly Bindable<string> passwordState;

    public LoginForm()
    {
        usernameState = new Bindable<string>(string.Empty);
        passwordState = new Bindable<string>(string.Empty);

        AutoSizeAxes = Axes.Both;
        Masking = true;
        BorderThickness = 2.0f;
        BorderColour = Colour4.FromHex("27272a");

        Padding = new MarginPadding(24.0f);
        CornerRadius = 24.0f;

        InternalChildren =
        [
            backgroundBox = new Box
            {
                Name = "Background Box",
                RelativeSizeAxes = Axes.Both,
                Colour = Colour4.FromHex("09090b")
            },
            new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(8.0f),
                AutoSizeAxes = Axes.Both,
                Padding = new MarginPadding(32.0f),
                Children =
                [
                    usernameTextBox = new BasicTextBox
                    {
                        PlaceholderText = "Username",
                        Size = new Vector2(200.0f, 32.0f),
                    },
                    passwordTextBox = new BasicTextBox
                    {
                        PlaceholderText = "Password",
                        Size = new Vector2(200.0f, 32.0f),
                    },
                    submitButton = new BasicButton
                    {
                        Text = "Login",
                        Action = onSubmit,
                        RelativeSizeAxes = Axes.X,
                        Height = 32.0f,
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Child = userStatusText = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "Not logged in.",
                            Font = FruttiFont.Inter.With(weight: FontWeight.Medium)
                        }
                    }
                ]
            }
        ];

        usernameState.BindTo(usernameTextBox.Current);
        passwordState.BindTo(passwordTextBox.Current);
    }

    private void onSubmit()
    {
        Logger.Log($"Data: {{ Username: {usernameState.Value}, Password: {passwordState.Value} }}");
    }
}
