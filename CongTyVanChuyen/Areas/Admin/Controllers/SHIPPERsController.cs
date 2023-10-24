using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CongTyVanChuyen.Models;

namespace CongTyVanChuyen.Areas.Admin.Controllers
{
    public class SHIPPERsController : Controller
    {
        private CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();

        // GET: Admin/SHIPPERs
        public async Task<ActionResult> Index()
        {
            return View(await db.SHIPPERs.ToListAsync());
        }

        // GET: Admin/SHIPPERs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHIPPER sHIPPER = await db.SHIPPERs.FindAsync(id);
            if (sHIPPER == null)
            {
                return HttpNotFound();
            }
            return View(sHIPPER);
        }

        // GET: Admin/SHIPPERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SHIPPERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaShipper,TenShipper,NgaySinh,SDT,CCCD,DiaChi,MatKhau")] SHIPPER sHIPPER)
        {
            if (ModelState.IsValid)
            {
                db.SHIPPERs.Add(sHIPPER);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sHIPPER);
        }

        // GET: Admin/SHIPPERs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHIPPER sHIPPER = await db.SHIPPERs.FindAsync(id);
            if (sHIPPER == null)
            {
                return HttpNotFound();
            }
            return View(sHIPPER);
        }

        // POST: Admin/SHIPPERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaShipper,TenShipper,NgaySinh,SDT,CCCD,DiaChi,MatKhau")] SHIPPER sHIPPER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sHIPPER).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sHIPPER);
        }

        // GET: Admin/SHIPPERs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHIPPER sHIPPER = await db.SHIPPERs.FindAsync(id);
            if (sHIPPER == null)
            {
                return HttpNotFound();
            }
            return View(sHIPPER);
        }

        // POST: Admin/SHIPPERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SHIPPER sHIPPER = await db.SHIPPERs.FindAsync(id);
            db.SHIPPERs.Remove(sHIPPER);
            await db.SaveChangesAsync();
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
