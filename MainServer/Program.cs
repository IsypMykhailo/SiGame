using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MainServer
{
    class Program
    {
        static int port = 1000;
        static string ip = "127.0.0.1";
        static TcpListener listener;
        static TcpClient client1;
        static TcpClient client2;
        //static List<TcpClient> clients;
        static void ReceiveMessage()
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            listener.Start();
            Console.WriteLine("Server start");
            int len = 0;
            byte[] buf = new byte[128];
            StringBuilder sb = new StringBuilder();
            do
            {
                try
                {                    
                    client1 = listener.AcceptTcpClient();
                    Console.WriteLine("1 client connection");

                    client2 = listener.AcceptTcpClient();
                    Console.WriteLine("2 client connection");

                    Console.WriteLine("Game ready");
                    NetworkStream stream = client1.GetStream();
                    sb.Clear();
                    do
                    {
                        len = stream.Read(buf, 0, buf.Length);
                        sb.Append(Encoding.UTF8.GetString(buf, 0, len));
                    } while (stream.DataAvailable);
                    Console.WriteLine(sb.ToString());
                    SendMessage();
                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            } while (sb.ToString().ToLower() != "close");
            listener.Stop();
            //System.Windows.Forms.MessageBox.Show("Server stop");
            Console.WriteLine("Server stop");
        }
        static void SendMessage()
        {

        }
        static void Main(string[] args)
        {
            ReceiveMessage();
        }
    }
}
