using CongTyVanChuyen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongTyVanChuyen.Controllers
{
    public class ThongKeController : Controller
    {
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        // GET: ThongKe
        public ActionResult Index(int? thang, int? nam)
        {
            int count = 0;
            double sum = 0;

            if (thang.HasValue && nam.HasValue)
            {
                count = db.DONVANCHUYENs.Where(a => DbFunctions.TruncateTime(a.NgayTao).Value.Month == thang && DbFunctions.TruncateTime(a.NgayTao).Value.Year == nam).Count();
                sum = db.DONVANCHUYENs.Where(a => DbFunctions.TruncateTime(a.NgayTao).Value.Month == thang && DbFunctions.TruncateTime(a.NgayTao).Value.Year == nam).Sum(a => a.TongTienCuoc);
            }

            var lstDVC = db.THONGKE_HETHONG.ToList();
            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            ViewBag.SoLuongDonHang = count;
            ViewBag.TongDoanhThu = sum;
            return View(lstDVC);
        }
    }
}