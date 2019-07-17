using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace WEINCDENTAL_PACSConsole
{

    class Program
    {

        #region Pacs JPG Alıcı
        static void Goruntuler()
        {
            Console.WriteLine("Pacs Alıcı Kontrolü Başladı.! ");
            Console.WriteLine(Environment.NewLine);
        PacsTimer:
            try
            {
                if (!System.IO.Directory.Exists("D:\\project\\PacsMemory") && !System.IO.Directory.Exists("D:\\project\\Pacs"))
                {
                    KlasorControl();
                }

                string[] goruntuAl = System.IO.Directory.GetFiles("D:\\project\\PacsMemory");
                if (goruntuAl.Count() != 0)
                {
                    DateTime nnow = DateTime.Now;
                    Console.WriteLine($"PACS :  {nnow}");
                    foreach (var item in goruntuAl)
                    {
                        string itemName = Path.GetFileName(item);
                        string subs = itemName.Substring(0, itemName.Length - 4);

                        #region iDixel Prepikal PACS
                        if (item.Contains(".txt"))
                        {
                            System.IO.FileInfo PacsBoyutu = new System.IO.FileInfo(item);
                            FileStream fs = new FileStream(item, FileMode.Open, FileAccess.Read);
                            StreamReader sw = new StreamReader(fs, Encoding.GetEncoding("iso-8859-9"), false);
                            string tc = null;
                            string yil = null;
                            string ay = null;
                            string gün = null;
                            string yazilar = sw.ReadLine();
                            while (yazilar != null)
                            {
                                if (yazilar.Contains("StdGivenName"))
                                {
                                    string[] tcGeldi = System.Text.RegularExpressions.Regex.Split(yazilar, ":");
                                    tc = tcGeldi[1];
                                    Directory.CreateDirectory("D:\\project\\Pacs\\" + tc);
                                }
                                if (yazilar.Contains("ExaminDate"))
                                {
                                    string[] tarih = System.Text.RegularExpressions.Regex.Split(yazilar, ":");
                                    string[] tarihBol = System.Text.RegularExpressions.Regex.Split(tarih[1], "/");
                                    yil = tarihBol[0];
                                    ay = tarihBol[1];
                                    gün = tarihBol[2];

                                    Directory.CreateDirectory("D:\\project\\Pacs\\" + tc + "\\" + yil + "\\" + ay + "\\" + gün);
                                }
                                yazilar = sw.ReadLine();
                            }
                            sw.Close();
                            fs.Close();
                            string orjFile = $"D:\\project\\PacsMemory\\{subs}.jpg";
                            string copyFile = $"D:\\project\\Pacs\\{tc}\\{yil}\\{ay}\\{gün}\\{subs}.jpg";
                            string klasorAd = $"{tc}\\{yil}\\{ay}\\{gün}";
                            string getTarih = $"{yil}-{ay}-{gün}";

                            try
                            {
                                Console.Write(Environment.NewLine + $"{subs} _{PacsBoyutu.Length} : ");
                                File.Move(orjFile, copyFile);
                                using (WEINCDENTALEntities db = new WEINCDENTALEntities())
                                {
                                    adm_pacs pacsYolla = new adm_pacs()
                                    {
                                        //t_modality = IO,
                                        t_tc = tc,
                                        t_pacspath = copyFile,
                                        t_ip = 0,
                                        t_createdate = Convert.ToDateTime(getTarih),
                                        t_createuser = "W3",
                                        t_aktif = 1,
                                        t_klasorad = klasorAd,
                                        t_resimad = $"{subs}.jpg"
                                    };
                                    db.adm_pacs.Add(pacsYolla);
                                    db.SaveChanges();
                                }
                                File.Delete($"D:\\project\\PacsMemory\\{subs}.jpg");
                                File.Delete($"D:\\project\\PacsMemory\\{subs}.txt");
                                Console.Write("[AKTARILDI]");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"HATA OLUŞTU _ DataBaseye Kaydedilemedi.");
                                Console.WriteLine($"{ex.Message}");
                                Thread.Sleep(5000);
                                goto PacsTimer;
                            }
                        }
                        #endregion

                        #region Morita Panoramik PACS
                        else
                        {
                            string tc = null;
                            string yil = null;
                            string ay = null;
                            string gün = null;
                            try
                            {
                                System.IO.FileInfo PacsBoyutu = new System.IO.FileInfo(item);
                                //Dosya ADI = 12312312300-2019-07-16
                                string[] morita = System.Text.RegularExpressions.Regex.Split(subs, "-");
                                tc = morita[0];
                                yil = morita[1];
                                ay = morita[2];
                                gün = morita[3];
                                Directory.CreateDirectory("D:\\project\\Pacs\\" + tc + "\\" + yil + "\\" + ay + "\\" + gün);
                                string orjFile = $"D:\\project\\PacsMemory\\{subs}.jpg";
                                string copyFile = $"D:\\project\\Pacs\\{tc}\\{yil}\\{ay}\\{gün}\\{subs}.jpg";
                                string klasorAd = $"{tc}\\{yil}\\{ay}\\{gün}";
                                string getTarih = $"{yil}-{ay}-{gün}";

                                try
                                {
                                    Console.Write(Environment.NewLine + $"{subs} _{PacsBoyutu.Length} : ");
                                    File.Move(orjFile, copyFile);
                                    using (WEINCDENTALEntities db = new WEINCDENTALEntities())
                                    {
                                        adm_pacs pacsYolla = new adm_pacs()
                                        {
                                            //t_modality = PX,
                                            t_tc = tc,
                                            t_pacspath = copyFile,
                                            t_ip = 0,
                                            t_createdate = Convert.ToDateTime(getTarih),
                                            t_createuser = "W3",
                                            t_aktif = 1,
                                            t_klasorad = klasorAd,
                                            t_resimad = $"{subs}.jpg"
                                        };
                                        db.adm_pacs.Add(pacsYolla);
                                        db.SaveChanges();
                                    }
                                    File.Delete($"D:\\project\\PacsMemory\\{subs}.jpg");
                                    Console.Write("[AKTARILDI]");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"HATA OLUŞTU _ DataBaseye Kaydedilemedi.");
                                    Console.WriteLine($"{ex.Message}");
                                    Thread.Sleep(5000);
                                    goto PacsTimer;
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.Write(Environment.NewLine);
                                //Thread.Sleep(5000);
                                //goto PacsTimer;
                            }

                        }
                        #endregion
                    }
                    Console.Write(Environment.NewLine);
                    Thread.Sleep(5000);
                    goto PacsTimer;
                }
                else
                {
                    Thread.Sleep(5000);
                    goto PacsTimer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA OLUŞTU _Veriler İşlenirken Hata Oluştu");
                Thread.Sleep(5000);
                goto PacsTimer;
            }

        }

        static void KlasorControl()
        {
        PacsBasla:
            if (System.IO.Directory.Exists("D:\\project\\PacsMemory") && System.IO.Directory.Exists("D:\\project\\Pacs"))
            {
                Goruntuler();
            }
            else
            {
                Directory.CreateDirectory("D:\\project\\PacsMemory");
                Directory.CreateDirectory("D:\\project\\Pacs");
                goto PacsBasla;
            }
        }

        #endregion
        static void Main(string[] args)
        {
            Console.Title = "WEINCREATIVE PACS";
            KlasorControl();
        }
    }
}
