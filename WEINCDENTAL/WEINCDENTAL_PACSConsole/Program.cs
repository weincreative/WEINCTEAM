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

        static void Goruntuler()
        {

            Console.WriteLine("Pacs Alıcı Kontrolü Başladı.! ");
            Console.WriteLine(Environment.NewLine);
        PacsTimer:
            try
            {
                string[] goruntuAl = System.IO.Directory.GetFiles("D:project/PacsMemory");
                if (goruntuAl.Count() != 0)
                {
                    DateTime nnow = DateTime.Now;
                    Console.WriteLine($"PACS :  {nnow}");
                    foreach (var item in goruntuAl)
                    {
                        if (item.Contains(".txt"))
                        {
                            string itemName = Path.GetFileName(item);
                            string subs = itemName.Substring(0, itemName.Length - 4);
                            System.IO.FileInfo PacsBoyutu = new System.IO.FileInfo(item);
                            Console.Write(Environment.NewLine + $"{subs} _{PacsBoyutu.Length} : ");
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
                                    Directory.CreateDirectory("D:project/Pacs/" + tc);
                                }
                                if (yazilar.Contains("ExaminDate"))
                                {
                                    string[] tarih = System.Text.RegularExpressions.Regex.Split(yazilar, ":");
                                    string[] tarihBol = System.Text.RegularExpressions.Regex.Split(tarih[1], "/");
                                    yil = tarihBol[0];
                                    ay = tarihBol[1];
                                    gün = tarihBol[2];

                                    Directory.CreateDirectory("D:project/Pacs/" + tc + "/" + yil + "/" + ay + "/" + gün);
                                }
                                yazilar = sw.ReadLine();
                            }
                            sw.Close();
                            fs.Close();
                            string orjFile = $"D:\\project\\PacsMemory\\{subs}.jpg";
                            string copyFile = $"D:\\project\\Pacs\\{tc}\\{yil}\\{ay}\\{gün}\\{subs}.jpg";
                            try
                            {
                                using (WEINCDENTALEntities db = new WEINCDENTALEntities())
                                {
                                    adm_pacs pacsYolla = new adm_pacs()
                                    {
                                        t_tc = tc,
                                        t_pacspath = copyFile,
                                        t_ip = 0,
                                        t_createdate = nnow,
                                        t_createuser = "W3",
                                        t_aktif = 1
                                    };
                                    db.adm_pacs.Add(pacsYolla);
                                    db.SaveChanges();
                                }
                                File.Move(orjFile, copyFile);
                                File.Delete($"D:\\project\\PacsMemory\\{subs}.jpg");
                                File.Delete($"D:\\project\\PacsMemory\\{subs}.txt");
                                Console.Write("[AKTARILDI]");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"HATA OLUŞTU _ DataBaseye Kaydedilemedi.");
                                Thread.Sleep(5000);
                                goto PacsTimer;
                            }
                        }
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

        static void Main(string[] args)
        {
            Console.Title = "WEINCREATIVE PACS";

        PacsBasla:
            if (System.IO.Directory.Exists("D:project/PacsMemory") && System.IO.Directory.Exists("D:project/Pacs"))
            {
                Goruntuler();
            }
            else
            {
                Directory.CreateDirectory("D:project/PacsMemory");
                Directory.CreateDirectory("D:project/Pacs");
                goto PacsBasla;
            }







        }
    }
}
