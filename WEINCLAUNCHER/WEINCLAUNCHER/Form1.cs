#region USING
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;

#endregion

namespace WEINCLAUNCHER
{
    //FORM BOYUTLARI = 374; 184
    //FORM AÇIK HALİ = 374; 735
    //FORM INSTAGRAM = 1111; 735
    //File.Move(pScheduler, Path.ChangeExtension(pScheduler, ".txt"));

    public partial class Form1 : Form
    {
        #region PUBLIC's
        public Boolean isADMIN = false;
        public string pAdress = "ftp://ftp.weincreative.com/_AUTHENTICATION/";
        public string pAuthentication = "AUTHENTICATION.weinc";
        public string pScheduler = "Scheduler.weinc";
        public string pUsername = "dcoweinc";
        public string pPassword = "85[dWiC6";
        public string pVersion = "";
        public string pUserAuth = "";
        public static List<string> pListMemory = new List<string>();
        public static List<string> pListAuthModules = new List<string>();
        public static List<string> pListScheduler = new List<string>();
        public Boolean pAdminPanel = false;
        public string pAuthorization = "";
        public Boolean pAuthorizationCheck = false;
        public Boolean pLoginFalse = false;
        public Boolean pNotActivetedUser = false;
        public string memoryUsername = "";
        public string memoryPassword = "";

        #endregion
        #region ERRORS
        public string pErrorDownload = "Dosya İndirilirken Servere Ulaşılamadı..";
        public string pErrorUpload = "Dosya Yüklenirken Servere Ulaşılamadı..";
        public string pErrorIsNotActive = "Kullanıcınız aktif edilmemiştir.. // Yönetici ile Görüşünüz.. ";
        public string pLogInTrue = "Giriş Başarılıdır..";
        public string pSingUpTrue = "Kayıt Başarılıdır..";
        public string pSingUpFalse = "Kayıtta Sıkıntı Çıktı..";
        public string pLogInFalse = "Hatalı Kullanıcı adı veya Şifre..";
        public string pWriteLogoinTrueUser = "Kullanıcı Bilgileri Kaydedildi..";
        public string pWriteLogoutTrueUser = "Kullanıcı Bilgileri Kaydedilemedi..";
        public string pNotSubsAnyModule = "Herhangi Programı Satın Almadınız.. // Satın Alma Ekranına Gitmelisiniz..";

        #endregion


        public Form1()
        {
            InitializeComponent();
        }
        void buttonADMIN()
        {
            //button4.Width = 320;
            //panel2.Location = new Point(panel2.Location.X + 300,panel2.Location.Y);
            //panel3.Location = new Point(panel3.Location.X + 300,panel3.Location.Y);
        }
        void userADMIN(Boolean trueFalse)
        {
            button4.Visible = trueFalse;
        }
        void userNORMAL1(Boolean trueFalse)
        {
            panel1.Visible = trueFalse;
        }
        void userNORMAL2(Boolean trueFalse)
        {
            panel2.Visible = trueFalse;
            panel3.Visible = trueFalse;
        }
        void adminLogin()
        {
            userADMIN(true);
            userNORMAL1(false);
            userNORMAL2(true);
        }
        void normalLogin()
        {
            userADMIN(false);
            userNORMAL1(false);
            userNORMAL2(true);
        }
        void userLogout()
        {
            StartingPos();
            userADMIN(false);
            userNORMAL1(true);
            userNORMAL2(false);
            pLoginFalse = false;
            pListMemory.Clear();
            File.Delete(pAuthentication);
            button2.Enabled = false;
            if (checkBox1.Checked == true)
            {
                if (pNotActivetedUser==false)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    memoryUsername = "";
                    memoryPassword = "";
                    pListScheduler.Clear();
                    File.Delete(pScheduler);
                }
                else
                {
                    textBox1.Text = memoryUsername;
                    textBox2.Text = "";
                    textBox2.Focus();
                    SchedulerLogout(memoryUsername);
                }
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                memoryUsername = "";
                memoryPassword = "";
                pListScheduler.Clear();
                File.Delete(pScheduler);
            }
        }
        void CenterForm()
        {
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
(Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
        public string getDownload(string Address, string filePath, string username, string password)
        {
            try
            {
                String result = String.Empty;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Address + filePath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responsestream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responsestream);
                result = reader.ReadToEnd();
                using (StreamWriter file = File.CreateText(filePath))
                {
                    file.WriteLine(result);
                    file.Close();
                }
                reader.Close();
                response.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Hata Mesajı: " + ex);
                return "";
            }
        }
        public void setUpload(string Address, string filePath, string username, string password)
        {
            try
            {
                FileInfo toUpload = new FileInfo(filePath);
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(Address + toUpload.Name);
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(username, password);
                Stream stream = req.GetRequestStream();
                FileStream file = File.OpenRead(filePath);
                int length = 1024;
                byte[] buffer = new byte[length];
                int bytesRead = 0;
                do
                {
                    bytesRead = file.Read(buffer, 0, length);
                    stream.Write(buffer, 0, bytesRead);
                } while (bytesRead != 0);
                file.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Hata Mesajı: " + ex);
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
        public void writeTXT(string filePath, string username, string password, List<string> list)
        {
            if (File.Exists(filePath))
            {
                list.Add(username + "|" + password + "|" + "0" + "|" + "0");
                File.WriteAllLines(filePath, list);
                File.Move(filePath, Path.ChangeExtension(filePath, ".weinc"));
            }
        }
        public void Authority(string userName, string userPassword, string userAuthority, string userActivated, string userItem, List<string> yetkiModulList)
        {
            yetkiModulList.Clear();
            if (userActivated != "0" && userActivated != "9999")
            {
                string[] auth = userAuthority.Split(',');
                if (userAuthority.Contains("ADMIN"))
                {
                    isADMIN = true;
                    yetkiModulList.Add("1");
                    yetkiModulList.Add("2");
                    yetkiModulList.Add("3");
                    LoginPos();
                    adminLogin();
                    SchedulerLogin(userItem);
                    pNotActivetedUser = true;
                    memoryUsername = textBox1.Text;
                    memoryPassword = textBox2.Text;

                    pListMemory.Clear();
                    File.Delete(pAuthentication);
                }
                else
                {
                    LoginPos();
                    normalLogin();
                    SchedulerLogin(userItem);
                    pNotActivetedUser = true;
                    memoryUsername = textBox1.Text;
                    memoryPassword = textBox2.Text;

                    pListMemory.Clear();
                    File.Delete(pAuthentication);
                    foreach (var item in auth)
                    {
                        isADMIN = false;
                        if (item != "ADMIN")
                        {
                            yetkiModulList.Add(item + ".exe");
                        }
                    }
                }
                if (yetkiModulList.Count != 0)
                {
                    buttonCreate(yetkiModulList);
                }
            }
            else
            {
                pNotActivetedUser = false;
                textBox1.Text = "";
                textBox2.Text = "";
                memoryUsername = "";
                memoryPassword = "";
                pListMemory.Clear();
                File.Delete(pAuthentication);
                pListScheduler.Clear();
                File.Delete(pScheduler);
                MessageBox.Show(pErrorIsNotActive);
            }
        }
        //public Tuple<int, int> LoginPos1()
        //{
        //    int posX = 386;
        //    int posY = 735;
        //    var loginPOS = new Tuple<int, int>(posX, posY);
        //    return loginPOS;
        //}
        void StartingPos()
        {
            this.Width = 386;
            this.Height = 184;
            CenterForm();
        }
        void LoginPos()
        {
            this.Width = 386;
            this.Height = 735;
            CenterForm();
        }
        public void Upload(string filePath)
        {
            setUpload(pAdress, filePath, pUsername, pPassword);
        }
        public void Download(string filePath)
        {
            getDownload(pAdress, filePath, pUsername, pPassword);
        }
        public void Scheduler()
        {
            readTXT(pScheduler, pListScheduler);
            if (pListScheduler.Count != 0)
            {
                foreach (var item in pListScheduler)
                {
                    string[] savedUser = item.Split('|');
                    if (savedUser != null)
                    {
                        memoryUsername = "";
                        memoryPassword = "";
                        pAuthorizationCheck = false;
                        memoryUsername = savedUser[0];
                        memoryPassword = savedUser[1];
                    }
                }
            }
        }
        public void SchedulerLogout(string username)
        {
            try
            {
                pListScheduler.Clear();
                File.Delete(pScheduler);
                pListScheduler.Add(username + "|" + "" + "|" + "9999" + "|" + "9999");
                File.WriteAllLines(pScheduler, pListScheduler);
                File.Move(pScheduler, Path.ChangeExtension(pScheduler, ".weinc"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(pWriteLogoutTrueUser + Environment.NewLine + ex);
            }
        }
        public void SchedulerLogin(string userItem)
        {
            try
            {
                pListScheduler.Clear();
                File.Delete(pScheduler);
                pListScheduler.Add(userItem);
                File.WriteAllLines(pScheduler, pListScheduler);
                File.Move(pScheduler, Path.ChangeExtension(pScheduler, ".weinc"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(pWriteLogoinTrueUser + Environment.NewLine + ex);
            }
        }
        public void SingUp(string usernameTextbox, string passwordTextbox)
        {
            Download(pAuthentication);
            readTXT(pAuthentication, pListMemory);
            writeTXT(pAuthentication, usernameTextbox, passwordTextbox, pListMemory);
            try
            {
                Upload(pAuthentication);
                MessageBox.Show(pSingUpTrue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(pSingUpFalse + Environment.NewLine + ex.Message);
            }

        }
        public void SingIn(string usernameTextbox, string passwordTextbox)
        {
            Download(pAuthentication);
            readTXT(pAuthentication, pListMemory);
            if (pListMemory.Count != 0)
            {
                foreach (var item in pListMemory)
                {
                    string[] correctUser = item.Split('|');
                    if (correctUser[0] == usernameTextbox && correctUser[1] == passwordTextbox)
                    {
                        Authority(correctUser[0], correctUser[1], correctUser[2], correctUser[3], item, pListAuthModules);
                        pLoginFalse = true;
                        break;
                    }
                    else
                    {
                        pLoginFalse = false;
                    }
                }
                if (!pLoginFalse)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    memoryUsername = "";
                    memoryPassword = "";
                    pListMemory.Clear();
                    File.Delete(pAuthentication);
                    pListScheduler.Clear();
                    File.Delete(pScheduler);
                    MessageBox.Show(pLogInFalse);
                }
            }

        }
        public void SingOut()
        {
            userLogout();
        }
        public void buttonCreate(List<string> appsList)
        {
            panel3.Controls.Clear();
            int EklenenButonlar_Width = 0;
            int Soldan = 3, Ustten = 3;
            for (int i = 0; i < appsList.Count; i++)
            {
                Button appsBtn = new Button()
                {
                    Height = 80,
                    Width = 80,
                    Text = appsList[i].ToString(),
                    Name = "appsButton" + i.ToString(),
                };
                panel3.Controls.Add(appsBtn);
                Soldan = (appsBtn.Width * (EklenenButonlar_Width / appsBtn.Width));
                EklenenButonlar_Width += appsBtn.Height;
                switch (EklenenButonlar_Width > panel3.Width)
                {
                    case true:
                        Soldan = 0;
                        Ustten += appsBtn.Height;
                        EklenenButonlar_Width = appsBtn.Width;
                        break;
                }
                appsBtn.Location = new Point(Soldan + 3, Ustten + 3);
                appsBtn.Click += appsBtn_Click;
            }
        }
        private void appsBtn_Click(object sender, EventArgs e)
        {
            Button appsBtn = sender as Button;
            if (File.Exists("APPS/" + appsBtn.Text + ".exe"))
            {
                MessageBox.Show("Text: " + appsBtn.Text + " - Name: " + appsBtn.Name);
            }
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            StartingPos();
            pVersion = "";
            pVersion = textBox3.Text;
            Scheduler();
            if (memoryUsername != "" && memoryPassword != "")
            {
                textBox1.Text = memoryUsername;
                textBox2.Text = memoryPassword;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            userLogout();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 4)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SingUp(textBox1.Text, textBox2.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SingIn(textBox1.Text, textBox2.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SingOut();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            buttonADMIN();
        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {

        }



    }
}
