using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace WEINCConsole
{
    class Program
    {
        #region PUBLIC's
        public static string pScheduler = "D:WEINCTEAM/Scheduler.weinc";
        public static List<string> pListScheduler = new List<string>();
        public static string memoryUsername = "";
        public static string memoryPassword = "";
        public static Boolean pAuthorizationCheck = false;

        #endregion
        #region Pacs JPG Alıcı
        static void Goruntuler()
        {
            Console.WriteLine("Pacs Alıcı Kontrolü Başladı.! ");
            Console.WriteLine(Environment.NewLine);
        PacsTimer:
            
            try
            {
                if (!System.IO.Directory.Exists("D:\\WEINCTEAM\\project\\PacsMemory") && !System.IO.Directory.Exists("D:\\WEINCTEAM\\project\\Pacs"))
                {
                    KlasorControl();
                }

                string[] goruntuAl = System.IO.Directory.GetFiles("D:\\WEINCTEAM\\project\\PacsMemory");
                if (goruntuAl.Count() != 0)
                {
                    Thread.Sleep(5000);
                    DateTime nnow = DateTime.Now;
                    Console.WriteLine($"PACS :  {nnow}");
                    foreach (var item in goruntuAl)
                    {
                        Thread.Sleep(5000);
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
                                    Directory.CreateDirectory("D:\\WEINCTEAM\\project\\Pacs\\" + tc);
                                }
                                if (yazilar.Contains("ExaminDate"))
                                {
                                    string[] tarih = System.Text.RegularExpressions.Regex.Split(yazilar, ":");
                                    string[] tarihBol = System.Text.RegularExpressions.Regex.Split(tarih[1], "/");
                                    yil = tarihBol[0];
                                    ay = tarihBol[1];
                                    gün = tarihBol[2];
                                    Directory.CreateDirectory("D:\\WEINCTEAM\\project\\Pacs\\" + tc + "\\" + yil + "\\" + ay + "\\" + gün);
                                }
                                yazilar = sw.ReadLine();
                            }
                            sw.Close();
                            fs.Close();
                            string orjFile = $"D:\\WEINCTEAM\\project\\PacsMemory\\{subs}.jpg";
                            string copyFile = $"D:\\WEINCTEAM\\project\\Pacs\\{tc}\\{yil}\\{ay}\\{gün}\\{subs}.jpg";
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
                                        t_aktif = true,
                                        t_klasorad = klasorAd,
                                        t_resimad = $"{subs}.jpg"
                                    };
                                    db.adm_pacs.Add(pacsYolla);
                                    db.SaveChanges();
                                }
                                File.Delete($"D:\\WEINCTEAM\\project\\PacsMemory\\{subs}.jpg");
                                File.Delete($"D:\\WEINCTEAM\\project\\PacsMemory\\{subs}.txt");
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
                                Directory.CreateDirectory("D:\\WEINCTEAM\\project\\Pacs\\" + tc + "\\" + yil + "\\" + ay + "\\" + gün);
                                string orjFile = $"D:\\WEINCTEAM\\project\\PacsMemory\\{subs}.jpg";
                                string copyFile = $"D:\\WEINCTEAM\\project\\Pacs\\{tc}\\{yil}\\{ay}\\{gün}\\{subs}.jpg";
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
                                            t_aktif = true,
                                            t_klasorad = klasorAd,
                                            t_resimad = $"{subs}.jpg"
                                        };
                                        db.adm_pacs.Add(pacsYolla);
                                        db.SaveChanges();
                                    }
                                    File.Delete($"D:\\WEINCTEAM\\project\\PacsMemory\\{subs}.jpg");
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
            if (System.IO.Directory.Exists("D:\\WEINCTEAM\\project\\PacsMemory") && System.IO.Directory.Exists("D:\\project\\Pacs"))
            {
                Goruntuler();
            }
            else
            {
                Directory.CreateDirectory("D:\\WEINCTEAM\\project\\PacsMemory");
                Directory.CreateDirectory("D:\\WEINCTEAM\\project\\Pacs");
                goto PacsBasla;
            }
        }
        #endregion

        const Int32 SW_MINIMIZE = 6;
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);
        private static void MinimizeConsoleWindow()
        {
            IntPtr hWndConsole = GetConsoleWindow();
            ShowWindow(hWndConsole, SW_MINIMIZE);
        }
        static void readTXT(string filePath, List<string> list)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine + Environment.NewLine + Environment.NewLine + "Hata Mesajı : " + ex.Message);
            }
            //if (File.Exists(filePath))
            //{
            //    list.Clear();
            //    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //    StreamReader sw = new StreamReader(fs);
            //    string getLine = sw.ReadLine();
            //    while (getLine != null && getLine != "")
            //    {
            //        list.Add(getLine);
            //        getLine = sw.ReadLine();
            //    }
            //    sw.Close();
            //    fs.Close();
            //}
        }
        static void Scheduler()
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
                            //MessageBox.Show(pNotLogin);
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        //MessageBox.Show(pNotActive);
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }
        static void Main(string[] args)
        {
            MinimizeConsoleWindow();
            Console.Title = "WEINCConsole";
            Scheduler();
            KlasorControl();
        }
    }
}
