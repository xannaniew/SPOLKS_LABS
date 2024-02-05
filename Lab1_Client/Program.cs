using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab1_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();
            Console.WriteLine("Ip: ");
            string ip = Console.ReadLine();
            Console.WriteLine("port: ");
            string port = Console.ReadLine();

            tcpClient.Connect(IPAddress.Parse(ip), int.Parse(port));

            var writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
            //var reader = new StreamReader(tcpClient.GetStream());
            //while (true)
            //{

            //}
            Task.Run(() => Receive(tcpClient));
            while (true)
            {
                writer.WriteLine(Console.ReadLine());
            }

        }

        static Task Receive(TcpClient client)
        {
            var reader = new StreamReader(client.GetStream());
            while (true)
            {
                var responce = reader.ReadLine();
                Console.WriteLine(responce);
            }
        }
    }
}