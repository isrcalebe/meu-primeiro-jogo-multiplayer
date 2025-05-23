using System.Threading;
using System.Threading.Tasks;

namespace Frutti.Server.Application.Networking.Packets;

public interface IPacketRouter
{
    Task RouteAsync(byte[] data, CancellationToken cancellationToken = default);
}
