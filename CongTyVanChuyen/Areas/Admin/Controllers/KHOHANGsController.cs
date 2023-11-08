using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CongTyVanChuyen.Models;

namespace CongTyVanChuyen.Areas.Admin.Controllers
{
    public class KHOHANGsController : Controller
    {
        private CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();

        // GET: Admin/KHOHANGs
        public ActionResult Index()
        {
            return View(db.KHOHANGs.ToList());
        }

        // GET: Admin/KHOHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOHANG kHOHANG = db.KHOHANGs.Find(id);
            if (kHOHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHOHANG);
        }

        // GET: Admin/KHOHANGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KHOHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKho,TenKho,DiaChi,TonKho")] KHOHANG kHOHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHOHANGs.Add(kHOHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHOHANG);
        }

        // GET: Admin/KHOHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOHANG kHOHANG = db.KHOHANGs.Find(id);
            if (kHOHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHOHANG);
        }

        // POST: Admin/KHOHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKho,TenKho,DiaChi,TonKho")] KHOHANG kHOHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHOHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHOHANG);
        }

        // GET: Admin/KHOHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOHANG kHOHANG = db.KHOHANGs.Find(id);
            if (kHOHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHOHANG);
        }

        // POST: Admin/KHOHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHOHANG kHOHANG = db.KHOHANGs.Find(id);
            db.KHOHANGs.Remove(kHOHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
