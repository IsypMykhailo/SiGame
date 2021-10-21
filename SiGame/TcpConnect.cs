using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DBLibary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SiGame
{
    public class TcpConnect
    {
        IPAddress ipAddress;
        int remotePort;
        TcpClient client;
        //MessageConstructor constructor;
        public TcpConnect(string ip, int port)
        {
            ipAddress = IPAddress.Parse(ip);
            remotePort = port;
            client = null;
        }
        public bool Connect()
        {
            if (client != null)
                return true;

            //try
            //{
            client = new TcpClient();
            client.Connect(new IPEndPoint(ipAddress, remotePort));

            return true;
            //}
            //catch (Exception)
            //{
            //return false;
            //}
        }
        public void Disconnect()
        {
            if (client == null)
                return;

            try
            {
                //Send("close");
                SendMessage("close", MessageType.String);
                System.Threading.Thread.Sleep(100);
                client.Close();
            }
            finally
            {
                client = null;
            }
        }
        #region old
        /*private byte[] toByteArray(string s) => Encoding.UTF8.GetBytes(s);
        private string toStringFromByte(byte[] b) => Encoding.UTF8.GetString(b);
        public void Send(string message)
        {
            if (client == null)
                return;

            byte[] b = toByteArray(message);
            client.GetStream().Write(b, 0, b.Length);
        }
        public void SendUser(Users user)
        {
            if (client == null)
                return;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, user);
        }
        public Users ReadUser()
        {
            if (client == null)
                return null;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            Users userDeserealize = bf.Deserialize(ns) as Users;
            return userDeserealize;
        }
        public List<Users> ReadUserList()
        {
            if (client == null)
                return null;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            List<Users> userDeserealize = bf.Deserialize(ns) as List<Users>;
            return userDeserealize;
        }
        public string Read()
        {
            if (client == null)
                return string.Empty;

            try
            {
                int len = 0;
                byte[] buf = new byte[1024];
                NetworkStream stream = client.GetStream();
                StringBuilder sb = new StringBuilder();
                do
                {
                    len = stream.Read(buf, 0, buf.Length);
                    sb.Append(toStringFromByte(buf));
                } while (stream.DataAvailable);
                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }*/
        #endregion

        public void SendMessage(object content, MessageType type)
        {
            MessageConstructor constructor = new MessageConstructor(content, type);
            if (client == null)
                return;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ns, constructor);
        }
        
        public object ReadMessage()
        {
            if (client == null)
                return null;
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            MessageConstructor message = bf.Deserialize(ns) as MessageConstructor;
            if(message.Type == MessageType.String)
            {
                string answer = message.Content.ToString();
                return answer;
            }
            else if(message.Type == MessageType.User)
            {
                Users user = message.Content as Users;
                return user;
            }
            else if(message.Type == MessageType.UserList)
            {
                List<Users> users = message.Content as List<Users>;
                return users;
            }
            return null;
        }
        
        public event Action<string> ReadAsyncMessage;
        public void ReadAsync()
        {
            ReadAsyncMessage?.Invoke(ReadMessage().ToString());
            //Task.Run(Read);
        }
    }
}
