using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class BllPacs
    {



        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        public IEnumerable<View_Pacs> PacsList(string tc, string ad, DateTime? basDate, DateTime? bitDate)
        {
            
            IQueryable<View_Pacs> list = null;
            if (String.IsNullOrEmpty(tc))
            {
                if (basDate == null && bitDate == null && !String.IsNullOrEmpty(ad))
                {
                    list = db.View_Pacs.Where(k => k.t_adi.Contains(ad) || k.t_soyadi.Contains(ad)).AsQueryable();
                }
                else if (String.IsNullOrEmpty(ad) && basDate != null && bitDate == null)
                {
                    list = db.View_Pacs.Where(k => k.t_createdate >= basDate).AsQueryable();
                }
                else if (String.IsNullOrEmpty(ad) && basDate == null && bitDate != null)
                {
                    list = db.View_Pacs.Where(k => k.t_createdate <= bitDate).AsQueryable();
                }
                else if (!String.IsNullOrEmpty(ad) && basDate != null && bitDate == null)
                {
                    list = db.View_Pacs.Where(k => k.t_createdate >= basDate || k.t_adi.Contains(ad) || k.t_soyadi.Contains(ad)).AsQueryable();
                }
                else if (String.IsNullOrEmpty(ad) && basDate != null && bitDate != null)
                {
                    list = db.View_Pacs.Where(k => k.t_createdate >= basDate && k.t_createdate <= bitDate)
                        .AsQueryable();
                }
                else if (!String.IsNullOrEmpty(ad) && basDate == null && bitDate != null)
                {
                    list = db.View_Pacs.Where(k => k.t_createdate <= bitDate || k.t_adi.Contains(ad) || k.t_soyadi.Contains(ad)).AsQueryable();
                }
            }
            else
            {
                list = db.View_Pacs.Where(k => k.t_tc == tc).AsQueryable();
            }
            IEnumerable<View_Pacs> list2 = list.Where(p => p.HastaAktif == true && p.PacsAktif == true).GroupBy(d => d.t_tc).Select(g => g.FirstOrDefault());

            return list2.ToList();
        }

    }
}