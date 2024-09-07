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
    public partial class MainMenu : Form
    {
        Login FormLogin = new Login();
        Login2 FormLogin2 = new Login2();
        Testing FormTest = new Testing();

        public MainMenu()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClosingEventCancel_Closing);
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {          
        }

        private void FormClosingEventCancel_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Yakin ingin keluar ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void pengenalanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTest.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Yakin ingin keluar ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pelatihanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormLogin.Show();
            this.Hide();
        }

        private void dataInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin2.Show();
            this.Hide();
        }     
    }
}
