using CongTyVanChuyen.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongTyVanChuyen.Controllers
{
    public class QuanLyDVCController : Controller
    {
        // GET: QuanLyDVC
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        public ActionResult Index(int? TrangThai, int? page)
        {
            ViewBag.TrangThai = new SelectList(db.TRANGTHAIDVCs, "id", "TrangThai", TrangThai);

            var listDVC = db.DONVANCHUYENs.Where(dvc => dvc.TrangThaiVanChuyen == 1).ToList();
            if (TrangThai != null)
            {
                listDVC = db.DONVANCHUYENs.Where(dvc => dvc.TrangThaiVanChuyen == TrangThai).ToList();
                ViewBag.currentStatus = TrangThai;
            }
                


            int pageSize = 10;
            int pageNum = (page ?? 1);
            
            return View(listDVC.ToPagedList(pageNum, pageSize));
        }
        //public ActionResult Index(int? TrangThai, int? page)
        //{
        //    ViewBag.TrangThai = new SelectList(db.TRANGTHAIDVCs, "id", "TrangThai");

        //    var listDVC = db.DONVANCHUYENs.Where(dvc => dvc.TrangThaiVanChuyen == 1).ToList(); 
        //    if (TrangThai != null)
        //        listDVC = db.DONVANCHUYENs.Where(dvc => dvc.TrangThaiVanChuyen == TrangThai).ToList();
        //    int pageSize = 1;
        //    int pageNum = (page ?? 1);
        //    return View(listDVC.ToPagedList(pageNum, pageSize));
        //}
        public ActionResult Details(string id)
        {
            var hanghoa = db.HANGHOAs.Where(hh => hh.MaDVC.Equals(id)).ToList();
            ViewBag.hanghoa = hanghoa;
            return View(db.DONVANCHUYENs.FirstOrDefault(dvc => dvc.MaDVC.Equals(id)));
        }
        [HttpGet]
        public ActionResult Assign(string id)
        {
            ViewBag.Shipper = new SelectList(db.SHIPPERs, "MaShipper", "TenShipper");
            var hanghoa = db.HANGHOAs.Where(hh => hh.MaDVC.Equals(id)).ToList();
            ViewBag.hanghoa = hanghoa;
            return View(db.DONVANCHUYENs.FirstOrDefault(dvc => dvc.MaDVC.Equals(id)));
        }
        [HttpPost]
        public ActionResult Assign(int Shipper, string maDVC)
        {
            try
            {
                var dvc = db.DONVANCHUYENs.FirstOrDefault(p => p.MaDVC.Equals(maDVC));
                dvc.TrangThaiVanChuyen = 3;
                VANCHUYEN vc = new VANCHUYEN();
                vc.MaDVC = maDVC;
                vc.MaShipper = Shipper;
                vc.NgayGio = DateTime.Now;
                vc.MaLoaiVC = 1;
                db.VANCHUYENs.Add(vc);
                db.SaveChanges();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}