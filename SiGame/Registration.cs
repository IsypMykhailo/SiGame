using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace SiGame
{
    public partial class Registration : Form
    {
        DBSiGameEntities db;
        //bool UserAdded;
        public Registration()
        {
            InitializeComponent();
            //UserAdded = false;
            db = new DBSiGameEntities(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ПРОГРАММИРОВАНИЕ\Программирование\Курсовая\SiGame\SiGame\DBSiGame.mdf;Integrated Security=True");
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {            
            if (IsValidEmail(txbEmail.Text))
            {
                if (txbPassword.Text == txbConfirmPassword.Text)
                {
                    Users user = new Users()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = txbFirstName.Text,
                        LastName = txbLastName.Text,
                        Email = txbEmail.Text,
                        Username = txbUsername.Text,
                        Password = txbPassword.Text,
                        Status = cbStatus.ValueMember
                    };

                    var checkUsername = (from u in db.Users
                                where txbUsername.Text == u.Username
                                select u).ToList();
                    var checkEmail = (from u in db.Users
                                      where txbEmail.Text == u.Email
                                      select u).ToList();
                    if (checkUsername.Count == 0 && checkEmail.Count == 0)
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        this.Close();
                    }
                    else if (checkUsername.Count != 0 && checkEmail.Count == 0)
                    {
                        MessageBox.Show("Such Username exists", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if(checkEmail.Count != 0 && checkUsername.Count == 0)
                    {
                        MessageBox.Show("Such Email exists", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Such Username and Email exists", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            cbStatus.BackColor = Color.FromArgb(51, 102, 153);
            cbStatus.ForeColor = Color.White;
        }
    }
}
