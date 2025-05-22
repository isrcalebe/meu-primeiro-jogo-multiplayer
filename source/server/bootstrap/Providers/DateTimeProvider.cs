using System;
using Frutti.Server.Domain.Providers;

namespace Frutti.Server.Bootstrap.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTimeOffset NowOffset => DateTimeOffset.Now;

    public DateTime UtcNow => DateTime.Now;

    public DateTimeOffset UtcNowOffset => DateTimeOffset.Now;
}
