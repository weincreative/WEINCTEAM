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

#endregion

namespace WEINCDENTAL_CALLINGSCREEN
{
    public partial class Form2 : Form
    {
        #region TANIMLAMALAR
        public Form2()
        {
            InitializeComponent();
        }
        WEINCDENTALEntities db = new WEINCDENTALEntities();
        //DateTime nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        #endregion

        #region VOIDS
        private void form2Close()
        {
            Form1.form2CloseValue = 0;
            this.Close();
        }

        public void cagriEkraniGetir()
        {
            //listBox1.Items.Clear();
            //cagriListesi.Clear();
            //LBsiradakiHasta1.Text = null;
            //LBsiradakiHasta2.Text = null;
            //WEINCDENTALEntities db = new WEINCDENTALEntities();
                try
                {
                    var _cagriList = db.hst_basvuru.Where(x => x.t_cagriekraniistem == 0).Where(x => x.t_bolumkodu == 1).Where(x => x.t_basvurudr == 0)/*.Where(x => x.t_basvurutarihi == nowDate)*/.Where(x => x.t_aktif == true).Where(x => x.t_taburcu == false).ToList().OrderByDescending(x => x.t_basvurutarihi);
                    if (_cagriList != null)
                    {
                        foreach (var item in _cagriList)
                        {
                            //cagriListesi.Add(item.hst_hastakarti.t_adi + " " + item.hst_hastakarti.t_soyadi + " " + item.t_basvurutarihi);
                            //listBox1.Items.Add(item.hst_hastakarti.t_adi + " " + item.hst_hastakarti.t_soyadi + " " + item.t_basvurutarihi);
                            //LBsiradakiHasta1.Text = cagriListesi[0];
                        }
                        //LBsiradakiHasta1.Text = cagriListesi[0];
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show("HATA MESAJI : " + x);
                }
        }

        public void cagrilanHastaGetir()
        {
            //listBox1.Items.Clear();
            //cagriListesi.Clear();
            //LBsiradakiHasta1.Text = null;
            //LBsiradakiHasta2.Text = null;
            //WEINCDENTALEntities db = new WEINCDENTALEntities();
            try
            {
                var _cagrilanList = db.hst_basvuru.Where(c => c.t_cagriekraniistem == 0).Where(c => c.t_bolumkodu == 1).Where(c => c.t_basvurudr == 1)/*.Where(x => x.t_basvurutarihi == nowDate)*/.Where(c => c.t_aktif == true).Where(c => c.t_taburcu == false).ToList();
                if (_cagrilanList != null)
                {
                    foreach (var item in _cagrilanList)
                    {
                        //LBiceridekiHasta.Text = "İÇERİDE'Kİ HASTA";
                        //LBLcagriDoktor.Text = "Doktorunuz : " + comboBox1.SelectedItem.ToString() + "'in odasına gidiniz.";
                        //cagrilmisListesi.Add(item.hst_hastakarti.t_adi + " " + item.hst_hastakarti.t_soyadi);
                        //LBLcagrilanHasta1.Text = item.hst_hastakarti.t_adi + " " + item.hst_hastakarti.t_soyadi;
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("HATA MESAJI : " + x);
            }
        }

        #endregion

        #region FORM2 LOAD
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = Form1.uygulamaAdı;
            this.Bounds = Screen.AllScreens[Form1.pencereSec].Bounds;
            tmrForm2CloseValue.Enabled = true;
        }

        #endregion

        #region SAGTIK
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2Close();
        }

        #endregion

        #region TIMERS
        private void tmrForm2CloseValue_Tick(object sender, EventArgs e)
        {
            if (Form1.form2CloseValue !=0)
            {
                form2Close();
                tmrForm2CloseValue.Enabled = false;
            }
        }

        #endregion
    }
}
