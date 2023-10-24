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
    public class KHOHANGsController : Controller
    {
        private CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();

        // GET: Admin/KHOHANGs
        public async Task<ActionResult> Index()
        {
            return View(await db.KHOHANGs.ToListAsync());
        }

        // GET: Admin/KHOHANGs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOHANG kHOHANG = await db.KHOHANGs.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "MaKho,TenKho,DiaChi,TonKho")] KHOHANG kHOHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHOHANGs.Add(kHOHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(kHOHANG);
        }

        // GET: Admin/KHOHANGs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOHANG kHOHANG = await db.KHOHANGs.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "MaKho,TenKho,DiaChi,TonKho")] KHOHANG kHOHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHOHANG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kHOHANG);
        }

        // GET: Admin/KHOHANGs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOHANG kHOHANG = await db.KHOHANGs.FindAsync(id);
            if (kHOHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHOHANG);
        }

        // POST: Admin/KHOHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            KHOHANG kHOHANG = await db.KHOHANGs.FindAsync(id);
            db.KHOHANGs.Remove(kHOHANG);
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
