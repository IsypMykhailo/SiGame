using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DBLibary;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Sockets;

namespace SiGame
{
    public partial class Autorization : Form
    {
        TcpConnect client;
        int timerCount = 0;
        //DBSiGameEntities db;
        //string Username, Password;
        Users currentUser;
        public Autorization()
        {
            InitializeComponent();
            timer1.Interval = 100;
            client = new TcpConnect("127.0.0.1", 1000);
            currentUser = new Users();
            client.Connect();
            
            //db = new DBSiGameEntities(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\36126\Desktop\SiGameRepo\SiGame\DBSiGame.mdf;Integrated Security=True");
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            label3.ForeColor = Color.DarkMagenta;
            label3.Location = new Point(216, 400);
        }

        private void label3_MouseUp(object sender, MouseEventArgs e)
        {
            label3.Location = new Point(216, 398);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerCount++;
            if(timerCount == 1)
            {
                timer1.Stop();
                //client.SendMessage("Registration starts", MessageType.String);
                this.Hide();
                new Registration(client).ShowDialog();
                this.Show();
                //client.SendMessage("Registration finished", MessageType.String);
            }
        }

        private void Autorization_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(51, 102, 153);
            this.ForeColor = Color.White;
            btnSignIn.BackColor = Color.FromArgb(51, 102, 153);
            txbLogin.BackColor = Color.FromArgb(51, 102, 153);
            txbLogin.ForeColor = Color.White;
            txbPassword.BackColor = Color.FromArgb(51, 102, 153);
            txbPassword.ForeColor = Color.White;
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void CheckUser()
        {
            //string answer = client.Read().TrimEnd('\0');
            string answer = client.ReadMessage().ToString().TrimEnd('\0');
            if (answer == "User valid")
            {
                //currentUser = client.ReadUser();
                currentUser = client.ReadMessage() as Users;
                if(currentUser.Status == "User")
                {
                    this.Hide();
                    new Menu(currentUser, client).ShowDialog();
                    this.Close();
                }
                else if(currentUser.Status == "Admin")
                {
                    this.Hide();
                    new AdminPanel(client).ShowDialog();
                    this.Close();
                }
            }
            else if (answer == "User not valid")
            {
                MessageBox.Show("Error");
            }
        }
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (IsValidEmail(txbLogin.Text))
            {
                client.SendMessage("Autorization", MessageType.String);
                currentUser.Email = txbLogin.Text;
                currentUser.Password = txbPassword.Text;
                client.SendMessage(currentUser, MessageType.User);
                //client.SendUser(currentUser);
                CheckUser();
            }
            else if (!IsValidEmail(txbLogin.Text))
            {
                client.SendMessage("Autorization", MessageType.String);
                currentUser.Username = txbLogin.Text;
                currentUser.Password = txbPassword.Text;
                client.SendMessage(currentUser, MessageType.User);
                //client.SendUser(currentUser);
                CheckUser();
            }
        }
    }
}
