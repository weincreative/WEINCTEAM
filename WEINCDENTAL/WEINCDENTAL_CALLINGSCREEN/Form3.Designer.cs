namespace WEINCDENTAL_CALLINGSCREEN
{
    partial class Form3
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrForm2CloseValue = new System.Windows.Forms.Timer(this.components);
            this.tmrForm2CagirildiHasta = new System.Windows.Forms.Timer(this.components);
            this.tmrForm2CagirilcakHasta = new System.Windows.Forms.Timer(this.components);
            this.tmrSiradakiHYanSon = new System.Windows.Forms.Timer(this.components);
            this.tmrBilgilendirme = new System.Windows.Forms.Timer(this.components);
            this.lblBolumDoktorBilgisi1 = new System.Windows.Forms.Label();
            this.dgvHastaListesi1 = new System.Windows.Forms.DataGridView();
            this.lblBilgilendirme1 = new System.Windows.Forms.Label();
            this.lblHastaneAdi = new System.Windows.Forms.Label();
            this.lblCagirilanHasta1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvHastaListesi2 = new System.Windows.Forms.DataGridView();
            this.lblBolumDoktorBilgisi2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblBilgilendirme2 = new System.Windows.Forms.Label();
            this.lblCagirilanHasta2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHastaListesi1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHastaListesi2)).BeginInit();
            this.panel2.SuspendLayout();
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
            // 
            // tmrForm2CloseValue
            // 
            this.tmrForm2CloseValue.Interval = 1000;
            // 
            // tmrForm2CagirildiHasta
            // 
            this.tmrForm2CagirildiHasta.Interval = 1000;
            // 
            // tmrForm2CagirilcakHasta
            // 
            this.tmrForm2CagirilcakHasta.Interval = 1000;
            // 
            // tmrSiradakiHYanSon
            // 
            this.tmrSiradakiHYanSon.Interval = 1000;
            // 
            // tmrBilgilendirme
            // 
            this.tmrBilgilendirme.Interval = 5000;
            // 
            // lblBolumDoktorBilgisi1
            // 
            this.lblBolumDoktorBilgisi1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBolumDoktorBilgisi1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBolumDoktorBilgisi1.Location = new System.Drawing.Point(3, 0);
            this.lblBolumDoktorBilgisi1.Name = "lblBolumDoktorBilgisi1";
            this.lblBolumDoktorBilgisi1.Size = new System.Drawing.Size(710, 65);
            this.lblBolumDoktorBilgisi1.TabIndex = 10;
            this.lblBolumDoktorBilgisi1.Text = "DOKTOR BİLGİSİ1";
            // 
            // dgvHastaListesi1
            // 
            this.dgvHastaListesi1.AllowUserToOrderColumns = true;
            this.dgvHastaListesi1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHastaListesi1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHastaListesi1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvHastaListesi1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHastaListesi1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHastaListesi1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvHastaListesi1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 24F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHastaListesi1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHastaListesi1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe MDL2 Assets", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHastaListesi1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHastaListesi1.Enabled = false;
            this.dgvHastaListesi1.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHastaListesi1.Location = new System.Drawing.Point(3, 68);
            this.dgvHastaListesi1.Name = "dgvHastaListesi1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHastaListesi1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHastaListesi1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHastaListesi1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvHastaListesi1.Size = new System.Drawing.Size(710, 297);
            this.dgvHastaListesi1.TabIndex = 9;
            // 
            // lblBilgilendirme1
            // 
            this.lblBilgilendirme1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBilgilendirme1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBilgilendirme1.Location = new System.Drawing.Point(0, 0);
            this.lblBilgilendirme1.Name = "lblBilgilendirme1";
            this.lblBilgilendirme1.Size = new System.Drawing.Size(713, 74);
            this.lblBilgilendirme1.TabIndex = 8;
            this.lblBilgilendirme1.Text = "BİLGİLENDİRME1";
            this.lblBilgilendirme1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblHastaneAdi
            // 
            this.lblHastaneAdi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHastaneAdi.Font = new System.Drawing.Font("Segoe MDL2 Assets", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHastaneAdi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblHastaneAdi.Location = new System.Drawing.Point(0, 0);
            this.lblHastaneAdi.Name = "lblHastaneAdi";
            this.lblHastaneAdi.Size = new System.Drawing.Size(1436, 119);
            this.lblHastaneAdi.TabIndex = 7;
            this.lblHastaneAdi.Text = "HASTANE ADI";
            this.lblHastaneAdi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCagirilanHasta1
            // 
            this.lblCagirilanHasta1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCagirilanHasta1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 150F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCagirilanHasta1.Location = new System.Drawing.Point(3, 78);
            this.lblCagirilanHasta1.Name = "lblCagirilanHasta1";
            this.lblCagirilanHasta1.Size = new System.Drawing.Size(707, 187);
            this.lblCagirilanHasta1.TabIndex = 6;
            this.lblCagirilanHasta1.Text = "İÇERİDEKİ / GİRİCEK HASTA1";
            this.lblCagirilanHasta1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBolumDoktorBilgisi2);
            this.panel1.Controls.Add(this.dgvHastaListesi2);
            this.panel1.Controls.Add(this.lblBolumDoktorBilgisi1);
            this.panel1.Controls.Add(this.dgvHastaListesi1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1436, 365);
            this.panel1.TabIndex = 11;
            // 
            // dgvHastaListesi2
            // 
            this.dgvHastaListesi2.AllowUserToOrderColumns = true;
            this.dgvHastaListesi2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHastaListesi2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHastaListesi2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvHastaListesi2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHastaListesi2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHastaListesi2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvHastaListesi2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 24F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHastaListesi2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvHastaListesi2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe MDL2 Assets", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHastaListesi2.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvHastaListesi2.Enabled = false;
            this.dgvHastaListesi2.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHastaListesi2.Location = new System.Drawing.Point(719, 68);
            this.dgvHastaListesi2.Name = "dgvHastaListesi2";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHastaListesi2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvHastaListesi2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHastaListesi2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvHastaListesi2.Size = new System.Drawing.Size(716, 297);
            this.dgvHastaListesi2.TabIndex = 10;
            // 
            // lblBolumDoktorBilgisi2
            // 
            this.lblBolumDoktorBilgisi2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBolumDoktorBilgisi2.Font = new System.Drawing.Font("Segoe MDL2 Assets", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBolumDoktorBilgisi2.Location = new System.Drawing.Point(719, 0);
            this.lblBolumDoktorBilgisi2.Name = "lblBolumDoktorBilgisi2";
            this.lblBolumDoktorBilgisi2.Size = new System.Drawing.Size(716, 65);
            this.lblBolumDoktorBilgisi2.TabIndex = 11;
            this.lblBolumDoktorBilgisi2.Text = "DOKTOR BİLGİSİ2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblCagirilanHasta2);
            this.panel2.Controls.Add(this.lblBilgilendirme2);
            this.panel2.Controls.Add(this.lblBilgilendirme1);
            this.panel2.Controls.Add(this.lblCagirilanHasta1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1436, 265);
            this.panel2.TabIndex = 12;
            // 
            // lblBilgilendirme2
            // 
            this.lblBilgilendirme2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBilgilendirme2.Font = new System.Drawing.Font("Segoe MDL2 Assets", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBilgilendirme2.Location = new System.Drawing.Point(719, 0);
            this.lblBilgilendirme2.Name = "lblBilgilendirme2";
            this.lblBilgilendirme2.Size = new System.Drawing.Size(717, 74);
            this.lblBilgilendirme2.TabIndex = 9;
            this.lblBilgilendirme2.Text = "BİLGİLENDİRME2";
            this.lblBilgilendirme2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCagirilanHasta2
            // 
            this.lblCagirilanHasta2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCagirilanHasta2.Font = new System.Drawing.Font("Segoe MDL2 Assets", 150F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCagirilanHasta2.Location = new System.Drawing.Point(729, 78);
            this.lblCagirilanHasta2.Name = "lblCagirilanHasta2";
            this.lblCagirilanHasta2.Size = new System.Drawing.Size(707, 187);
            this.lblCagirilanHasta2.TabIndex = 10;
            this.lblCagirilanHasta2.Text = "İÇERİDEKİ / GİRİCEK HASTA2";
            this.lblCagirilanHasta2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 734);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHastaneAdi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHastaListesi1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHastaListesi2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer tmrForm2CloseValue;
        private System.Windows.Forms.Timer tmrForm2CagirildiHasta;
        private System.Windows.Forms.Timer tmrForm2CagirilcakHasta;
        private System.Windows.Forms.Timer tmrSiradakiHYanSon;
        private System.Windows.Forms.Timer tmrBilgilendirme;
        private System.Windows.Forms.Label lblBolumDoktorBilgisi1;
        private System.Windows.Forms.DataGridView dgvHastaListesi1;
        private System.Windows.Forms.Label lblBilgilendirme1;
        private System.Windows.Forms.Label lblHastaneAdi;
        private System.Windows.Forms.Label lblCagirilanHasta1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvHastaListesi2;
        private System.Windows.Forms.Label lblBolumDoktorBilgisi2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCagirilanHasta2;
        private System.Windows.Forms.Label lblBilgilendirme2;
    }
}