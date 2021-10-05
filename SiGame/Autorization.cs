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
        public Autorization()
        {
            InitializeComponent();
            timer1.Interval = 100;
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
    }
}
