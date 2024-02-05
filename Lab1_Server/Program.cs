using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Lab1_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPAddress[] addrs = Dns.GetHostAddresses(host); 
            var parser = new BaseParser();
            var server = new Server(parser, addrs.First(addr => addr.AddressFamily == AddressFamily.InterNetwork), 8888);
            //var server = new Server(parser, IPAddress.Any, 8888);
        }

    }
}