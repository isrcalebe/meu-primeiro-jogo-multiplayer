using System.Threading;
using System.Threading.Tasks;

namespace Frutti.Server.Application.Networking.Packets;

public interface IPacketHandler
{
    int PacketId { get; }

    Task HandleAsync(byte[] data, CancellationToken cancellationToken = default);
}
