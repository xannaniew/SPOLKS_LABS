using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Server
{
    public class Server
    {
        private IParser parser;
        private TcpListener listener;
        IPAddress serverAddress;
        public Server(IParser Parser, IPAddress Ip, int Port)
        {
            try
            {
                parser = Parser;
                listener = new TcpListener(Ip, Port);
                listener.Start();
                Console.WriteLine($"Server started on {Ip}:{Port}");
                GetConnections();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    listener.Stop();
                }
                catch
                {

                }
            }
        }

        private async void GetConnections()
        {
            while (true)
            {
                var client = listener.AcceptTcpClient();
                Console.WriteLine($"Client connected {((IPEndPoint)client.Client.RemoteEndPoint).Address}");
                Task.Run(() => HandleConnection(client));
            }
        }

        private void HandleConnection(TcpClient client)
        {
            var writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;
            var reader = new StreamReader(client.GetStream());
            writer.WriteLine("Hi");

            //while (client.Client.Poll(1000000, SelectMode.SelectRead))
            while(client.Connected)
            {
                var request = reader.ReadLine();
                Console.WriteLine($"request {request} came");
                var command = parser.Parse(request);
                
                switch (command.CommandName)
                {
                    case "ECHO":
                        {
                            var response = new StringBuilder();
                            foreach(var arg in command.Arguments)
                            {
                                response.Append(arg + " ");

                            }
                            response.Remove(response.Length - 1, 1);

                            writer.WriteLine(response.ToString());
                            break;
                        }
                    case "TIME":
                        {
                            writer.WriteLine(DateTime.Now.ToString());
                            break;
                        }
                    case "CLOSE":
                        {
                            listener.Stop();
                            break;
                        }
                    default:
                        {
                            writer.WriteLine("What?");
                            break;
                        }
                }
            }

            client.Close();
        }

    }
}
