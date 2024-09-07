using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace LeafDetect
{
    public partial class Training : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        DataTable dtDaun;

        string save_action;

        Bitmap img, img2, img3, img4;
        int l, t, lebarMaks, lebarMin, tinggiMaks, tinggiMin;
        int red, green, blue;
        double hue, saturation, falue;

        int counter = 0;
        double rataR, rataG, rataB;
        int jumlahR = 0;
        int jumlahG = 0;
        int jumlahB = 0;

        public Training()
        {
            InitializeComponent();
            this.tampilData();
            DGV.Update();
            DGV.Refresh();
            nonaktif();
        }

        private void Training_Load(object sender, EventArgs e)
        {
            setRowNumber(DGV);
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

        private void CloseConnectionDb()
        {
            if (conn != null)
            {
                conn.Close();
                listBox1.Items.Add("Koneksi database ditutup");
            }
        }

        private void tampilData()
        {
            try
            {
                this.ConnectDb();
                cmd.Connection = conn;
                string query = "SELECT ID, jenis as [Jenis],"
                                + "red as [Red], green as [Green], blue as [Blue],"
                                + "hue as [Hue], saturation as [Saturation], falue as [Value],"
                                + "lebar as [Lebar], lebarmin as [Lebar Min], lebarmax as [Lebar Max],"
                                + "tinggi as [Tinggi], tinggimin as [Tinggi Min], tinggimax as [Tinggi Max],"
                                + "medianx as [Median X], mediany as [Median Y],"
                                + "diagonal1 as [Diagonal 1], diagonal2 as [Diagonal 2],"
                                + "diagonal3 as [Diagonal 3], diagonal4 as [Diagonal 4]"
                                + "FROM DataDaun ORDER BY jenis ASC";
                cmd.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dtDaun = new DataTable();
                da.Fill(dtDaun);
                DGV.DataSource = dtDaun;

                DGV.Columns[0].Visible = false;
                DGV.Focus();
                DGV.CurrentCell = DGV.Rows[0].Cells[1];

                DGV.Refresh();
                setRowNumber(DGV);
                DGV.Update();
                this.CloseConnectionDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void setRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }
        }

        public void nonaktif()
        {
            btnPilih.Enabled = false;
            btnProses.Enabled = false;
            btnSimpan.Enabled = false;
            btnBatal.Visible = false;
            txtJenis.Enabled = false;

            btnTambah.Enabled = true;
            btnHapus.Enabled = true;
            btnUbah.Enabled = true;
            DGV.Enabled = true;
        }

        public void aktif()
        {
            btnPilih.Enabled = true;
            btnProses.Enabled = true;
            btnSimpan.Enabled = true;
            btnBatal.Visible = true;
            txtJenis.Enabled = true;

            btnTambah.Enabled = false;
            btnHapus.Enabled = false;
            btnUbah.Enabled = false;
            DGV.Enabled = false;
        }

        public void bersih()
        {
            txtId.Text = string.Empty;
            txtJenis.Text = string.Empty;
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
                //Image<Bgr, Byte> imgOri = new Image<Bgr, Byte>(openDialog.FileName);
                //Image<Bgr, byte> resizedImg = imgOri.Resize(480, 360, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                //resizedImg._EqualizeHist();
                //pbgambar.Image = resizedImg.ToBitmap();
                img = new Bitmap(new Bitmap(openDialog.FileName), pbgambar.Width, pbgambar.Height);
                pbgambar.Image = img;
                txtKet.Text = openDialog.FileName.ToString();
                pbgambar.ImageLocation = txtKet.Text;
                MessageBox.Show("Open Picture Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bersih();
                progressBar1.Value = 0;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            bersih();
            aktif();
            save_action = "add";
            progressBar1.Value = 0;
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            aktif();
            btnPilih.Enabled = false;
            btnProses.Enabled = false;
            btnCetak.Enabled = false;
            save_action = "edit";
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (save_action == "add")
            {
                if (txtRed.Text != "")
                {
                    try
                    {

                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO DataDaun (jenis, red, green, blue,"
                                            + "hue, saturation, falue,"
                                            + "lebar, lebarmin, lebarmax,"
                                            + "tinggi, tinggimin, tinggimax,"
                                            + "medianx, mediany,"
                                            + "diagonal1, diagonal2,"
                                            + "diagonal3, diagonal4)"
                                            + "VALUES"
                                            + "('" + txtJenis.Text + "','"
                                            + txtRed.Text + "','" + txtGreen.Text + "','" + txtBlue.Text + "','"
                                            + txtHue.Text + "','" + txtSaturation.Text + "','" + txtValue.Text + "','"
                                            + txtLebar.Text + "', '" + txtLmin.Text + "', '" + txtLmax.Text + "', '"
                                            + txtTinggi.Text + "','" + txtTmin.Text + "', '" + txtTinggi.Text + "','"
                                            + txtMedX.Text + "','" + txtMedY.Text + "','"
                                            + txtS45.Text + "','" + txtS135.Text + "','" + txtS225.Text + "','" + txtS315.Text + "')";

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        setRowNumber(DGV);
                        conn.Close();
                        bersih();
                        nonaktif();
                        //loadData();
                        tampilData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex);
                    }
                }
                else
                    MessageBox.Show("Data fitur masih kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        string query = "UPDATE DataDaun SET jenis = '" + txtJenis.Text + "',"
                                        + "red = '" + txtRed.Text + "', green = '" + txtGreen.Text + "', blue = '" + txtBlue.Text + "',"
                                        + "hue = '" + txtHue.Text + "', saturation = '" + txtSaturation.Text + "', falue = '" + txtValue.Text + "',"
                                        + "lebar = '" + txtLebar.Text + "', lebarmin = '" + txtLmin.Text + "', lebarmax = '" + txtLmax.Text + "',"
                                        + "tinggi = '" + txtTinggi.Text + "', tinggimin = '" + txtTmin.Text + "', tinggimax = '" + txtTmax.Text + "',"
                                        + "medianx = '" + txtMedX.Text + "', mediany = '" + txtMedY.Text + "',"
                                        + "diagonal1 = '" + txtS45.Text + "', diagonal2 = '" + txtS135.Text + "', diagonal3 = '" + txtS225.Text + "', diagonal4 = '" + txtS315.Text + "'"
                                        + "WHERE ID = " + txtId.Text + "";
                        cmd.CommandText = query;

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        bersih();
                        //loadData();
                        tampilData();
                        nonaktif();
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
                    string query = "DELETE FROM DataDaun WHERE ID=" + txtId.Text + "";
                    //MessageBox.Show(query);
                    cmd.CommandText = query;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    tampilData();
                    bersih();
                    nonaktif();
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
            bersih();
            nonaktif();
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            if (pbgambar.Image != null)
            {
                if (txtJenis.Text != "")
                {
                    img = new Bitmap(txtKet.Text);
                    progressBar1.Maximum = img.Width * 3;

                    int lebar = img.Width;
                    int tinggi = img.Height;

                    lebarMaks = 0;
                    tinggiMaks = 0;
                    lebarMin = 1000;
                    tinggiMin = 1000;

                    for (int i = 0; i < lebar; i++)
                    {
                        progressBar1.Value = progressBar1.Value + 1;
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
		            if(cmax == rp){
			            double hr = 60*(((gp - bp)/delta)%6);
			            int h = Convert.ToInt32(hr);
			            listBox1.Items.Add("Nilai Hue : "+h);
                        txtHue.Text = h.ToString();
		            }else if(cmax == gp){
			            double hg = 60*(((bp - rp)/delta)+2);
                        int h = Convert.ToInt32(hg);
			            listBox1.Items.Add("Nilai Hue : "+h);
                        txtHue.Text = h.ToString();
		            }else if(cmax == bp){
			            double hb = 60*(((rp - gp)/delta)+4);
                        int h = Convert.ToInt32(hb);
                        listBox1.Items.Add("Nilai Hue : " + h);
                        txtHue.Text = h.ToString();
		            }

                    //Nilai Saturation
		            if(cmax == 0){
			            int s = 0;
			            listBox1.Items.Add("Nilai Saturation : "+s);
                        txtSaturation.Text = s.ToString();
		            }
		            else if(cmax != 0){
			            double sat = (delta/cmax)*100;
                        int s = Convert.ToInt32(sat);
			            listBox1.Items.Add("Nilai Saturation : "+s);
                        txtSaturation.Text = s.ToString();
		            }
		
		            //Nilai Value
		            double va = cmax*100;
                    int v = Convert.ToInt32(va);
		            listBox1.Items.Add("Nilai Value : "+v);
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

                    // diagonal 1

                    int x1 = medl;
                    int y1 = 0;

                    for (int ys1 = medt; ys1 > 0; ys1--)
                    {
                        int rs = img.GetPixel(x1, ys1).R;
                        int gs = img.GetPixel(x1, ys1).G;
                        int bs = img.GetPixel(x1, ys1).B;

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
                            y1 = ys1;
                            x1++;
                        }
                    }
                    x1 = x1 - 1;

                    // diagonal 2

                    int x2 = medl;
                    int y2 = 0;

                    for (int ys2 = medt; ys2 > 0; ys2--)
                    {
                        int rs = img.GetPixel(x2, ys2).R;
                        int gs = img.GetPixel(x2, ys2).G;
                        int bs = img.GetPixel(x2, ys2).B;

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
                            y2 = ys2;
                            x2--;
                        }
                    }
                    x2 = x2 + 1;

                    // diagonal 3

                    int x3 = medl;
                    int y3 = 0;

                    for (int ys3 = medt; ys3 < tinggiMaks; ys3++)
                    {
                        int rs = img.GetPixel(x3, ys3).R;
                        int gs = img.GetPixel(x3, ys3).G;
                        int bs = img.GetPixel(x3, ys3).B;

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
                            y3 = ys3;
                            x3--;
                        }
                    }
                    x3 = x3 + 1;

                    // diagonal 4

                    int x4 = medl;
                    int y4 = 0;

                    for (int ys4 = medt; ys4 < tinggiMaks; ys4++)
                    {
                        int rs = img.GetPixel(x4, ys4).R;
                        int gs = img.GetPixel(x4, ys4).G;
                        int bs = img.GetPixel(x4, ys4).B;

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
                            y4 = ys4;
                            x4++;
                        }
                    }
                    x4 = x4 - 1;

                    int d1, d2, dc1, dc2, dc3, dc4;

                    dc1 = (int)Math.Sqrt((int)Math.Pow(x1 - medl, 2) + (int)Math.Pow(y1 - medt, 2));
                    dc2 = (int)Math.Sqrt((int)Math.Pow(x2 - medl, 2) + (int)Math.Pow(y2 - medt, 2));
                    dc3 = (int)Math.Sqrt((int)Math.Pow(x4 - medl, 2) + (int)Math.Pow(y4 - medt, 2));
                    dc4 = (int)Math.Sqrt((int)Math.Pow(x3 - medl, 2) + (int)Math.Pow(y3 - medt, 2));

                    int S0 = lebarMaks - medl; txtS0.Text = S0.ToString();
                    int S90 = medt - tinggiMin; txtS90.Text = S90.ToString();
                    int S180 = medl - lebarMin; txtS180.Text = S180.ToString();
                    int S270 = medt - tinggiMin; txtS270.Text = S270.ToString();

                    txtS45.Text = dc1.ToString();
                    txtS135.Text = dc2.ToString();
                    txtS225.Text = dc3.ToString();
                    txtS315.Text = dc4.ToString();

                    listBox1.Items.Add("x1,y1 = " + x1 + "," + y1);
                    listBox1.Items.Add("====================");
                    listBox1.Items.Add("x2,y2 = " + x2 + "," + y2);
                    listBox1.Items.Add("====================");
                    listBox1.Items.Add("x3,y3 = " + x3 + "," + y3);
                    listBox1.Items.Add("====================");
                    listBox1.Items.Add("x4,y4 = " + x4 + "," + y4);

                    RGB2GS();
                    RGB2BIN();

                    MessageBox.Show("Scanning Complete");
                }
                else
                {
                    MessageBox.Show("TextBox masih kosong");
                }
            }
            else
            {
                MessageBox.Show("Gambar masih kosong");
            }
        }

        private void RGB2GS()
        {
            int i, j;
            if (img != null)
            {
                img2 = new Bitmap(img);
                for (i = 0; i <= img2.Width - 1; i++)
                {
                    progressBar1.Value = progressBar1.Value + 1;
                    for (j = 0; j <= img2.Height - 1; j++)
                    {
                        Color originalColor = img2.GetPixel(i, j);
                        int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));
                        Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                        img2.SetPixel(i, j, newColor);
                    }
                }
                img2.Save("Image\\Gray" + txtJenis.Text + ".jpg");
            }
        }

        private void RGB2BIN()
        {
            int i, j, rata;
            int gray = 0;



            if (img != null)
            {
                img3 = new Bitmap(img);

                for (i = 0; i <= img3.Width - 1; i++)
                {
                    progressBar1.Value = progressBar1.Value + 1;
                    for (j = 0; j <= img3.Height - 1; j++)
                    {
                        red = img.GetPixel(i, j).R;
                        green = img.GetPixel(i, j).G;
                        blue = img.GetPixel(i, j).B;
                        rata = ((red + green + blue) / 3);
                        if (rata < 128)
                        {
                            gray = 255;
                        }
                        else if (rata > 128)
                            gray = 0;
                        Color newpixelColor = Color.FromArgb(gray, gray, gray);
                        img3.SetPixel(i, j, newpixelColor);
                    }
                }

                img3.Save("Image\\Biner" + txtJenis.Text + ".jpg");
                pbBiner.Image = img3;


            }
        }

        private void DGV_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in DGV.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                txtId.Text = row.Cells[0].Value.ToString();
                txtJenis.Text = row.Cells[1].Value.ToString();
                txtRed.Text = row.Cells[2].Value.ToString();
                txtGreen.Text = row.Cells[3].Value.ToString();
                txtBlue.Text = row.Cells[4].Value.ToString();
                txtHue.Text = row.Cells[5].Value.ToString();
                txtSaturation.Text = row.Cells[6].Value.ToString();
                txtValue.Text = row.Cells[7].Value.ToString();
                txtLebar.Text = row.Cells[8].Value.ToString();
                txtLmin.Text = row.Cells[9].Value.ToString();
                txtLmax.Text = row.Cells[10].Value.ToString();
                txtTinggi.Text = row.Cells[11].Value.ToString();
                txtTmin.Text = row.Cells[12].Value.ToString();
                txtTmax.Text = row.Cells[13].Value.ToString();
                txtMedX.Text = row.Cells[14].Value.ToString();
                txtMedY.Text = row.Cells[15].Value.ToString();
                txtS45.Text = row.Cells[16].Value.ToString();
                txtS135.Text = row.Cells[17].Value.ToString();
                txtS225.Text = row.Cells[18].Value.ToString();
                txtS315.Text = row.Cells[19].Value.ToString();
            }
            conn.Close();
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int i = 0;
            int j = 0;

            for (i = 0; i <= DGV.RowCount - 1; i++)
            {
                for (j = 0; j <= DGV.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = DGV[j, i];
                    xlWorkSheet.Cells[i + 1, j + 1] = cell.Value;
                }
            }

            MessageBox.Show("File created !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            xlApp.Visible = true;
            try
            {
                xlWorkSheet.SaveAs(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DataSet.xls", misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
            }
            catch
            {
                MessageBox.Show("Report Telah Ditutup", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
