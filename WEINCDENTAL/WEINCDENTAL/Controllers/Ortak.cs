﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    
    public class Ortak
    {
        public static string _hastatc="";
        public static int _hastayas;


        // Son Kaydın İd sini alıyor
        public int GetSonIndex()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            var donen = 0;
            try
            {
                donen = db.hst_basvuru.Max(t => t.t_id);


            }
            catch (Exception e)
            {
                return donen;
            }


            return donen;
        }

    }
}