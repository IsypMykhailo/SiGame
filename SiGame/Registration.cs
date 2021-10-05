﻿using System;
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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (IsValidEmail(txbEmail.Text))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Email", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void Registration_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(51, 102, 153);
            this.ForeColor = Color.White;
            btnSignUp.BackColor = Color.FromArgb(51, 102, 153);
            txbUsername.BackColor = Color.FromArgb(51, 102, 153);
            txbUsername.ForeColor = Color.White;
            txbPassword.BackColor = Color.FromArgb(51, 102, 153);
            txbPassword.ForeColor = Color.White;
            txbFirstName.BackColor = Color.FromArgb(51, 102, 153);
            txbFirstName.ForeColor = Color.White;
            txbLastName.BackColor = Color.FromArgb(51, 102, 153);
            txbLastName.ForeColor = Color.White;
            txbEmail.BackColor = Color.FromArgb(51, 102, 153);
            txbEmail.ForeColor = Color.White;
            txbConfirmPassword.BackColor = Color.FromArgb(51, 102, 153);
            txbConfirmPassword.ForeColor = Color.White;
        }
    }
}