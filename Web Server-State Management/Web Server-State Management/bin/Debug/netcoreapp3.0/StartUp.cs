using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web_Server_State_Management
{
    public class StartUp
    {
        private const string NewLine = "\r\n";

        private static Dictionary<string, int> Sessions = new Dictionary<string, int>();

        public static async Task Main()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                Task.Run(() => ProcessClientAsync(tcpClient));
            }
        }

        private static async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using NetworkStream networkStream = tcpClient.GetStream();

            byte[] requestBytes = new byte[1000];
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string requestMessage = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

            Console.WriteLine("This is request message:");
            Console.WriteLine(requestMessage);

            var sid = Regex.Match(requestMessage, @"sid=[^\n]*\n").Value?.Replace("sid=", string.Empty).Trim();
            Console.WriteLine(sid);

            var newSessionId = Guid.NewGuid().ToString();
            var count = 0;

            if (Sessions.ContainsKey(sid))
            {
                Sessions[sid]++;
                count = Sessions[sid];
            }
            else
            {
                sid = null;
                Sessions[newSessionId] = 1;
                count = 1;
            }

            string responseText =count + NewLine + "<h1> Some text </h1>" + "<h1>" + DateTime.Now + "</h1>";
            string response = "HTTP/1.1 200 OK" + NewLine +
                "Server: Yolito" + NewLine +
                "Content-Type: text/html" + NewLine +
                 "Set-Cookie: user=Yolito; Max-Age: 3600; Security; HttpOnly;" + NewLine +
                              (string.IsNullOrWhiteSpace(sid) ?
                                ("Set-Cookie: sid=" + newSessionId + NewLine)
                                : string.Empty) +
                "Content-Lenght: " + responseText.Length + NewLine
                + NewLine +
                responseText;
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
