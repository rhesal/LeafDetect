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
    public partial class Testing : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        Bitmap img;

        int l, t, lebarMaks, lebarMin, tinggiMaks, tinggiMin;
        int red, green, blue;
        double hue, saturation, falue;

        int counter = 0;
        double rataR, rataG, rataB;
        int jumlahR = 0;
        int jumlahG = 0;
        int jumlahB = 0;

        int medx, medy;
        int sudut45, sudut135, sudut225, sudut315;

        public Testing()
        {
            InitializeComponent();
            ConnectDb();
        }

        private void Testing_Load(object sender, EventArgs e)
        {

        }

        private void ConnectDb()
        {
            try
            {
                conn = new OleDbConnection();
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataSet.accdb;Persist Security Info = false";
                conn.Open();
                listBox1.Items.Add("Database terkoneksi");
                cmd = conn.CreateCommand();
            }
            catch (Exception)
            {
                listBox1.Items.Add("ERROR : Database gagal terkoneksi");
            }
        }

        public void bersih()
        {
            lbJenis.Text = string.Empty;
            txtRed.Text = string.Empty;
            txtGreen.Text = string.Empty;
            txtBlue.Text = string.Empty;
            txtHue.Text = string.Empty;
            txtSaturation.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtLebar.Text = string.Empty;
            txtLmax.Text = string.Empty;
            txtLmin.Text = string.Empty;
            txtTinggi.Text = string.Empty;
            txtTmax.Text = string.Empty;
            txtTmin.Text = string.Empty;
            txtMedX.Text = string.Empty;
            txtMedY.Text = string.Empty;
            txtS45.Text = string.Empty;
            txtS135.Text = string.Empty;
            txtS225.Text = string.Empty;
            txtS315.Text = string.Empty;
            listBox1.Items.Clear();
        }

        private void btnPilih_Click(object sender, EventArgs e)
        {
            bersih();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select an image file.";
            openDialog.Filter = "Jpeg Images(*.jpg)|*.jpg|Png Images(*.png)|*.png|Bitmap Images(*.bmp)|*.bmp";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                img = new Bitmap(new Bitmap(openDialog.FileName), pbGambar.Width, pbGambar.Height);
                pbGambar.Image = img;
                txtKet.Text = openDialog.FileName.ToString();
                pbGambar.ImageLocation = txtKet.Text;
                MessageBox.Show("Open Picture Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            bersih();
            if (pbGambar.Image != null)
            {
                img = new Bitmap(txtKet.Text);
                //progressBar1.Maximum = img.Width * 3;

                int lebar = img.Width;
                int tinggi = img.Height;

                lebarMaks = 0;
                tinggiMaks = 0;
                lebarMin = 1000;
                tinggiMin = 1000;

                for (int i = 0; i < lebar; i++)
                {
                    //progressBar1.Value = progressBar1.Value + 1;
                    int x = i;
                    for (int j = 0; j < tinggi; j++)
                    {
                        int y = j;
                        red = img.GetPixel(x, y).R;
                        green = img.GetPixel(x, y).G;
                        blue = img.GetPixel(x, y).B;

                        if (red < 10)
                        {
                            continue;
                        }
                        else if (green < 30)
                        {
                            continue;
                        }
                        else if (blue < 0)
                        {
                            continue;
                        }
                        else if (red > 150)
                        {
                            continue;
                        }
                        else if (green > 250)
                        {
                            continue;
                        }
                        else if (blue > 50)
                        {
                            continue;
                        }
                        else
                        {
                            counter++;
                        }

                        if (x > lebarMaks)
                            lebarMaks = x;
                        if (x < lebarMin)
                            lebarMin = x;
                        if (y > tinggiMaks)
                            tinggiMaks = y;
                        if (y < tinggiMin)
                            tinggiMin = y;

                        jumlahR = jumlahR + red;
                        jumlahG = jumlahG + green;
                        jumlahB = jumlahB + blue;
                    }

                    listBox1.Items.Add("kolom ke-" + x);
                }

                rataR = jumlahR / counter;
                rataG = jumlahG / counter;
                rataB = jumlahB / counter;

                listBox1.Items.Add("Rata Red : " + rataR);
                txtRed.Text = rataR.ToString();

                listBox1.Items.Add("Rata Green : " + rataG);
                txtGreen.Text = rataG.ToString();

                listBox1.Items.Add("Rata Blue : " + rataB);
                txtBlue.Text = rataB.ToString();

                listBox1.Items.Add("Counter : " + counter);
                listBox1.Items.Add("====================");

                //==========HSV==========

                double rp = rataR / 255;
                double gp = rataG / 255;
                double bp = rataB / 255;
                double cmax = 0;
                double cmin = 0;

                //Vmax & Vmin & Delta

                if ((rp > gp) && (rp > bp))
                {
                    cmax = rp;
                    listBox1.Items.Add("Cmax : " + cmax);
                }
                else if ((gp > bp) && (gp > rp))
                {
                    cmax = gp;
                    listBox1.Items.Add("Cmax : " + cmax);
                }
                else
                {
                    cmax = bp;
                    listBox1.Items.Add("Cmax : " + cmax);
                }

                if ((rp < gp) && (rp < bp))
                {
                    cmin = rp;
                    listBox1.Items.Add("Cmin : " + rp);
                }
                else if ((gp < bp) && (gp < rp))
                {
                    cmin = gp;
                    listBox1.Items.Add("Cmin : " + gp);
                }
                else
                {
                    cmin = bp;
                    listBox1.Items.Add("Cmin : " + bp);
                }

                double delta = cmax - cmin;
                listBox1.Items.Add("Delta : " + delta);

                //Nilai Hue
                if (cmax == rp)
                {
                    double hr = 60 * (((gp - bp) / delta) % 6);
                    int h = Convert.ToInt32(hr);
                    listBox1.Items.Add("Nilai Hue : " + h);
                    txtHue.Text = h.ToString();
                }
                else if (cmax == gp)
                {
                    double hg = 60 * (((bp - rp) / delta) + 2);
                    int h = Convert.ToInt32(hg);
                    listBox1.Items.Add("Nilai Hue : " + h);
                    txtHue.Text = h.ToString();
                }
                else if (cmax == bp)
                {
                    double hb = 60 * (((rp - gp) / delta) + 4);
                    int h = Convert.ToInt32(hb);
                    listBox1.Items.Add("Nilai Hue : " + h);
                    txtHue.Text = h.ToString();
                }

                //Nilai Saturation
                if (cmax == 0)
                {
                    int s = 0;
                    listBox1.Items.Add("Nilai Saturation : " + s);
                    txtSaturation.Text = s.ToString();
                }
                else if (cmax != 0)
                {
                    double sat = (delta / cmax) * 100;
                    int s = Convert.ToInt32(sat);
                    listBox1.Items.Add("Nilai Saturation : " + s);
                    txtSaturation.Text = s.ToString();
                }

                //Nilai Value
                double va = cmax * 100;
                int v = Convert.ToInt32(va);
                listBox1.Items.Add("Nilai Value : " + v);
                txtValue.Text = v.ToString();
                listBox1.Items.Add("====================");

                // ====================================CCD==============================

                txtTmax.Text = tinggiMaks.ToString();
                txtTmin.Text = tinggiMin.ToString();
                txtLmax.Text = lebarMaks.ToString();
                txtLmin.Text = lebarMin.ToString();

                l = lebarMaks - lebarMin; txtLebar.Text = l.ToString();
                t = tinggiMaks - tinggiMin; txtTinggi.Text = t.ToString();
                int medl = (l / 2) + lebarMin; txtMedX.Text = medl.ToString();
                int medt = (t / 2) + tinggiMin; txtMedY.Text = medt.ToString();

                // sudut 45 derajat

                int x45 = medl;
                int y45 = 0;

                for (int ys45 = medt; ys45 > 0; ys45--)
                {
                    int rs = img.GetPixel(x45, ys45).R;
                    int gs = img.GetPixel(x45, ys45).G;
                    int bs = img.GetPixel(x45, ys45).B;

                    if (rs < 10)
                    {
                        continue;
                    }
                    else if (gs < 30)
                    {
                        continue;
                    }
                    else if (bs < 0)
                    {
                        continue;
                    }
                    else if (rs > 150)
                    {
                        continue;
                    }
                    else if (gs > 250)
                    {
                        continue;
                    }
                    else if (bs > 50)
                    {
                        continue;
                    }
                    else
                    {
                        y45 = ys45;
                        x45++;
                    }
                }
                x45 = x45 - 1;

                // sudut 135 derajat

                int x135 = medl;
                int y135 = 0;

                for (int ys135 = medt; ys135 > 0; ys135--)
                {
                    int rs = img.GetPixel(x135, ys135).R;
                    int gs = img.GetPixel(x135, ys135).G;
                    int bs = img.GetPixel(x135, ys135).B;

                    if (rs < 10)
                    {
                        continue;
                    }
                    else if (gs < 30)
                    {
                        continue;
                    }
                    else if (bs < 0)
                    {
                        continue;
                    }
                    else if (rs > 150)
                    {
                        continue;
                    }
                    else if (gs > 250)
                    {
                        continue;
                    }
                    else if (bs > 50)
                    {
                        continue;
                    }
                    else
                    {
                        y135 = ys135;
                        x135--;
                    }
                }
                x135 = x135 + 1;

                // sudut 225 derajat

                int x225 = medl;
                int y225 = 0;

                for (int ys225 = medt; ys225 < tinggiMaks; ys225++)
                {
                    int rs = img.GetPixel(x225, ys225).R;
                    int gs = img.GetPixel(x225, ys225).G;
                    int bs = img.GetPixel(x225, ys225).B;

                    if (rs < 10)
                    {
                        continue;
                    }
                    else if (gs < 30)
                    {
                        continue;
                    }
                    else if (bs < 0)
                    {
                        continue;
                    }
                    else if (rs > 150)
                    {
                        continue;
                    }
                    else if (gs > 250)
                    {
                        continue;
                    }
                    else if (bs > 50)
                    {
                        continue;
                    }
                    else
                    {
                        y225 = ys225;
                        x225--;
                    }
                }
                x225 = x225 + 1;

                // sudut 315 derajat

                int x315 = medl;
                int y315 = 0;

                for (int ys315 = medt; ys315 < tinggiMaks; ys315++)
                {
                    int rs = img.GetPixel(x315, ys315).R;
                    int gs = img.GetPixel(x315, ys315).G;
                    int bs = img.GetPixel(x315, ys315).B;

                    if (rs < 10)
                    {
                        continue;
                    }
                    else if (gs < 30)
                    {
                        continue;
                    }
                    else if (bs < 0)
                    {
                        continue;
                    }
                    else if (rs > 150)
                    {
                        continue;
                    }
                    else if (gs > 250)
                    {
                        continue;
                    }
                    else if (bs > 50)
                    {
                        continue;
                    }
                    else
                    {
                        y315 = ys315;
                        x315++;
                    }
                }
                x315 = x315 - 1;

                int d1, d2, dc1, dc2, dc3, dc4;

                dc1 = (int)Math.Sqrt((int)Math.Pow(x45 - medl, 2) + (int)Math.Pow(y45 - medt, 2));
                dc2 = (int)Math.Sqrt((int)Math.Pow(x135 - medl, 2) + (int)Math.Pow(y135 - medt, 2));
                dc3 = (int)Math.Sqrt((int)Math.Pow(x315 - medl, 2) + (int)Math.Pow(y315 - medt, 2));
                dc4 = (int)Math.Sqrt((int)Math.Pow(x225 - medl, 2) + (int)Math.Pow(y225 - medt, 2));

                txtS45.Text = dc1.ToString();
                txtS135.Text = dc2.ToString();
                txtS225.Text = dc3.ToString();
                txtS315.Text = dc4.ToString();

                clasify();
                MessageBox.Show("Scanning Complete - " + lbJenis.Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Gambar masih kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clasify()
        {
            int tred = Convert.ToInt32(txtRed.Text);
            int tgreen = Convert.ToInt32(txtGreen.Text);
            int tblue = Convert.ToInt32(txtBlue.Text);
            int thue = Convert.ToInt32(txtHue.Text);
            int tsaturation = Convert.ToInt32(txtSaturation.Text);
            int tvalue = Convert.ToInt32(txtValue.Text);
            int tLebar = Convert.ToInt32(txtLebar.Text);
            int tLmin = Convert.ToInt32(txtLmin.Text);
            int tLmax = Convert.ToInt32(txtLmax.Text);
            int tTinggi = Convert.ToInt32(txtTinggi.Text);
            int tTmin = Convert.ToInt32(txtTmin.Text);
            int tTmax = Convert.ToInt32(txtTmax.Text);
            int tmedx = Convert.ToInt32(txtMedX.Text);
            int tmedy = Convert.ToInt32(txtMedY.Text);
            int td1 = Convert.ToInt32(txtS45.Text);
            int td2 = Convert.ToInt32(txtS135.Text);
            int td3 = Convert.ToInt32(txtS225.Text);
            int td4 = Convert.ToInt32(txtS315.Text);


            if (tLmin <= 131)
            {
                if (tLebar <= 343)
                {
                    if (tred <= 25)
                    {
                        if (tblue <= 12)
                        {
                            if (tTinggi <= 141)
                            {
                                lbJenis.Text = "Belimbing Wuluh";
                            }
                            else if (tTinggi > 141)
                            {
                                lbJenis.Text = "Binahong";
                            }
                            

                        }
                        else if (tblue > 12)
                        {
                            if (tred <= 20)
                            {
                                if (tblue <= 13)
                                {
                                    
                                     if (tLebar > 273)
                                    {
                                        lbJenis.Text = "Melati";
                                    }
                                    
                                }
                                else if (tblue > 13)
                                {
                                    lbJenis.Text = "Sirih Hijau";
                                }
                            }
                            else if (tred > 20)
                            {
                                lbJenis.Text = "Binahong";
                            }
                        }
                    }
                    else if (tred > 30)
                    {
                        lbJenis.Text = "Daun Ungu";
                    }
                }
                else if (tLebar > 343)
                {
                    
                     if (tblue > 13)
                    {
                        if (tTinggi > 200)
                        {
                            lbJenis.Text = "Jambu Biji";
                        }
                        else if (tTinggi < 200)
                        {
                            lbJenis.Text = "Sirsat";
                        }
                            
                    }
                }
            }

            else if (tLmin > 131)
            {
                if (tLmin <= 149)
                {
                    if (td1 <= 50)
                    {
                        if (tgreen <= 40)
                        {
                            if (tTinggi <= 94)
                            {
                                lbJenis.Text = "Jeruk Purut";
                            }
                            else if (tTinggi > 94)
                            {
                                lbJenis.Text = "Pecut Kuda";
                            }
                        }
                        else if (tgreen > 40)
                        {
                            lbJenis.Text = "Jeruk Purut";
                        }
                    }
                    else if (td1 > 50)
                    {
                        lbJenis.Text = "Pecut Kuda";
                    }
                }
                else if (tLmin > 149)
                {
                    lbJenis.Text = "Jeruk Nipis";
                }
            }



            else
            {
                lbJenis.Text = "Daun Tidak Diketahui";
                MessageBox.Show("Jenis daun Tidak ditemukan");
            }



            OleDbDataReader rd = null;
            try
            {
                conn.Close();
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                string query = "SELECT * FROM informasi WHERE jenis='" + lbJenis.Text + "'";
                cmd.CommandText = query;
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    lblatin.Text = rd["latin"].ToString();
                    lbKhasiat.Text = rd["informasi"].ToString();
                }
                else
                {
                    lbJenis.Text = "Tidak Ditemukan";
                }
                conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu FormMenu = new MainMenu();
            FormMenu.Show();
        }    
    }
}
