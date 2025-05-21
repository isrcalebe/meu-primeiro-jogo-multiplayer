using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Frutti.Server.Net;

public class FruttiConnectionHandler
{
    private readonly TcpClient socket;
    private readonly ILogger<FruttiConnectionHandler> logger;
    private readonly Func<string, Task<string>> packetProcessor;

    public FruttiConnectionHandler(TcpClient socket, ILogger<FruttiConnectionHandler> logger, Func<string, Task<string>> packetProcessor)
    {
        this.socket = socket;
        this.logger = logger;
        this.packetProcessor = packetProcessor;
    }

    public async Task HandleConnectionAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Client connected: {RemoteEndPoint}", socket.Client.RemoteEndPoint);

        try
        {
            using var stream = socket.GetStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);
            using var writer = new StreamWriter(stream, Encoding.UTF8);

            writer.AutoFlush = true;

            while (!cancellationToken.IsCancellationRequested)
            {
                string? message;

                try
                {
                    message = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (IOException ex)
                {
                    logger.LogWarning(ex, "Client {RemoteEndPoint} disconnected unexpectedly.", socket.Client.RemoteEndPoint);
                    break;
                }
                catch (ObjectDisposedException)
                {
                    logger.LogInformation("Client {RemoteEndPoint} stream disposed.", socket.Client.RemoteEndPoint);
                    break;
                }

                if (message == null)
                {
                    logger.LogInformation("Client disconnected: {RemoteEndPoint}", socket.Client.RemoteEndPoint);
                    break;
                }

                logger.LogInformation("Received from {RemoteEndPoint}: {Message}", socket.Client.RemoteEndPoint, message);

                var response = await packetProcessor(message).ConfigureAwait(false);
                await writer.WriteLineAsync(response).ConfigureAwait(false);
                logger.LogInformation("Sent to {RemoteEndPoint}: {Response}", socket.Client.RemoteEndPoint, response);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling socket connection {RemoteEndPoint}.", socket.Client.RemoteEndPoint);
        }
        finally
        {
            socket.Close();
            logger.LogInformation("Client connection closed: {RemoteEndPoint}", socket.Client.RemoteEndPoint);
        }
    }
}
