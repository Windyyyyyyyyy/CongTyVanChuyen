﻿using System;
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
    public class VOUCHERsController : Controller
    {
        private CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();

        // GET: Admin/VOUCHERs
        public ActionResult Index()
        {
            return View(db.VOUCHERs.ToList());
        }

        // GET: Admin/VOUCHERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOUCHER vOUCHER = db.VOUCHERs.Find(id);
            if (vOUCHER == null)
            {
                return HttpNotFound();
            }
            return View(vOUCHER);
        }

        // GET: Admin/VOUCHERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VOUCHERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,code,moTa,phanTram,toiDa,SoLuong,isActive")] VOUCHER vOUCHER)
        {
            if (ModelState.IsValid)
            {
                db.VOUCHERs.Add(vOUCHER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vOUCHER);
        }

        // GET: Admin/VOUCHERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOUCHER vOUCHER = db.VOUCHERs.Find(id);
            if (vOUCHER == null)
            {
                return HttpNotFound();
            }
            return View(vOUCHER);
        }

        // POST: Admin/VOUCHERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,code,moTa,phanTram,toiDa,SoLuong,isActive")] VOUCHER vOUCHER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vOUCHER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vOUCHER);
        }

        // GET: Admin/VOUCHERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOUCHER vOUCHER = db.VOUCHERs.Find(id);
            if (vOUCHER == null)
            {
                return HttpNotFound();
            }
            return View(vOUCHER);
        }

        // POST: Admin/VOUCHERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VOUCHER vOUCHER = db.VOUCHERs.Find(id);
            db.VOUCHERs.Remove(vOUCHER);
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
