using CongTyVanChuyen.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongTyVanChuyen.Controllers
{
    public class DonVanChuyenController : Controller
    {
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        // GET: DonVanChuyen
        public ActionResult Index(string sortDVC, string searchString, string currentFilter)
        {
            var lstDon = db.DONVANCHUYENs.ToList();

            ViewBag.currentFilter = searchString;
            if(!string.IsNullOrEmpty(searchString))
            {
                lstDon = db.DONVANCHUYENs.Where(n => n.MaDVC.Contains(searchString)).ToList();
            }
            else
            {
                lstDon = db.DONVANCHUYENs.ToList();
            }

            return View(lstDon);
        }
        [HttpGet]
        public ActionResult Detail(string id)
        {
            var objDon = db.DONVANCHUYENs.Where(n => n.MaDVC == id).FirstOrDefault();

            return View(objDon);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        { 
            var sp = db.DONVANCHUYENs.Where(s => s.MaDVC == id).FirstOrDefault(); 
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(DONVANCHUYEN donvc)
        {
            if (ModelState.IsValid)
            {
                var donDB = db.DONVANCHUYENs.FirstOrDefault(p => p.MaDVC == donvc.MaDVC);
                if (donDB != null)
                {
                    donDB.MaDVC = donvc.MaDVC;
                    donDB.MaChuCuaHang = donvc.MaChuCuaHang;
                    donDB.TrangThaiVanChuyen = donvc.TrangThaiVanChuyen;
                    donDB.TenNguoiNhan = donvc.TenNguoiNhan;
                    donDB.DiaChi = donvc.DiaChi;
                    donDB.ThanhPho = donvc.ThanhPho;
                    donDB.Quan = donvc.Quan;
                    donDB.Phuong = donvc.Phuong;
                    donDB.SDT = donvc.SDT;
                    donDB.GhiChu = donvc.GhiChu;
                    donDB.TienThuHo= donvc.TienThuHo;
                    donDB.TongTienCuoc = donvc.TongTienCuoc;
                     
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            return View(donvc);

        }
    }
}