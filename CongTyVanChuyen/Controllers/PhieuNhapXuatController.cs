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
    public class PhieuNhapXuatController : Controller
    {
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        // GET: PhieuNhapXuat
        public ActionResult Index()
        {
            var lstPhieu = db.PHIEUNHAPXUATKHOes.ToList();
            return View(lstPhieu);
        }
      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPXUATKHO phieu = db.PHIEUNHAPXUATKHOes.Find(id);
            if (phieu == null)
            {
                return HttpNotFound();
            }
            return View(phieu);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUNHAPXUATKHO phieu = db.PHIEUNHAPXUATKHOes.Find(id);
            db.PHIEUNHAPXUATKHOes.Remove(phieu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.LOAIPHIEU = new SelectList(db.LOAIPHIEUx, "MaLoaiPhieu", "LoaiPhieu");
            return View();
        }
        [HttpPost]
        public ActionResult Create(PHIEUNHAPXUATKHO objPhieu)
        {
            db.PHIEUNHAPXUATKHOes.Add(objPhieu);
            db.SaveChanges();
            ViewBag.LOAIPHIEU = new SelectList(db.LOAIPHIEUx, "MaLoaiPhieu", "LoaiPhieu");
            return RedirectToAction("Index");
        }
    }
}
