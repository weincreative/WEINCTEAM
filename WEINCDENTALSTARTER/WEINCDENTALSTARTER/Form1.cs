
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
        public string pScheduler = "D:WEINCTEAM/Scheduler.weinc";
        public static List<string> pListScheduler = new List<string>();
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
                    //var result = options.hst_weincoptions.FirstOrDefault(b => b.t_kullanici == memoryUsername && b.t_sifre == memoryPassword);
                    //var result = options.hst_weincoptions.SingleOrDefault(b => b.t_kullanici == memoryUsername && b.t_sifre == memoryPassword);
                    if (result != null)
                    {
                        if (result.t_kullanici == memoryUsername && result.t_sifre == memoryPassword)
                        {
                            result.t_serial = "987987987";
                            //change other properties ..
                            options.Entry(result).State = EntityState.Modified;
                            options.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Kodu:" + ex);
            }

            //try
            //{
            //    if (cmbBolumSec.Text == "Bölüm Seçiniz.." && cmbKullaniciSec.Text == "Kullanıcı Seçiniz..")
            //    {
            //        var _bolumList = db.hst_bölüm.Where(x => x.t_aktif == true).ToList();
            //        var _kullanıcıList = db.adm_kullanicilar.Where(x => x.t_aktif == true).ToList();
            //        int bolumIndexControl = 0;
            //        int kullaniciIndexControl = 0;
            //        secilenBolumVAL = 0;
            //        secilenKullaniciVAL = 0;
            //        foreach (var item in _bolumList)
            //        {
            //            _BIDGetirList.Add(new secilenBIDbul { BLID = bolumIndexControl, BID = item.t_id, BAD = item.t_adi });
            //            cmbBolumSec.Items.Add(item.t_adi);
            //            bolumIndexControl++;
            //        }
            //        foreach (var item in _kullanıcıList)
            //        {
            //            _KIDGetirList.Add(new secilenKIDbul { KLID = kullaniciIndexControl, KID = item.t_id, KAD = item.t_adi });
            //            cmbKullaniciSec.Items.Add(item.t_adi);
            //            kullaniciIndexControl++;
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    //MessageBox.Show("HATA MESAJI : " + x);
            //}



            //using (WEINCOPTIONSEntities options = new WEINCOPTIONSEntities())
            //{
            //    var result = options.hst_weincoptions.Where(b => b.t_kullanici == memoryUsername && b.t_sifre == memoryPassword).First();
            //    if (result != null)
            //    {
            //        result.t_serial = "987987987";
            //        options.SaveChanges();
            //    }
            //}
        }
        void serialOptionsClose()
        {
            try
            {
                using (WEINCOPTIONSEntities options = new WEINCOPTIONSEntities())
                {
                    var result = options.hst_weincoptions.Where(b => b.t_id == 1).FirstOrDefault();
                    //var result = options.hst_weincoptions.FirstOrDefault(b => b.t_kullanici == memoryUsername && b.t_sifre == memoryPassword);
                    //var result = options.hst_weincoptions.SingleOrDefault(b => b.t_kullanici == memoryUsername && b.t_sifre == memoryPassword);
                    if (result != null)
                    {
                        if (result.t_kullanici == memoryUsername && result.t_sifre == memoryPassword)
                        {
                            result.t_serial = "1";
                            //change other properties ..
                            options.Entry(result).State = EntityState.Modified;
                            options.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Kodu:" + ex);
            }
            //using (var options = new WEINCOPTIONSEntities())
            //{
            //    var result = options.hst_weincoptions.SingleOrDefault(b => b.t_kullanici == memoryUsername && b.t_sifre == memoryPassword);
            //    if (result != null)
            //    {
            //        result.t_serial = "";
            //        options.SaveChanges();
            //    }
            //}
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
        public void readTXT(string filePath, List<string> list)
        {
            if (File.Exists(filePath))
            {
                list.Clear();
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                string getLine = sw.ReadLine();
                while (getLine != null && getLine != "")
                {
                    list.Add(getLine);
                    getLine = sw.ReadLine();
                }
                sw.Close();
                fs.Close();
            }
        }
        public void Scheduler()
        {
            readTXT(pScheduler, pListScheduler);
            if (pListScheduler.Count != 0)
            {
                foreach (var item in pListScheduler)
                {
                    string[] savedUser = item.Split('|');
                    if (savedUser[2] != "0" && savedUser[3] != "0")
                    {
                        if (savedUser[2] != "9999" && savedUser[3] != "9999")
                        {
                            memoryUsername = "";
                            memoryPassword = "";
                            pAuthorizationCheck = false;
                            memoryUsername = savedUser[0];
                            memoryPassword = savedUser[1];
                        }
                        else
                        {
                            MessageBox.Show(pNotLogin);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        MessageBox.Show(pNotActive);
                        Application.Exit();
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Scheduler();
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
                Process.Start(@"D:\WEINCTEAM\APPS\Console\WEINCConsole.exe");
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
            Scheduler();
        }


    }
}
