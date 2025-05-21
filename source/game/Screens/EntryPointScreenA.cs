using osu.Framework.Graphics.Sprites;
using Frutti.Game.Graphics;

namespace Frutti.Game.Screens;

public partial class EntryPointScreenA : FruttiScreen
{
    [BackgroundDependencyLoader]
    private void load()
    {
        AddInternal(new SpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Text = "Screen A",
            Font = FruttiFont.Roboto.With(size: 48.0f),
        });
    }
}
