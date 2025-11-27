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
            using var stream = client.GetStream();
            using var reader = new StreamReader(stream);

            string packet = await reader.ReadLineAsync();

            if (!string.IsNullOrEmpty(packet))
            {
                PacketReceived?.Invoke(packet);
            }
            else
            {
                
            }
        }

        public void SendDisconnect()
        {
            _isRunning = false;
            _listener.Stop();
        }
    }
}
