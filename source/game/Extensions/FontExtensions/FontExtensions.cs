using osu.Framework.Graphics.Sprites;
using Frutti.Game.Graphics;

namespace Frutti.Game.Extensions.FontExtensions;

public static class FontExtensions
{
    public static FontUsage With(this FontUsage usage, FontTypeface? typeface = null, float? size = null, FontWeight? weight = null, bool? italics = null, bool? fixedWidth = null)
    {
        var familyStr = typeface != null ? FruttiFont.GetFamilyString(typeface.Value) : usage.Family;
        var weightStr = weight != null ? FruttiFont.GetWeightString(familyStr, weight.Value) : usage.Weight;

        return usage.With(familyStr, size, weightStr, italics, fixedWidth);
    }
}
