namespace Frutti.Server.Domain.Net;

public interface IPacketHandler
{
    Task<string> HandlePacketAsync(string rawPacket);
}
