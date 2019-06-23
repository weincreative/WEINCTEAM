namespace WEINCDENTAL_CALLINGSCREEN
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrForm2CloseValue = new System.Windows.Forms.Timer(this.components);
            this.lblCagirilanHasta = new System.Windows.Forms.Label();
            this.lblHastaneAdi = new System.Windows.Forms.Label();
            this.lblBilgilendirme = new System.Windows.Forms.Label();
            this.dgvHastaListesi = new System.Windows.Forms.DataGridView();
            this.lblBolumDoktorBilgisi = new System.Windows.Forms.Label();
            this.tmrForm2CagirildiHasta = new System.Windows.Forms.Timer(this.components);
            this.tmrForm2CagirilcakHasta = new System.Windows.Forms.Timer(this.components);
            this.tmrSiradakiHYanSon = new System.Windows.Forms.Timer(this.components);
            this.tmrBilgilendirme = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHastaListesi)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(100, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem.Text = "Çıkış";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tmrForm2CloseValue
            // 
            this.tmrForm2CloseValue.Interval = 1000;
            this.tmrForm2CloseValue.Tick += new System.EventHandler(this.tmrForm2CloseValue_Tick);
            // 
            // lblCagirilanHasta
            // 
            this.lblCagirilanHasta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCagirilanHasta.Font = new System.Drawing.Font("Segoe MDL2 Assets", 150F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCagirilanHasta.Location = new System.Drawing.Point(6, 193);
            this.lblCagirilanHasta.Name = "lblCagirilanHasta";
            this.lblCagirilanHasta.Size = new System.Drawing.Size(1430, 241);
            this.lblCagirilanHasta.TabIndex = 1;
            this.lblCagirilanHasta.Text = "İÇERİDEKİ / GİRİCEK HASTA";
            this.lblCagirilanHasta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHastaneAdi
            // 
            this.lblHastaneAdi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHastaneAdi.Font = new System.Drawing.Font("Segoe MDL2 Assets", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHastaneAdi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblHastaneAdi.Location = new System.Drawing.Point(0, 0);
            this.lblHastaneAdi.Name = "lblHastaneAdi";
            this.lblHastaneAdi.Size = new System.Drawing.Size(1436, 119);
            this.lblHastaneAdi.TabIndex = 2;
            this.lblHastaneAdi.Text = "HASTANE ADI";
            this.lblHastaneAdi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBilgilendirme
            // 
            this.lblBilgilendirme.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBilgilendirme.Font = new System.Drawing.Font("Segoe MDL2 Assets", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBilgilendirme.Location = new System.Drawing.Point(0, 119);
            this.lblBilgilendirme.Name = "lblBilgilendirme";
            this.lblBilgilendirme.Size = new System.Drawing.Size(1436, 74);
            this.lblBilgilendirme.TabIndex = 3;
            this.lblBilgilendirme.Text = "BİLGİLENDİRME";
            this.lblBilgilendirme.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dgvHastaListesi
            // 
            this.dgvHastaListesi.AllowUserToOrderColumns = true;
            this.dgvHastaListesi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHastaListesi.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvHastaListesi.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHastaListesi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHastaListesi.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvHastaListesi.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 24F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHastaListesi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvHastaListesi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe MDL2 Assets", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHastaListesi.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvHastaListesi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvHastaListesi.Enabled = false;
            this.dgvHastaListesi.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHastaListesi.Location = new System.Drawing.Point(0, 323);
            this.dgvHastaListesi.Name = "dgvHastaListesi";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHastaListesi.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvHastaListesi.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHastaListesi.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvHastaListesi.Size = new System.Drawing.Size(1436, 411);
            this.dgvHastaListesi.TabIndex = 4;
            // 
            // lblBolumDoktorBilgisi
            // 
            this.lblBolumDoktorBilgisi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBolumDoktorBilgisi.Font = new System.Drawing.Font("Segoe MDL2 Assets", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBolumDoktorBilgisi.Location = new System.Drawing.Point(0, 269);
            this.lblBolumDoktorBilgisi.Name = "lblBolumDoktorBilgisi";
            this.lblBolumDoktorBilgisi.Size = new System.Drawing.Size(1436, 54);
            this.lblBolumDoktorBilgisi.TabIndex = 5;
            this.lblBolumDoktorBilgisi.Text = "DOKTOR BİLGİSİ";
            // 
            // tmrForm2CagirildiHasta
            // 
            this.tmrForm2CagirildiHasta.Interval = 1000;
            this.tmrForm2CagirildiHasta.Tick += new System.EventHandler(this.tmrForm2CagirildiHasta_Tick);
            // 
            // tmrForm2CagirilcakHasta
            // 
            this.tmrForm2CagirilcakHasta.Interval = 1000;
            this.tmrForm2CagirilcakHasta.Tick += new System.EventHandler(this.tmrForm2CagirilcakHasta_Tick);
            // 
            // tmrSiradakiHYanSon
            // 
            this.tmrSiradakiHYanSon.Interval = 1000;
            this.tmrSiradakiHYanSon.Tick += new System.EventHandler(this.tmrSiradakiHYanSon_Tick);
            // 
            // tmrBilgilendirme
            // 
            this.tmrBilgilendirme.Interval = 5000;
            this.tmrBilgilendirme.Tick += new System.EventHandler(this.tmrBilgilendirme_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 734);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.lblBolumDoktorBilgisi);
            this.Controls.Add(this.dgvHastaListesi);
            this.Controls.Add(this.lblBilgilendirme);
            this.Controls.Add(this.lblHastaneAdi);
            this.Controls.Add(this.lblCagirilanHasta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHastaListesi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer tmrForm2CloseValue;
        private System.Windows.Forms.Label lblCagirilanHasta;
        private System.Windows.Forms.Label lblHastaneAdi;
        private System.Windows.Forms.Label lblBilgilendirme;
        private System.Windows.Forms.DataGridView dgvHastaListesi;
        private System.Windows.Forms.Label lblBolumDoktorBilgisi;
        private System.Windows.Forms.Timer tmrForm2CagirildiHasta;
        private System.Windows.Forms.Timer tmrForm2CagirilcakHasta;
        private System.Windows.Forms.Timer tmrSiradakiHYanSon;
        private System.Windows.Forms.Timer tmrBilgilendirme;
    }
}