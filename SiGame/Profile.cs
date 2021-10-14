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
    public partial class Profile : Form
    {
        //string Username, Password;
        Users currentUser;
        
        public Profile(Users user)
        {
            InitializeComponent();
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

        }
    }
}
