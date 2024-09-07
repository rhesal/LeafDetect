using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LeafDetect
{
    public partial class FormInfo : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;

        string save_action;

        public FormInfo()
        {
            InitializeComponent();
            ConnectDb();
            Set_ComboBox();
            btnSimpan.Enabled = false;
            btnBatal.Enabled = false;
        }

        private void FormInfo_Load(object sender, EventArgs e)
        {
            FormInfo forminfo = new FormInfo();
            forminfo.Refresh();
        }

        private void ConnectDb()
        {
            try
            {
                conn = new OleDbConnection();
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataSet.accdb;Persist Security Info = false";
                conn.Open();
                //listBox1.Items.Add("Database terkoneksi");
                cmd = conn.CreateCommand();
            }
            catch (Exception)
            {
                //listBox1.Items.Add("ERROR : Database gagal terkoneksi");
            }
        }

        private void Set_ComboBox()
        {
            OleDbDataReader read = null;
            try
            {
                conn.Close();
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                string query = "SELECT jenis FROM Informasi";
                cmd.CommandText = query;
                read = cmd.ExecuteReader();
                comboBox1.Items.Clear();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        comboBox1.Items.Add(read["jenis"].ToString());
                    }
                }
                comboBox1.Update();
                comboBox1.Refresh();
                conn.Close();
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error : " + ex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataReader rd = null;
            try
            {
                conn.Close();
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                string query = "SELECT * FROM informasi WHERE jenis='" + comboBox1.Text + "'";
                cmd.CommandText = query;
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["ID"].ToString();
                    txtJenis.Text = rd["jenis"].ToString();
                    txtLatin.Text = rd["latin"].ToString();
                    txtKhasiat.Text = rd["informasi"].ToString();
                }
                comboBox1.Update();
                comboBox1.Refresh();
                conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            txtJenis.Visible = true;
            btnBatal.Visible = true;
            comboBox1.Visible = false;
            btnHapus.Visible = false;

            txtJenis.Text = "";
            txtLatin.Text = "";
            txtKhasiat.Text = "";

            save_action = "add";
            btnSimpan.Enabled = true;
            btnBatal.Enabled = true;
            btnTambah.Enabled = false;
            btnUbah.Enabled = false;
            btnHapus.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtJenis.Visible = true;
            btnBatal.Visible = true;
            comboBox1.Visible = false;
            btnHapus.Visible = false;

            save_action = "edit";
            txtLatin.Enabled = true;
            txtKhasiat.Enabled = true;
            btnSimpan.Enabled = true;
            btnBatal.Enabled = true;
            btnTambah.Enabled = false;
            btnUbah.Enabled = false;
            btnHapus.Enabled = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            txtJenis.Visible = false;
            comboBox1.Visible = true;

            if (save_action == "add")
            {
                if (txtLatin.Text != "" && txtKhasiat.Text != "")
                {
                    try
                    {

                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO Informasi (jenis, latin, informasi) VALUES ('" + txtJenis.Text + "','" + txtLatin.Text + "','" + txtKhasiat.Text + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        txtLatin.Text = "";
                        txtKhasiat.Text = "";
                        this.Refresh();
                        this.Update();
                        Set_ComboBox();
                        btnTambah.Enabled = true;
                        btnHapus.Enabled = true;
                        btnBatal.Enabled = false;
                        txtLatin.Enabled = false;
                        txtKhasiat.Enabled = false;
                        comboBox1.Visible = true;
                        btnHapus.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex);
                    }
                }
                else
                    MessageBox.Show("Data masih kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (save_action == "edit")
            {
                try
                {
                    if (txtId.Text != "")
                    {
                        conn.Close();
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        string query = cmd.CommandText = "UPDATE Informasi SET jenis = '"+ txtJenis.Text +"'," 
                                                         + "latin = '"+ txtLatin.Text +"',"
                                                         + "informasi = '"+txtKhasiat.Text +"'"
                                                         + "WHERE ID = " + txtId.Text + "";
                        cmd.CommandText = query;

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        txtLatin.Text = "";
                        txtKhasiat.Text = "";
                        this.Refresh();
                        this.Update();
                        Set_ComboBox();
                        btnTambah.Enabled = true;
                        btnHapus.Enabled = true;
                        btnUbah.Enabled = true;
                        btnBatal.Enabled = false;
                        txtLatin.Enabled = false;
                        txtKhasiat.Enabled = false;
                        comboBox1.Visible = true;
                        btnHapus.Visible = true;
                    }
                    else
                        MessageBox.Show("Pilih data Dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Error : " + ex);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    conn.Close();
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    string query = "DELETE FROM Informasi WHERE ID=" + txtId.Text + "";
                    //MessageBox.Show(query);
                    cmd.CommandText = query;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    Set_ComboBox();
                    txtId.Text = "";
                    txtLatin.Text = "";
                    txtKhasiat.Text = "";
                }
                else
                    MessageBox.Show("Pilih data dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error : " + ex);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            
            btnTambah.Enabled = true;
            btnHapus.Enabled = true;
            comboBox1.Visible = true;
            btnHapus.Visible = true;
            btnBatal.Visible = false;
            btnBatal.Enabled = false;
            btnSimpan.Enabled = false;
            txtLatin.Enabled = false;
            txtKhasiat.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu FormMenu = new MainMenu();
            FormMenu.Show();
        }

        
    }
}
