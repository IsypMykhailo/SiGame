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
    public partial class AdminPanel : Form
    {
        //DBSiGameEntities db;
        TcpConnect currentClient;
        public AdminPanel(TcpConnect client)
        {
            InitializeComponent();
            currentClient = client;
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(51, 102, 153);
            dgvUsers.DataSource = currentClient.ReadUserList();//db.Users.ToList();
            dgvUsers.ReadOnly = true;
            dgvUsers.MultiSelect = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnAdd.BackColor = Color.FromArgb(51, 102, 153);
            btnAdd.ForeColor = Color.White;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Registration(currentClient).ShowDialog();
            this.Show();
            dgvUsers.DataSource = currentClient.ReadUserList();
        }
    }
}
