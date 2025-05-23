using System.Threading;
using System.Threading.Tasks;

namespace Frutti.Server.Application.Networking;

public interface IServer
{
    Task ListenAsync(CancellationToken cancellationToken);
}
