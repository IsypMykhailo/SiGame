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

namespace SiGame
{
    public partial class Autorization : Form
    {
        int timerCount = 0;
        DBSiGameEntities db;
        //string Username, Password;
        Users currentUser;
        public Autorization()
        {
            InitializeComponent();
            timer1.Interval = 100;
            db = new DBSiGameEntities(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ПРОГРАММИРОВАНИЕ\Программирование\Курсовая\SiGame\SiGame\DBSiGame.mdf;Integrated Security=True");
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
                this.Hide();
                new Registration().ShowDialog();
                this.Show();
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

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            List<Users> users = db.Users.ToList();
            var searchUser = (from u in users
                             where txbLogin.Text == u.Username && txbPassword.Text == u.Password || txbLogin.Text == u.Email && txbPassword.Text == u.Password
                             select u).ToList();
            if(searchUser.Count > 0)
            {
                currentUser = searchUser[0];
                /*Username = searchUser[0].Username;
                Password = searchUser[0].Password;*/
                //MessageBox.Show("Success");
                this.Hide();
                new Menu(currentUser).ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Login or Password", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
