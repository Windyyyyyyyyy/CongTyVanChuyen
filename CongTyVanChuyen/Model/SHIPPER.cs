//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongTyVanChuyen.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SHIPPER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SHIPPER()
        {
            this.PHIEUNHAPXUATKHOes = new HashSet<PHIEUNHAPXUATKHO>();
            this.VANCHUYENs = new HashSet<VANCHUYEN>();
        }
    
        public int MaShipper { get; set; }
        public string TenShipper { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string DiaChi { get; set; }
        public string MatKhau { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPXUATKHO> PHIEUNHAPXUATKHOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VANCHUYEN> VANCHUYENs { get; set; }
    }
}
