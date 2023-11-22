using CongTyVanChuyen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace CongTyVanChuyen.Controllers
{
    public class QuanLyDonHangShipperController : Controller
    {
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(int id)
        {
            var shipper = db.SHIPPERs.FirstOrDefault(a => a.MaShipper == id);
            Session["shipper"] = shipper;
            return Redirect("DanhSachDonVanChuyen");
        }
        public ActionResult DanhSachDonVanChuyen(int? sort)
        {
            SHIPPER shipper = Session["shipper"] as SHIPPER;
            var list = db.VANCHUYENs.Where(a => a.MaShipper == shipper.MaShipper && a.chuyenShipper == null && a.Nhan == null && a.TuChoi == null).ToList();
            switch (sort)
            {
                case 1:
                    list = db.VANCHUYENs
                            .Where(v => v.MaShipper == shipper.MaShipper && v.Nhan == true && v.isDone == null)
                            .ToList();
                    break;
                case 2:
                    list = db.VANCHUYENs
                            .Where(v => v.MaShipper == shipper.MaShipper && v.isDone == true && v.Nhan == true)
                            .ToList();
                    break;
                case 3:
                    list = db.VANCHUYENs
                            .Where(v => v.MaShipper == shipper.MaShipper && v.chuyenShipper != null)
                            .ToList();
                    break;
               
            }

            return View(list);
        }
        public ActionResult Details(int id)
        {
            SHIPPER shipper = Session["shipper"] as SHIPPER;
            ViewBag.shipper = shipper;
            var dvC = db.VANCHUYENs.FirstOrDefault(a => a.id == id/*a => a.DONVANCHUYEN.MaDVC == id && (a.MaShipper == shipper.MaShipper || a.chuyenShipper == shipper.MaShipper)*/);

            return View(dvC);
        }
        public ActionResult TuChoi(int id)
        {
            SHIPPER shipper = Session["shipper"] as SHIPPER;
            var vanchuyen = db.VANCHUYENs.FirstOrDefault(a => a.id == id);
            var donVanChuyen = db.DONVANCHUYENs.FirstOrDefault(a => a.MaDVC == vanchuyen.MaDVC);
            var shipper_db = db.SHIPPERs.FirstOrDefault(a => a.MaShipper == shipper.MaShipper);
            vanchuyen.TuChoi = true;
            db.Entry(vanchuyen).State = EntityState.Modified;
            donVanChuyen.TrangThaiVanChuyen = 13;
            db.Entry(donVanChuyen).State = EntityState.Modified;
            if (shipper_db.soLanCancel++ > 5)
            {
                shipper_db.soLanCancel = 0;
                if (shipper_db.diemTichLuy < 5)
                {
                    shipper_db.diemTichLuy = 0;
                }
                else
                {
                    shipper_db.diemTichLuy -= 6;
                }
            }
            else
            {
                shipper_db.soLanCancel++;
            }


            db.Entry(shipper_db).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachDonVanChuyen");
        }
        public ActionResult ChapNhan(int id)
        {
            SHIPPER shipper = Session["shipper"] as SHIPPER;
            var vanchuyen = db.VANCHUYENs.FirstOrDefault(a => a.id == id);
            var donVanChuyen = db.DONVANCHUYENs.FirstOrDefault(a => a.MaDVC == vanchuyen.MaDVC);

            if (vanchuyen.chuyenShipper != null)
            {
                vanchuyen.Nhan = true;
                vanchuyen.TuChoi = false;
                vanchuyen.MaShipper = shipper.MaShipper;
                vanchuyen.chuyenShipper = null;
                db.Entry(vanchuyen).State = EntityState.Modified;
            }
            else
            {
                vanchuyen.Nhan = true;
                vanchuyen.TuChoi = false;
                db.Entry(vanchuyen).State = EntityState.Modified;
            }


            donVanChuyen.TrangThaiVanChuyen = 14;
            db.Entry(donVanChuyen).State = EntityState.Modified;


            db.SaveChanges();
            return RedirectToAction("DanhSachDonVanChuyen");
        }
        [HttpGet]
        public ActionResult ChuyenTiep(int id)
        {
            var vanchuyen = db.VANCHUYENs.FirstOrDefault(a => a.id == id);
            ViewBag.Shipper = new SelectList(db.SHIPPERs, "MaShipper", "TenShipper");
            return View(vanchuyen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChuyenTiep([Bind(Include = "id, MaDVC, MaShipper, MaLoaiVC, NgayGio, hinhAnh, TuChoi, Nhan, chuyenShipper, isDone")] VANCHUYEN vc, int Shipper)
        {
            vc.chuyenShipper = vc.MaShipper;
            vc.MaShipper = Shipper;
            db.Entry(vc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachDonVanChuyen");
        }
        public ActionResult TuChoiChuyenTiep(int id)
        {
            var vanchuyen = db.VANCHUYENs.FirstOrDefault(a => a.id == id);
            vanchuyen.MaShipper = vanchuyen.chuyenShipper;
            vanchuyen.chuyenShipper = null;
            db.Entry(vanchuyen).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachDonVanChuyen");
        }

        [HttpGet]
        public ActionResult GiaoHang(int id)
        {
            SHIPPER shipper = Session["shipper"] as SHIPPER;
            ViewBag.shipper = shipper;
            var dvC = db.VANCHUYENs.FirstOrDefault(a => a.id == id/*a => a.DONVANCHUYEN.MaDVC == id && (a.MaShipper == shipper.MaShipper || a.chuyenShipper == shipper.MaShipper)*/);

            return View(dvC);
        }
        [HttpPost]
        public ActionResult CapNhatGiaoHang([Bind(Include = "id, MaDVC, MaShipper, MaLoaiVC, NgayGio, hinhAnh, TuChoi, Nhan, chuyenShipper, isDone, ghiChu")] VANCHUYEN vc
            , HttpPostedFileBase hinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (hinhAnh != null)
                {
                    //Lấy tên file của hình được up lên

                    var fileName = Path.GetFileName(hinhAnh.FileName);

                    //Tạo đường dẫn tới file

                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    //Lưu tên

                    vc.hinhAnh = fileName;
                    //Save vào Images Folder
                    hinhAnh.SaveAs(path);

                }
            }
            vc.isDone = true;
            db.Entry(vc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachDonVanChuyen");
        }
        [HttpGet]
        public ActionResult DonThanhCong(int id)
        {
            SHIPPER shipper = Session["shipper"] as SHIPPER;
            ViewBag.shipper = shipper;
            var dvC = db.VANCHUYENs.FirstOrDefault(a => a.id == id/*a => a.DONVANCHUYEN.MaDVC == id && (a.MaShipper == shipper.MaShipper || a.chuyenShipper == shipper.MaShipper)*/);

            return View(dvC);
        }
    }
}