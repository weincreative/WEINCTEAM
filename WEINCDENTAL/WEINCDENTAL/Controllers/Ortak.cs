﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class Ortak
    {
        public static string _hastatc;


        // Son Kaydın İd sini alıyor
        public int GetSonIndex()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            var donen=db.hst_basvuru.Max(t=>t.t_id);

            return donen;
        }

    }
}