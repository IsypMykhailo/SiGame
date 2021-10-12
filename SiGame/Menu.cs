using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SiGame
{
    public partial class Menu : Form
    {
        TcpConnect client;
        Loading loginForm;
        public Menu()
        {
            InitializeComponent();
            //client = null;
            FormClosing += (s, a) =>
            {
                if (client != null)
                    client.Disconnect();
            };
            client = new TcpConnect("127.0.0.1", 1000);
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(51, 102, 153);
            this.ForeColor = Color.White;            
        }

        private void Mouse_Enter(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            if (lbl == null)
                return;

            lbl.Text = ">" + lbl.Tag.ToString() + "<";
            lbl.ForeColor = Color.DarkSeaGreen;
        }

        private void lblCreate_MouseLeave(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            if (lbl == null)
                return;

            lbl.Text = lbl.Tag.ToString();
            lbl.ForeColor = Color.White;
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblCreate_Click(object sender, EventArgs e)
        {
            client.Connect();
            client.ReadMessage += Client_ReadMessage;
            loginForm = new Loading();
            loginForm.Show();                    
        }

        private void Client_ReadMessage(string answer)
        {
            if (answer == "Ready")
            {
                loginForm.Close();
            }
        }
    }
}
