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
    
    public partial class adm_YetkiGroups
    {
        public int Id { get; set; }
        public long YetkiId { get; set; }
        public int GroupId { get; set; }
        public bool Aktif { get; set; }
    
        public virtual adm_kullanicigrup adm_kullanicigrup { get; set; }
        public virtual adm_Yetki adm_Yetki { get; set; }
    }
}