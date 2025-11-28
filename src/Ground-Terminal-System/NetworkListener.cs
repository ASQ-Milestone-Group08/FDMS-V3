using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GroundTerminalSystem
{
    public class NetworkListener
    {
        private TcpListener _listener;
        private bool _isRunning = false;

        public event Action<string> PacketReceived;

        public void InitializePort(string ip, int port)
        {
            _listener = new TcpListener(IPAddress.Parse(ip), port);
        }

        public void Start()
        {
            _isRunning = true;
            _listener.Start();
            Listen();
        }

        private async void Listen()
        {
            try
            {
                while (_isRunning)
                {
                    var client = await AcceptConnection();
                    await ReadPacket(client);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<TcpClient> AcceptConnection()
        {
            return await _listener.AcceptTcpClientAsync();
        }

        private async Task ReadPacket(TcpClient client)
        {
            try
            {
                using var stream = client.GetStream();
                using var reader = new StreamReader(stream);

                // Keep reading packets from this client until connection closes
                while (_isRunning && !reader.EndOfStream)
                {
                    string? packet = await reader.ReadLineAsync();

                    if (!string.IsNullOrEmpty(packet))
                    {
                        PacketReceived?.Invoke(packet);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HandleClient error: {ex.Message}");
            }
            finally
            {
                client?.Close();
            }
        }

        public void SendDisconnect()
        {
            _isRunning = false;
            _listener.Stop();
        }
    }
}
