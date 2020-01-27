using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpProtocol
{
    public class StartUp
    {
        public static async Task Main()
        {
            //Http forms
            const string NewLine = "\r\n";

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 3333);
            tcpListener.Start();


            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                using NetworkStream networkStream = tcpClient.GetStream();

                byte[] buffer = new byte[10000000];

                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string responseText = "<form action='/Account/Login' method='post'> " +
                                      "Username: <input type='text' name='username' /> " + 
                                      "Password: <input type='password' name='password' /> " + 
                                      "Date: <input type='date' name='date' />" +
                                      "<input type='submit' value='Login' /> </form>";

                string response = "HTTP/1.1 200 OK" + NewLine +
                    "Server: Yolito/1.0" + NewLine +
                    "Content-Type: text/html" + NewLine +
                    "Content-Lenght: " + responseText.Length + NewLine +
                    NewLine +
                    responseText;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                networkStream.Write(responseBytes, 0, responseBytes.Length);

                Console.WriteLine(request);
                Console.WriteLine(new string('=', 60));
            }


        }

        //Response PDF
        public static async Task HttpPDFResponse()
        {
            const string NewLine = "\r\n";

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 3333);
            tcpListener.Start();


            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                using NetworkStream networkStream = tcpClient.GetStream();

                byte[] buffer = new byte[10000000];

                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string responseText = "<h1>Hello Header!</h1>";
                string response = "HTTP/1.1 200 OK" + NewLine +
                    "Server: Yolito/1.0" + NewLine +
                    "Content-Type: text/html" + NewLine +
                    "Content-Disposition: attachment; filename=Index.pdf" + NewLine +
                    "Content-Lenght: " + responseText.Length + NewLine +
                    NewLine +
                    responseText;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                networkStream.Write(responseBytes, 0, responseBytes.Length);

                Console.WriteLine(request);
                Console.WriteLine(new string('=', 60));
            }
        }

        //Response text
        public static async Task HttpResponse()
        {
            const string NewLine = "\r\n";

            //Create and open port
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 3333);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                using NetworkStream networkStream = tcpClient.GetStream();

                byte[] buffer = new byte[10000000];

                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string responseText = "<h1>Hello Header!</h1>";

                string response = "HTTP/1.1 200 OK" + NewLine +
                                  "Server: Yolito/1.0" + NewLine +
                                  "Content-Type: text/html" + NewLine +
                                  "Content-Lenght: " + responseText.Length + NewLine +
                                   NewLine +
                                   responseText;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                networkStream.Write(responseBytes, 0, responseBytes.Length);

                Console.WriteLine(request);
                Console.WriteLine(new string('=', 60));
            }
        }

        public static async Task HttpRequest()
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync("https://softuni.bg/trainings/2613/csharp-web-basics-january-2020#lesson-14147");

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }
    }
}
