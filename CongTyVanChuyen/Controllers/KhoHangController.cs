using CongTyVanChuyen.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CongTyVanChuyen.Controllers
{
    public class KhoHangController : Controller
    {
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        // GET: KhoHang
        public ActionResult Index()
        {
            var lstKhoHang = db.KHOHANGs.ToList();
            return View(lstKhoHang);
        }
        [HttpGet]
        public ActionResult Create()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Create(KHOHANG objKhoHang)
        { 
            db.KHOHANGs.Add(objKhoHang);
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.KHOHANGs.Where(c => c.MaKho == id).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var order = db.KHOHANGs.Where(c => c.MaKho == id).FirstOrDefault();
                db.KHOHANGs.Remove(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được");
            }
        }
    }
}