using System;

namespace WEINCDENTAL_CALLINGSCREEN
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.cmbScreenList = new System.Windows.Forms.ComboBox();
            this.LBLbottom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBolumSec = new System.Windows.Forms.ComboBox();
            this.cmbKullaniciSec = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Location = new System.Drawing.Point(209, 84);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(125, 35);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "BAŞLAT";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(358, 84);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(125, 35);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "DURDUR";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cmbScreenList
            // 
            this.cmbScreenList.FormattingEnabled = true;
            this.cmbScreenList.Location = new System.Drawing.Point(209, 3);
            this.cmbScreenList.Name = "cmbScreenList";
            this.cmbScreenList.Size = new System.Drawing.Size(274, 21);
            this.cmbScreenList.TabIndex = 1;
            this.cmbScreenList.Text = "Ekran Seçiniz..";
            this.cmbScreenList.SelectedIndexChanged += new System.EventHandler(this.cmbScreenList_SelectedIndexChanged);
            // 
            // LBLbottom
            // 
            this.LBLbottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LBLbottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LBLbottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LBLbottom.ForeColor = System.Drawing.Color.Red;
            this.LBLbottom.Location = new System.Drawing.Point(0, 255);
            this.LBLbottom.Name = "LBLbottom";
            this.LBLbottom.Size = new System.Drawing.Size(664, 25);
            this.LBLbottom.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ekran Seçiniz : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Bölüm Seçiniz : ";
            // 
            // cmbBolumSec
            // 
            this.cmbBolumSec.FormattingEnabled = true;
            this.cmbBolumSec.Location = new System.Drawing.Point(209, 30);
            this.cmbBolumSec.Name = "cmbBolumSec";
            this.cmbBolumSec.Size = new System.Drawing.Size(274, 21);
            this.cmbBolumSec.TabIndex = 6;
            this.cmbBolumSec.Text = "Bölüm Seçiniz..";
            // 
            // cmbKullaniciSec
            // 
            this.cmbKullaniciSec.FormattingEnabled = true;
            this.cmbKullaniciSec.Location = new System.Drawing.Point(209, 57);
            this.cmbKullaniciSec.Name = "cmbKullaniciSec";
            this.cmbKullaniciSec.Size = new System.Drawing.Size(274, 21);
            this.cmbKullaniciSec.TabIndex = 7;
            this.cmbKullaniciSec.Text = "Kullanıcı Seçiniz..";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Kullanıcı Seçiniz : ";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.cmbKullaniciSec);
            this.panel1.Controls.Add(this.cmbScreenList);
            this.panel1.Controls.Add(this.cmbBolumSec);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 226);
            this.panel1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LBLbottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox cmbScreenList;
        private System.Windows.Forms.Label LBLbottom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBolumSec;
        private System.Windows.Forms.ComboBox cmbKullaniciSec;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
    }
}

