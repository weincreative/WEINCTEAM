using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEINCDENTAL.Models
{
    public class HataMessage
    {

        public string Errormesaj { get; set; }
        public int? Durum { get; set; }
    }
}