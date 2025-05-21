using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Platform;
using Frutti.Game.Configuration.Settings;

namespace Frutti.Game.Configuration;

public class FruttiConfigManager : IniConfigManager<FruttiSetting>
{
    protected override string Filename => "frutti.ini";

    public FruttiConfigManager(Storage storage, IDictionary<FruttiSetting, object>? defaultOverrides = null)
        : base(storage, defaultOverrides)
    {
        PerformSave();
    }

    protected override void InitialiseDefaults()
    {
        SetDefault(FruttiSetting.ScreenEntryPoint, ScreenEntryPoint.ScreenA);
    }
}
