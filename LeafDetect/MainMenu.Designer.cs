namespace LeafDetect
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.administratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pelatihanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pengenalanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administratorToolStripMenuItem,
            this.pengenalanToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(610, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // administratorToolStripMenuItem
            // 
            this.administratorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pelatihanToolStripMenuItem1,
            this.dataInfoToolStripMenuItem});
            this.administratorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.administratorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("administratorToolStripMenuItem.Image")));
            this.administratorToolStripMenuItem.Name = "administratorToolStripMenuItem";
            this.administratorToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.administratorToolStripMenuItem.Text = "Administrator";
            // 
            // pelatihanToolStripMenuItem1
            // 
            this.pelatihanToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("pelatihanToolStripMenuItem1.Image")));
            this.pelatihanToolStripMenuItem1.Name = "pelatihanToolStripMenuItem1";
            this.pelatihanToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.pelatihanToolStripMenuItem1.Text = "Pelatihan";
            this.pelatihanToolStripMenuItem1.Click += new System.EventHandler(this.pelatihanToolStripMenuItem1_Click);
            // 
            // dataInfoToolStripMenuItem
            // 
            this.dataInfoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dataInfoToolStripMenuItem.Image")));
            this.dataInfoToolStripMenuItem.Name = "dataInfoToolStripMenuItem";
            this.dataInfoToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.dataInfoToolStripMenuItem.Text = "Data Info";
            this.dataInfoToolStripMenuItem.Click += new System.EventHandler(this.dataInfoToolStripMenuItem_Click);
            // 
            // pengenalanToolStripMenuItem
            // 
            this.pengenalanToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pengenalanToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pengenalanToolStripMenuItem.Image")));
            this.pengenalanToolStripMenuItem.Name = "pengenalanToolStripMenuItem";
            this.pengenalanToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.pengenalanToolStripMenuItem.Text = "Pengenalan";
            this.pengenalanToolStripMenuItem.Click += new System.EventHandler(this.pengenalanToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Impact", 26.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(0, 139);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(610, 60);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "Aplikasi Pengenalan Daun Obat - Obatan";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(610, 341);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Utama";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pengenalanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pelatihanToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dataInfoToolStripMenuItem;
        internal System.Windows.Forms.Label Label1;
    }
}

