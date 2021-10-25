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
        static List<Users> connectedUsers = new List<Users>();
        static string clientAnswer;
        static DBSiGameEntities db;


        static void SendMessage(object content, MessageType type, TcpClient client)
        {
            MessageConstructor constructor = new MessageConstructor(content, type);
            if (client == null)
                return;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, constructor);
        }

        static object ReadMessage(TcpClient client)
        {
            if (client == null)
                return null;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            MessageConstructor message = bf.Deserialize(ns) as MessageConstructor;
            if (message.Type == MessageType.String)
            {
                string answer = message.Content.ToString();
                return answer;
            }
            else if (message.Type == MessageType.User)
            {
                Users user = message.Content as Users;
                return user;
            }
            else if (message.Type == MessageType.UserList)
            {
                List<Users> users = message.Content as List<Users>;
                return users;
            }
            return null;
        }


        #region old2
        /*static string ReadMessage(TcpClient client)
        {
            if (client == null)
                return "";
            string answer;
            int len = 0;
            byte[] buf = new byte[128];
            StringBuilder sb = new StringBuilder();
            do
            {
                try
                {

                    NetworkStream stream = client.GetStream();
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
        #region old
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
        #endregion
        static void SendUserList(TcpClient client)
        {
            if (client == null)
                return;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, db.Users.ToList());
        }*/
        #endregion

        static void Main(string[] args)
        {
            db = new DBSiGameEntities(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ПРОГРАММИРОВАНИЕ\Программирование\Курсовая\SiGameRepo\MainServer\DBSiGame.mdf;Integrated Security=True;Connect Timeout=30");
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
            //listener.Start();

            Console.WriteLine("Server start");
            Thread thread1 = new Thread(Connect);
            Thread thread2 = new Thread(Connect);
            thread1.Start();
            thread2.Start();
            //Console.WriteLine("Game ready");

            //listener.Stop();
            Console.ReadLine();
        }
        static void Autorize(TcpClient client)
        {
            Users user1 = ReadMessage(client) as Users;
            if (IsValidUser(user1))
            {
                SendMessage("User valid", MessageType.String, client);
            }
            else if (!IsValidUser(user1))
            {
                SendMessage("User not valid", MessageType.String, client);
            }
            Console.WriteLine("1 client connection");
            user1 = SearchUser(user1);
            SendMessage(user1, MessageType.User, client);
            if (user1.Status == "Admin")
            {
                SendMessage(db.Users.ToList(), MessageType.UserList, client);
                NetworkStream stream = client.GetStream();
                int len = 0;
                byte[] buf = new byte[1024];
                StringBuilder sb = new StringBuilder();
                do
                {
                    sb.Clear();
                    do
                    {
                        len = stream.Read(buf, 0, buf.Length);
                        sb.Append(Encoding.UTF8.GetString(buf, 0, len));
                    } while (stream.DataAvailable);

                    Console.WriteLine($"Client: {client.Client.RemoteEndPoint}, Message: {sb}");

                    buf = Encoding.UTF8.GetBytes("Got it!");
                    stream.Write(buf, 0, buf.Length);
                } while (clientAnswer == "close");
            }
            else if (user1.Status == "User")
            {
                NetworkStream stream = client.GetStream();
                int len = 0;
                byte[] buf = new byte[1024];
                StringBuilder sb = new StringBuilder();
                do
                {
                    sb.Clear();
                    do
                    {
                        len = stream.Read(buf, 0, buf.Length);
                        sb.Append(Encoding.UTF8.GetString(buf, 0, len));
                    } while (stream.DataAvailable);
                    clientAnswer = sb.ToString();
                    Console.WriteLine($"Client: {client.Client.RemoteEndPoint}, Message: {sb}");

                } while (clientAnswer != "start");
            }
            if (clientAnswer == "start")
            {
                connectedUsers.Add(user1);
                if (connectedUsers.Count == 2)
                {
                    SendMessage("Ready", MessageType.String, client);
                    Console.WriteLine("Game Ready");
                }
            }
            clientAnswer = null;
        }

        static void Register(TcpClient client)
        {
            do
            {
                Users user = ReadMessage(client) as Users;
                var checkUsername = (from u in db.Users
                                     where user.Username == u.Username
                                     select u).ToList();
                var checkEmail = (from u in db.Users
                                  where user.Email == u.Email
                                  select u).ToList();
                if (checkUsername.Count == 0 && checkEmail.Count == 0)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    SendMessage("Complete", MessageType.String, client);
                }
                else if (checkUsername.Count != 0 && checkEmail.Count == 0)
                {
                    SendMessage("Such Username exists", MessageType.String, client);
                }
                else if (checkEmail.Count != 0 && checkUsername.Count == 0)
                {
                    SendMessage("Such Email exists", MessageType.String, client);
                }
                else
                {
                    SendMessage("Such Username and Email exists", MessageType.String, client);
                }
                clientAnswer = ReadMessage(client).ToString();
            } while (clientAnswer == "Registration finished");

        }

        static void Connect()
        {
            TcpClient currentClient;
            listener.Start();
            
            try
            {
                currentClient = listener.AcceptTcpClient();
                do
                {                    
                    clientAnswer = ReadMessage(currentClient).ToString();
                    if (clientAnswer == "Registration")
                        Register(currentClient);
                    else if (clientAnswer == "Autorization")
                        Autorize(currentClient);                    
                } while (clientAnswer != "close");
                currentClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                listener.Stop();
            }
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
