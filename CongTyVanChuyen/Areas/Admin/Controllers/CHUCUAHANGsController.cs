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
    public class CHUCUAHANGsController : Controller
    {
        private CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();

        // GET: Admin/CHUCUAHANGs
        public ActionResult Index()
        {
            return View(db.CHUCUAHANGs.ToList());
        }

        // GET: Admin/CHUCUAHANGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCUAHANG cHUCUAHANG = db.CHUCUAHANGs.Find(id);
            if (cHUCUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHUCUAHANG);
        }

        // GET: Admin/CHUCUAHANGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CHUCUAHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCCH,TenChuCuaHang,SDT,CCCD,NgaySinh,DiaChi,TinhTP,QuanHuyen,PhuongXa,MatKhau")] CHUCUAHANG cHUCUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.CHUCUAHANGs.Add(cHUCUAHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cHUCUAHANG);
        }

        // GET: Admin/CHUCUAHANGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCUAHANG cHUCUAHANG = db.CHUCUAHANGs.Find(id);
            if (cHUCUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHUCUAHANG);
        }

        // POST: Admin/CHUCUAHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCCH,TenChuCuaHang,SDT,CCCD,NgaySinh,DiaChi,TinhTP,QuanHuyen,PhuongXa,MatKhau")] CHUCUAHANG cHUCUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHUCUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHUCUAHANG);
        }

        // GET: Admin/CHUCUAHANGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCUAHANG cHUCUAHANG = db.CHUCUAHANGs.Find(id);
            if (cHUCUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHUCUAHANG);
        }

        // POST: Admin/CHUCUAHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHUCUAHANG cHUCUAHANG = db.CHUCUAHANGs.Find(id);
            db.CHUCUAHANGs.Remove(cHUCUAHANG);
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
