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
    public partial class Loading : Form
    {
        int n;
        public Loading()
        {
            InitializeComponent();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            n++;
            if(n%10 == 0)
                lblTime.Text = "Time: " + n/10;
            lblSearch.Text += ".";
            if(lblSearch.Text == "Searching for opponent....")
            {
                lblSearch.Text = "Searching for opponent";
            }
        }
    }
}
