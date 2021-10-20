using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibary
{
    public partial class DBSiGameEntities
    {
        public DBSiGameEntities(string path) : base(path)
        {

        }
        public void AddUser(Users user)
        {
            if (user == null)
                return;
            Users.Add(user);
            SaveChanges();
        }
        public void DeleteUser(Users user)
        {
            if (user == null)
                return;
            Users.Remove(user);
            SaveChanges();
        }
    }
}
