//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongTyVanChuyen.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOAIPHIEU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAIPHIEU()
        {
            this.PHIEUNHAPXUATKHOes = new HashSet<PHIEUNHAPXUATKHO>();
        }
    
        public int MaLoaiPhieu { get; set; }
        public string LoaiPhieu1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPXUATKHO> PHIEUNHAPXUATKHOes { get; set; }
    }
}
