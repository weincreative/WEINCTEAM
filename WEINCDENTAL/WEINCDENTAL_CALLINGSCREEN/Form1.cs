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
        public static int form2CloseMuValue { get; set; }
        public static int form3CloseValue { get; set; }
        public static int form3CloseMuValue { get; set; }
        public static int secilenBolumVAL { get; set; }
        public static int secilenKullaniciVAL { get; set; }
        public static List<cagrilacak> cagrilacakList = new List<cagrilacak>();
        public static List<cagrildi> cagrildiList = new List<cagrildi>();
        public static List<cagrilmis> cagrilmisList = new List<cagrilmis>();
        public static List<secilenBIDbul> _BIDGetirList = new List<secilenBIDbul>();
        public static List<secilenKIDbul> _KIDGetirList = new List<secilenKIDbul>();

        #region LIST CLASS
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
        public class cagrilacak
        {
            public int SIRA { get; set; }
            public int BASVURUNO { get; set; }
            public string HASTAADI { get; set; }
            public string HASTASOYADI { get; set; }
        }
        public class cagrilmis
        {
            public int CGRLMS_LID { get; set; }
            public int CGRLMS_ID { get; set; }
            public string CGRLMS_AD { get; set; }
            public string CGRLMS_SOYAD { get; set; }
        }
        public class cagrildi
        {
            public int CGRLDI_LID { get; set; }
            public int CGRLDI_ID { get; set; }
            public string CGRLDI_AD { get; set; }
            public string CGRLDI_SOYAD { get; set; }
        }

        #endregion

        #endregion

        #region VOIDS
        private void GetScreenList()
        {
            try
            {
                cmbScreenList.Text = "Ekran Seçiniz..";
                cmbScreenList.Items.Clear();
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                foreach (var item in allScreens)
                {
                    pencereSec = 0;
                    cmbScreenList.Enabled = true;
                    cmbScreenList.Items.Add("Monitör Adı : " + item.DeviceName.ToString() + " - Ana Ekran mı ? : " + item.Primary);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("HATA MESAJI : " + x);
            }
        }

        private void GetComboDoldur()
        {
            _BIDGetirList.Clear();
            _KIDGetirList.Clear();
            cmbBolumSec.Items.Clear();
            cmbBolumSec.Text = "Bölüm Seçiniz..";
            cmbKullaniciSec.Items.Clear();
            cmbKullaniciSec.Text = "Kullanıcı Seçiniz..";
            try
            {
                if (cmbBolumSec.Text == "Bölüm Seçiniz.." && cmbKullaniciSec.Text =="Kullanıcı Seçiniz..")
                {
                    var _bolumList = db.hst_bölüm.Where(x => x.t_aktif == true).ToList();
                    var _kullanıcıList = db.adm_kullanicilar.Where(x => x.t_aktif == true).ToList();
                    int bolumIndexControl = 0;
                    int kullaniciIndexControl = 0;
                    secilenBolumVAL = 0;
                    secilenKullaniciVAL = 0;
                    foreach (var item in _bolumList)
                    {
                        _BIDGetirList.Add(new secilenBIDbul { BLID = bolumIndexControl, BID = item.t_id, BAD = item.t_adi });
                        cmbBolumSec.Items.Add(item.t_adi);
                        bolumIndexControl++;
                    }
                    foreach (var item in _kullanıcıList)
                    {
                        _KIDGetirList.Add(new secilenKIDbul { KLID = kullaniciIndexControl, KID = item.t_id, KAD = item.t_adi });
                        cmbKullaniciSec.Items.Add(item.t_adi);
                        kullaniciIndexControl++;
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("HATA MESAJI : " + x);
            }
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
                if (cmbScreenList.Text != "Ekran Seçiniz.." && cmbBolumSec.Text != "Bölüm Seçiniz..")
                {

                    Form2 _form2 = new Form2();
                    pencereSec = cmbScreenList.SelectedIndex;
                    secilenBolumVAL = _BIDGetirList[cmbBolumSec.SelectedIndex].BLID;
                    secilenKullaniciVAL = _KIDGetirList[cmbKullaniciSec.SelectedIndex].KLID;
                    _form2.Show();
                    btnStart.Enabled = false;
                    cmbBolumSec.Enabled = false;
                    cmbKullaniciSec.Enabled = false;
                    btnStop.Enabled = true;
                    form2CloseValue = 0;
                    form2CloseMuValue = 0;
                    tmrForm2CloseMu.Enabled = true;
                    LBLbottom.Text = "Çağrı Ekranını Durdurana Kadar Bir İşlem Yapamayacaksınız..";
                }
                else
                {
                    MessageBox.Show("Ekran Seçimi veya Bölüm Seçimi Yapmadınız..");
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("HATA MESAJI 1: " + x);
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
                form2CloseMuValue = 0;
            }
            catch (Exception)
            {
                //MessageBox.Show("HATA MESAJI 2: " + x);
            }
        }

        private void cmbScreenList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbScreenList.Enabled = false;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        #region TIMERS
        private void tmrForm2CloseMu_Tick(object sender, EventArgs e)
        {
            if (form2CloseMuValue != 0)
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
                    form2CloseMuValue = 0;
                    tmrForm2CloseMu.Enabled = false;
                    secilenBolumVAL = 0;
                    secilenKullaniciVAL = 0;
                }
                catch (Exception)
                {
                    //MessageBox.Show("HATA MESAJI 3: " + x);
                }
            }
        }

        #endregion


    }
}
