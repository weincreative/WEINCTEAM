using System;
#region USING
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;

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

        #endregion

        public Form1()
        {
            InitializeComponent();
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
                            MessageBox.Show("GİRİŞ BAŞARILIDIR");
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
        }
    }
}
