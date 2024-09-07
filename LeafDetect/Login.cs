using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LeafDetect
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu FormMenu = new MainMenu();
            FormMenu.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "" && txtPass.Text == "")
            {
                MessageBox.Show("Username & Password masih kosong !!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
                txtPass.Focus();
            }
            else if(txtUser.Text == "")
            {
                MessageBox.Show("Username masih kosong !!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPass.Text == "")
            {
                MessageBox.Show("Password masih kosong !!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtUser.Text == "admin" && txtPass.Text == "admin")
                {
                    MessageBox.Show("Login, Berhasil !!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    Training FormLatih = new Training();
                    FormLatih.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username & Password, Salah !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
