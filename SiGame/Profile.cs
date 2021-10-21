using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBLibary;

namespace SiGame
{
    public partial class Profile : Form
    {
        //string Username, Password;
        Users currentUser;
        //DBSiGameEntities db;
        TcpConnect currentClient;
        public Profile(Users user, TcpConnect client)
        {
            InitializeComponent();
            //db = db_;
            currentClient = client;
            currentUser = user;
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(51, 102, 153);
            this.ForeColor = Color.White;
            btnSaveChanges.BackColor = Color.FromArgb(51, 102, 153);
            edUsername.Text = currentUser.Username;
            edFirstName.Text = currentUser.FirstName;
            edLastName.Text = currentUser.LastName;
            edEmail.Text = currentUser.Email;
            edPassword.Text = currentUser.Password;
            edStatus.Text = currentUser.Status;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //Users user = db.Users.FirstOrDefault((u)=>u.Username == currentUser.Username);
            Users user = new Users()
            {
                Id = Guid.NewGuid(),
                Username = currentUser.Username,
                FirstName = edFirstName.Text,
                LastName = edLastName.Text,
                Email = currentUser.Email,
                Password = edPassword.Text,
                Status = edStatus.Text
            };
            currentClient.SendMessage(user, MessageType.User);
            //currentClient.SendUser(user);
            string answer = currentClient.ReadMessage().ToString();
            /*if (user != null)
            {
                user.FirstName = edFirstName.Text;
                user.LastName = edLastName.Text;
                user.Password = edPassword.Text;
                user.Status = edStatus.Text;
                db.SaveChanges();
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Error");
            }*/
        }
    }    
}
