using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Frutti.Server.Services;

public class FruttiServerService : IHostedService, IDisposable
{
    private readonly ILogger<FruttiServerService> logger;
    private readonly int port;
    private readonly Func<TcpClient, Task> clientHandlerFactory; // Fábrica para criar e lidar com conexões
    private TcpListener listener;
    private CancellationTokenSource cancellationTokenSource;
    private Task listenerTask;

    public FruttiServerService(ILogger<FruttiServerService> logger, int port, Func<TcpClient, Task> clientHandlerFactory)
    {
        this.logger = logger;
        this.port = port;
        this.clientHandlerFactory = clientHandlerFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        listener = new TcpListener(IPAddress.Any, port);

        try
        {
            listener.Start();
            logger.LogInformation("TCP Server started on port {Port}", port);

            listenerTask = Task.Run(async () =>
                await listenForClientsAsync(cancellationTokenSource.Token).ConfigureAwait(false), cancellationTokenSource.Token);
        }
        catch (SocketException ex)
        {
            logger.LogError(ex, "Failed to start TCP server.");
            return Task.FromException(ex);
        }

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping TCP Server...");
        await cancellationTokenSource.CancelAsync().ConfigureAwait(false);
        listener.Stop();

        await Task.WhenAny(listenerTask, Task.Delay(TimeSpan.FromSeconds(5), cancellationToken)).ConfigureAwait(false);

        if (!listenerTask.IsCompleted)
            logger.LogWarning("TCP Listener task did not complete gracefully.");

        logger.LogInformation("TCP Server stopped.");
    }

    private async Task listenForClientsAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);
                _ = clientHandlerFactory(client);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("TCP Listener cancelled.");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error accepting client connection.");
            }
        }
    }

    #region IDisposable Support

    public void Dispose()
    {
        listener.Dispose();
        cancellationTokenSource.Dispose();
    }

    #endregion
}
