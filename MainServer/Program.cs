using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DBLibary;
using System.Runtime.Serialization.Formatters.Binary;

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
        static DBSiGameEntities db;
        static string ReadMessage()
        {
            string answer;
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
                    answer = sb.ToString();
                    return answer;
                }
                catch (Exception ex)
                {                    
                    Console.WriteLine(ex.Message);
                    return "";
                }
            } while (sb.ToString() != "close");
        }
        static Users ReadUser(TcpClient client)
        {
            if (client == null)
                return null;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            Users userDeserealize = bf.Deserialize(ns) as Users;
            return userDeserealize;
        }
        #region old
        /*static void ReceiveMessage()
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
        }*/
        #endregion
        static void SendUserList(TcpClient client)
        {
            if (client == null)
                return;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, db.Users.ToList());
        }
        static void Main(string[] args)
        {
            db = new DBSiGameEntities(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\36126\Desktop\SiGameRepo\MainServer\DBSiGame.mdf;Integrated Security=True;Connect Timeout=30");
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
                Users user1 = ReadUser(client1);
                if (IsValidUser(user1))
                {
                    Send("User valid", client1);
                }
                else if (!IsValidUser(user1))
                {
                    Send("User not valid", client1);
                }
                Console.WriteLine("1 client connection");                
                user1 = SearchUser(user1);                
                SendUser(user1, client1);
                if(user1.Status == "Admin")
                {
                    SendUserList(client1);
                }

                client2 = listener.AcceptTcpClient();
                Users user2 = ReadUser(client2);
                if (IsValidUser(user2))
                {
                    Send("User valid", client2);
                }
                else if (!IsValidUser(user2))
                {
                    Send("User not valid", client2);
                }
                Console.WriteLine("2 client connection");
                user2 = SearchUser(user2);
                SendUser(user2, client2);

                Console.WriteLine("Game ready");
                /*byte[] buf = Encoding.UTF8.GetBytes("Ready");
                client1.GetStream().Write(buf, 0, buf.Length);
                client2.GetStream().Write(buf, 0, buf.Length);*/
                string answer = ReadMessage().TrimEnd('\0');
                Send("Ready", client1);
                Send("Ready", client2);
                //ReceiveMessage();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        static void SendUser(Users user, TcpClient client)
        {
            if (client == null)
                return;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, user);
        }
        static byte[] toByteArray(string s) => Encoding.UTF8.GetBytes(s);
        static void Send(string message, TcpClient client)
        {
            if (client == null)
                return;
            
            byte[] b = toByteArray(message);
            client.GetStream().Write(b, 0, b.Length);
        }
        static bool IsValidUser(Users user)
        {
            if (SearchUser(user) == null)
                return false;
            else if (SearchUser(user) != null)
                return true;
            else            
                return false;            
        }
        static Users SearchUser(Users user)
        {
            List<Users> users = db.Users.ToList();
            var searchUser = (from u in users
                              where user.Username == u.Username && user.Password == u.Password || user.Email == u.Email && user.Password == u.Password
                              select u).ToList();
            if (searchUser.Count > 0)
            {                
                return searchUser[0];
            }
            else
            {
                return null;
            }
        }
    }
}
