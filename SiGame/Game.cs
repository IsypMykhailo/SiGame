using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiGame
{
    public partial class Game : Form
    {
        TcpConnect client;
        //string Username, Password;
        Users currentUser;
        public Game(TcpConnect client_, Users user)
        {
            InitializeComponent();
            client = client_;
            FormClosing += (s, a) =>
            {
                client.Send("close");
                if (client != null)
                    client.Disconnect();
            };
            currentUser = user;
        }

        private void Game_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(51, 102, 153);
            this.ForeColor = Color.White;
        }
    }
}
