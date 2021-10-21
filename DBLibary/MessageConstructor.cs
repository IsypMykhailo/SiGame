using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibary
{
    [Serializable]
    public class MessageConstructor
    {
        public MessageType Type { get; set; }
        public object Content { get; set; }
        public MessageConstructor(object content, MessageType type)
        {
            Content = content;
            Type = type;
        }
    }
    public enum MessageType
    {
        User,
        String,
        UserList
    }
}
