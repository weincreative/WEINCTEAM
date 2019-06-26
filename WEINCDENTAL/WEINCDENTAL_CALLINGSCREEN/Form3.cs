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
//using System.Speech;
//using System.Speech.Synthesis;
using static WEINCDENTAL_CALLINGSCREEN.Form1;

#endregion

namespace WEINCDENTAL_CALLINGSCREEN
{
    public partial class Form3 : Form
    {
        #region TANIMLAMALAR
        public Form3()
        {
            InitializeComponent();
        }
        WEINCDENTALEntities db = new WEINCDENTALEntities();
        //SpeechSynthesizer reader = new SpeechSynthesizer();
        public static int readerSayar = 0;
        public static string Memory = null;
        DateTime nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        DateTime sonrakiDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(1);
        #endregion

        #region VOIDS
        private void form3Close()
        {
            form3CloseValue = 0;
            form3CloseMuValue = 1;
            tmrForm2CagirildiHasta.Enabled = false;
            tmrForm2CagirilcakHasta.Enabled = false;
            this.Close();
        }

        public void cagriEkraniGetir(int id)
        {
            int sayacIndex = 0;
            cagrilacakList.Clear();
            try
            {
                var _cagrilcakList = db.hst_basvuru.Where(c => c.t_cagriekraniistem == 0 && c.t_bolumkodu == id && c.t_basvurudr == 0 && c.t_aktif == true && c.t_taburcu == false).Where(k => k.t_basvurutarihi >= nowDate && k.t_basvurutarihi < sonrakiDate).ToList();
                if (_cagrilcakList != null)
                {
                    foreach (var item in _cagrilcakList)
                    {
                        cagrilacakList.Add(new cagrilacak { SIRA = sayacIndex, BASVURUNO = item.t_id, HASTAADI = item.hst_hastakarti.t_adi, HASTASOYADI = item.hst_hastakarti.t_soyadi });
                        sayacIndex++;
                    }
                    dgvHastaListesi1.DataSource = cagrilacakList.ToList();
                }
                tmrForm2CagirilcakHasta.Enabled = true;
            }
            catch (Exception)
            {
                //MessageBox.Show("HATA MESAJI 5: " + x);
            }
        }

        public void cagrilanHastaGetir(int id)
        {
            int sayacIndex = 0;
            lblCagirilanHasta1.Text = null;
            cagrildiList.Clear();

            try
            {
                var _cagrilanList = db.hst_basvuru.Where(c => c.t_cagriekraniistem == 0 && c.t_bolumkodu == id && c.t_basvurudr == 1 && c.t_aktif == true && c.t_taburcu == false).Where(k => k.t_basvurutarihi >= nowDate && k.t_basvurutarihi < sonrakiDate).ToList();
                if (_cagrilanList.Count != 0)
                {
                    foreach (var item in _cagrilanList)
                    {
                        cagrildiList.Add(new cagrildi { CGRLDI_LID = sayacIndex, CGRLDI_ID = item.t_id, CGRLDI_AD = item.hst_hastakarti.t_adi, CGRLDI_SOYAD = item.hst_hastakarti.t_soyadi });
                        sayacIndex++;
                    }
                    lblCagirilanHasta1.Text = cagrildiList[0].CGRLDI_AD + " " + cagrildiList[0].CGRLDI_SOYAD;
                    if (Memory != lblCagirilanHasta1.Text)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                        player.SoundLocation = "DingDong.wav";
                        player.Load();
                        player.PlaySync();
                        Memory = cagrildiList[0].CGRLDI_AD + " " + cagrildiList[0].CGRLDI_SOYAD;
                        readerSayar = 0;
                        lblBilgilendirme1.Text = "SIRADA'Kİ HASTA";
                        tmrBilgilendirme.Enabled = true;
                    }

                }
                tmrForm2CagirildiHasta.Enabled = true;
            }
            catch (Exception)
            {
                //MessageBox.Show("HATA MESAJI 4: " + x);
            }
        }

        #endregion

        #region FORM2 LOAD
        private void Form2_Load(object sender, EventArgs e)
        {
            form2CloseValue = 0;
            form2CloseMuValue = 0;
            this.Text = uygulamaAdı;
            this.Bounds = Screen.AllScreens[pencereSec].Bounds;
            tmrForm2CloseValue.Enabled = true;
            tmrSiradakiHYanSon.Enabled = true;
            cagrilanHastaGetir(_BIDGetirList[secilenBolumVAL].BID);
            cagriEkraniGetir(_BIDGetirList[secilenBolumVAL].BID);
            lblHastaneAdi.Text = uygulamaAdı;
            lblBolumDoktorBilgisi1.Text = $"Bölüm : {_BIDGetirList[secilenBolumVAL].BAD} - Doktorunuz : {_KIDGetirList[secilenKullaniciVAL].KAD}";
        }

        #endregion

        #region SAGTIK
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form3Close();
        }

        #endregion

        #region TIMERS
        private void tmrForm2CloseValue_Tick(object sender, EventArgs e)
        {

        }
        private void tmrForm2CagirildiHasta_Tick(object sender, EventArgs e)
        {
            cagrilanHastaGetir(_BIDGetirList[secilenBolumVAL].BID);
        }
        private void tmrForm2CagirilcakHasta_Tick(object sender, EventArgs e)
        {
            cagriEkraniGetir(_BIDGetirList[secilenBolumVAL].BID);
        }
        private void tmrSiradakiHYanSon_Tick(object sender, EventArgs e)
        {

        }
        private void tmrBilgilendirme_Tick(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
