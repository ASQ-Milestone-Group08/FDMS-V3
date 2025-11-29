/*
 * File Name    : NetworkListener.cs
 * Description  : This is the class for listening to incoming network connections and reading data packets.
 * Author       : Andrei Haboc
 * Last Modified: November 28, 2025
 */
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace GroundTerminalSystem
{
    public class NetworkListener
    {
        private TcpListener _listener;
        private CancellationTokenSource _cts;
        private bool _isRunning;

        public event Action<string> PacketReceived;

        public void InitializePort(string ip, int port)
        {
            _listener = new TcpListener(IPAddress.Parse(ip), port);
        }

        public void Start()
        {
            if (_listener == null)
                throw new InvalidOperationException("Listener not initialized. Call InitializePort first.");

            _cts = new CancellationTokenSource();
            _listener.Start();
            _isRunning = true;

            // Run accept loop on a background thread
            Task.Run(() => AcceptLoopAsync(_cts.Token));
        }

        public void SendDisconnect()
        {
            _isRunning = false;
            try
            {
                _cts?.Cancel();
                _listener?.Stop();
            }
            catch { }
        }

        private async Task AcceptLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    TcpClient client;
                    try
                    {
                        client = await _listener.AcceptTcpClientAsync(token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception)
                    {
                        if (!token.IsCancellationRequested)
                            throw;
                        break;
                    }

                    // Handle each client on its own task
                    _ = Task.Run(() => HandleClientAsync(client, token), token);
                }
            }
            finally
            {
                _listener.Stop();
            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken token)
        {
            using (client)
            using (var stream = client.GetStream())
            using (var reader = new StreamReader(stream))
            {
                try
                {
                    while (!token.IsCancellationRequested && _isRunning)
                    {
                        string line = await reader.ReadLineAsync();

                        if (line == null)
                        {
                            // client closed connection
                            break;
                        }

                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            // Fire event WITHOUT blocking the read loop
                            var captured = line;
                            Task.Run(() => PacketReceived?.Invoke(captured), token);
                        }
                    }
                }
                catch (IOException)
                {
                    // network error, client likely disconnected
                }
                catch (Exception)
                {
                    // swallow any unexpected errors for now
                }
            }
        }
    }
}
