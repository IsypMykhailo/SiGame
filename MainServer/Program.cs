using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
            int len = 0;
            byte[] buf = new byte[128];
            StringBuilder sb = new StringBuilder();
            do
            {
                try
                {

                    NetworkStream stream = client1.GetStream();
                    sb.Clear();
                    do
                    {
                        len = stream.Read(buf, 0, buf.Length);
                        sb.Append(Encoding.UTF8.GetString(buf, 0, len));
                    } while (stream.DataAvailable);
                    Console.WriteLine(sb.ToString());
                    if(sb.ToString()!="close")
                    SendMessage();
                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            } while (sb.ToString() != "close");
            listener.Stop();
            //System.Windows.Forms.MessageBox.Show("Server stop");
            Console.WriteLine("Server stop");
        }
        static void SendMessage()
        {

            ReceiveMessage();
        }
        static void Main(string[] args)
        {
            Connect();
            client1.Close();
            client2.Close();
            listener.Stop();
        }
        static void Connect()
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            listener.Start();
            Console.WriteLine("Server start");
            try
            {
                client1 = listener.AcceptTcpClient();
                Console.WriteLine("1 client connection");

                client2 = listener.AcceptTcpClient();
                Console.WriteLine("2 client connection");

                Console.WriteLine("Game ready");
                /*byte[] buf = Encoding.UTF8.GetBytes("Ready");
                client1.GetStream().Write(buf, 0, buf.Length);
                client2.GetStream().Write(buf, 0, buf.Length);*/
                Send("Ready");
                ReceiveMessage();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        static byte[] toByteArray(string s) => Encoding.UTF8.GetBytes(s);
        static void Send(string message)
        {
            if (client1 == null || client2 == null)
                return;
            
            byte[] b = toByteArray(message);
            client1.GetStream().Write(b, 0, b.Length);
            client2.GetStream().Write(b, 0, b.Length);
        }
    }
}
