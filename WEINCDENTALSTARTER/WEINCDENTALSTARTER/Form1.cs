
#region USING
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;
#endregion

namespace WEINCDENTALSTARTER
{
    public partial class Form1 : Form
    {
        #region PUBLIC's
        public string memoryUsername = "";
        public string memoryPassword = "";
        public Boolean pAuthorizationCheck = false;
        public string pNotLogin = "WEINCLAUNCHER programından giriş yapmanız gerekiyor..";
        public string pNotActive = "Giriş Yaptığınız kullanıcı aktif kullanıcı değildir // Lütfen yönetici ile görüşünüz [WEINCREATIVE]";
        DateTime toDay = DateTime.Today;

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        void serialOptionsOpen()
        {
            try
            {
                using (WEINCOPTIONSEntities options = new WEINCOPTIONSEntities())
                {
                    var result = options.hst_weincoptions.Where(b => b.t_id == 1).FirstOrDefault();
                    if (result.t_yetki != null && result.t_aktif != null)
                    {
                        result.t_serial = "99999999999999999999";
                        options.Entry(result).State = EntityState.Modified;
                        options.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata :" + Environment.NewLine + "Kullanıcı girişi yapmadınız // WEINCLAUNCHER 'dan giriş yapınız");
            }
        }
        void serialOptionsClose()
        {
            try
            {
                using (WEINCOPTIONSEntities options = new WEINCOPTIONSEntities())
                {
                    var result = options.hst_weincoptions.Where(b => b.t_id == 1).FirstOrDefault();
                    if (result.t_yetki != null && result.t_aktif != null)
                    {
                        result.t_serial = "";
                        options.Entry(result).State = EntityState.Modified;
                        options.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        void optionsAktive()
        {
            using (WEINCOPTIONSEntities options = new WEINCOPTIONSEntities())
            {
                var result = options.hst_weincoptions.Where(b => b.t_id == 1).FirstOrDefault();
                if (result.t_yetki == null && result.t_aktif == null)
                {
                    MessageBox.Show("Hata :" + Environment.NewLine + "Kullanıcı girişi yapmadınız // WEINCLAUNCHER 'dan giriş yapınız");
                }
            }
        }
        void thisDayPACS()
        {
            using (WEINCDENTALEntities db = new WEINCDENTALEntities())
            {
                var result = db.adm_pacs.Where(b => b.t_createdate == toDay).ToList();
                if (result != null)
                {
                    listBox1.Items.Clear();
                    foreach (var item in result)
                    {
                        listBox1.Items.Add(item.t_resimad);
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            optionsAktive();
            timer2.Start();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialOptionsClose();
            timer2.Stop();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "BAŞLAT")
            {
                button1.Text = "DURDUR";
                timer1.Start();
                Process.Start(@"D:\WEINCTEAM\Console\WEINCConsole.exe");
            }
            else if (button1.Text == "DURDUR")
            {
                //timer1.Stop();
                //button1.Text = "BAŞLAT";
                foreach (var process in Process.GetProcessesByName("WEINCConsole"))
                {
                    timer1.Stop();
                    process.Kill();
                    button1.Text = "BAŞLAT";
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "BAŞLAT")
            {
                button2.Text = "DURDUR";
                serialOptionsOpen();
            }
            else if (button2.Text == "DURDUR")
            {
                button2.Text = "BAŞLAT";
                serialOptionsClose();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            thisDayPACS();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            optionsAktive();
        }


    }
}
