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
    public class CHUCUAHANGsController : Controller
    {
        private CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();

        // GET: Admin/CHUCUAHANGs
        public async Task<ActionResult> Index()
        {
            return View(await db.CHUCUAHANGs.ToListAsync());
        }

        // GET: Admin/CHUCUAHANGs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCUAHANG cHUCUAHANG = await db.CHUCUAHANGs.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "MaCCH,TenChuCuaHang,SDT,CCCD,NgaySinh,DiaChi,TinhTP,QuanHuyen,PhuongXa,MatKhau")] CHUCUAHANG cHUCUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.CHUCUAHANGs.Add(cHUCUAHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cHUCUAHANG);
        }

        // GET: Admin/CHUCUAHANGs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCUAHANG cHUCUAHANG = await db.CHUCUAHANGs.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "MaCCH,TenChuCuaHang,SDT,CCCD,NgaySinh,DiaChi,TinhTP,QuanHuyen,PhuongXa,MatKhau")] CHUCUAHANG cHUCUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHUCUAHANG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cHUCUAHANG);
        }

        // GET: Admin/CHUCUAHANGs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCUAHANG cHUCUAHANG = await db.CHUCUAHANGs.FindAsync(id);
            if (cHUCUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHUCUAHANG);
        }

        // POST: Admin/CHUCUAHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CHUCUAHANG cHUCUAHANG = await db.CHUCUAHANGs.FindAsync(id);
            db.CHUCUAHANGs.Remove(cHUCUAHANG);
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
