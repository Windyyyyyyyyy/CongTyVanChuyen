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
    using System.Linq;

    public partial class VANCHUYEN
    {
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        public int id { get; set; }
        public string MaDVC { get; set; }
        public Nullable<int> MaShipper { get; set; }
        public Nullable<int> MaLoaiVC { get; set; }
        public Nullable<System.DateTime> NgayGio { get; set; }
        public string hinhAnh { get; set; }
        public Nullable<bool> TuChoi { get; set; }
        public Nullable<bool> Nhan { get; set; }
        public Nullable<int> chuyenShipper { get; set; }
        public Nullable<bool> isDone { get; set; }
        public string ghiChu { get; set; }
    
        public virtual DONVANCHUYEN DONVANCHUYEN { get; set; }
        public virtual LOAIVANCHUYEN LOAIVANCHUYEN { get; set; }
        public virtual SHIPPER SHIPPER { get; set; }
        public string getTenChuyenShipper(int? id)
        {
            var shipper = db.SHIPPERs.FirstOrDefault(a => a.MaShipper == id);
            return shipper.TenShipper;
        }
    }
}
