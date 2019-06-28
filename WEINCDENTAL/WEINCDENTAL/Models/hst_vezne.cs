//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace WEINCDENTAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class hst_vezne
    {
        public int t_id { get; set; }
        [Required]
        public Nullable<int> t_hareketid { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "Yanl�� de�er girdiniz...")]
        public decimal t_odenen { get; set; }
        [DataType(DataType.Currency)]
        public decimal t_kalan { get; set; }
        [Required]
        public int t_odemetipi { get; set; }
        public string t_createuser { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime t_odemetarih { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime t_createdate { get; set; }
        public bool t_aktif { get; set; }
    
        public virtual hst_his_hareket hst_his_hareket { get; set; }
        public virtual hst_odemetip hst_odemetip { get; set; }
    }
}
