//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEINCDENTAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class hst_hastadurum
    {
        public int t_id { get; set; }
        public string t_tc { get; set; }
        public int t_hdurumid { get; set; }
        [StringLength(100, ErrorMessage = "100 karakter s�n�r�n� a�t�n�z.")]
        [DataType(DataType.MultilineText)]
        public string t_aciklama { get; set; }
        public bool t_aktif { get; set; }

        public virtual hst_hastakarti hst_hastakarti { get; set; }
        public virtual hst_hastalik hst_hastalik { get; set; }
    }
}
