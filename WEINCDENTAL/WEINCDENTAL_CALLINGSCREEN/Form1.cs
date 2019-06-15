#region USING
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

#endregion

namespace WEINCDENTAL_CALLINGSCREEN
{
    public partial class Form1 : Form
    {
        #region TANIMLAMALAR
        public Form1()
        {
            this.Text = uygulamaAdı;
            InitializeComponent();
        }
        WEINCDENTALEntities db = new WEINCDENTALEntities();
        public static string uygulamaAdı { get; set; } = "Özel Akademi Ağız ve Diş Sağlığı Polikliniği";
        public static int pencereSec { get; set; }
        public static int form2CloseValue { get; set; }
        public static int secilenBolumVAL { get; set; }
        public static int secilenKullanıcıVal { get; set; }
        public static List<string> cagrilacakList = new List<string>();
        public static List<string> cagrilmisList = new List<string>();
        public static List<secilenBIDbul> _BIDGetirList = new List<secilenBIDbul>();
        public static List<secilenKIDbul> _KIDGetirList = new List<secilenKIDbul>();
        public class secilenBIDbul
        {
            public int BLID { get; set; }
            public int BID { get; set; }
            public string BAD { get; set; }
        }
        public class secilenKIDbul
        {
            public int KLID { get; set; }
            public int KID { get; set; }
            public string KAD { get; set; }
        }


        #endregion

        #region VOIDS
        private void GetScreenList()
        {
            try
            {
                cmbScreenList.Items.Clear();
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                foreach (var item in allScreens)
                {
                    pencereSec = 0;
                    cmbScreenList.Enabled = true;
                    cmbScreenList.Items.Add("Monitör Adı : " + item.DeviceName.ToString() + " - Ana Ekran mı ? : " + item.Primary);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("HATA MESAJI : " + x);
            }
        }

        private void GetComboDoldur()
        {
            try
            {
                  var _bolumList = db.hst_bölüm.Where(x => x.t_aktif == true).ToList();
                  var _kullanıcıList = db.adm_kullanicilar.Where(x => x.t_aktif == true).ToList();
                int bolumIndexControl = 0;
                int kullaniciIndexControl = 0;
                    foreach (var item in _bolumList)
                    {
                        _BIDGetirList.Add(new secilenBIDbul { BLID = bolumIndexControl, BID = item.t_id, BAD = item.t_adi });
                        cmbBolumSec.Items.Add(item.t_adi + " " + item.t_id);
                        bolumIndexControl++;
                    }
                    foreach (var item in _kullanıcıList)
                    {
                        _KIDGetirList.Add(new secilenKIDbul { KLID = kullaniciIndexControl, KID = item.t_id, KAD = item.t_adi });
                        cmbKullaniciSec.Items.Add(item.t_adi+" "+item.t_id);
                        kullaniciIndexControl++;
                    }
            }
            catch (Exception x)
            {
                MessageBox.Show("HATA MESAJI : " + x);
            }

        }

        private void SecilenBKIDGetir(int blmVal,int kllncVal)
        {

        }

        #endregion  

        #region FORM1 LOAD
        private void Form1_Load(object sender, EventArgs e)
        {
            GetScreenList();
            GetComboDoldur();
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 _form2 = new Form2();
                pencereSec = cmbScreenList.SelectedIndex;
                _form2.Show();
                btnStart.Enabled = false;
                cmbBolumSec.Enabled = false;
                cmbKullaniciSec.Enabled = false;
                btnStop.Enabled = true;
                GetComboDoldur();
                SecilenBKIDGetir(cmbBolumSec.SelectedIndex, cmbKullaniciSec.SelectedIndex);
                LBLbottom.Text = "Çağrı Ekranını Durdurana Kadar Bir İşlem Yapamayacaksınız..";
            }
            catch (Exception x)
            {
                MessageBox.Show("HATA MESAJI : " + x);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                form2CloseValue = 1;
                btnStop.Enabled = false;
                btnStart.Enabled = true;
                cmbBolumSec.Enabled = true;
                cmbKullaniciSec.Enabled = true;
                GetScreenList();
                GetComboDoldur();
                LBLbottom.Text = "";
            }
            catch (Exception x)
            {
                MessageBox.Show("HATA MESAJI : " + x);
            }
        }

        private void cmbScreenList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbScreenList.Enabled = false;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
