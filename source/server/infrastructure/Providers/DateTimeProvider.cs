using System;
using Frutti.Server.Domain.Providers;

namespace Frutti.Server.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTimeOffset NowOffset => DateTimeOffset.Now;
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset UtcNowOffset => DateTimeOffset.UtcNow;
}
