using System;

namespace Frutti.Server.Domain.Providers;

public interface IDateTimeProvider
{
    DateTime Now { get; }

    DateTimeOffset NowOffset { get; }

    DateTime UtcNow { get; }

    DateTimeOffset UtcNowOffset { get; }
}
