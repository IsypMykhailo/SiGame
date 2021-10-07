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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
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

        }

        private void lblJoin_Click(object sender, EventArgs e)
        {

        }
    }
}
