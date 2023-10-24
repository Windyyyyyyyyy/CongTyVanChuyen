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
    public class DONVANCHUYENsController : Controller
    {
        private CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();

        // GET: Admin/DONVANCHUYENs
        public async Task<ActionResult> Index()
        {
            var dONVANCHUYENs = db.DONVANCHUYENs.Include(d => d.CHUCUAHANG).Include(d => d.TRANGTHAIDVC);
            return View(await dONVANCHUYENs.ToListAsync());
        }

        // GET: Admin/DONVANCHUYENs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONVANCHUYEN dONVANCHUYEN = await db.DONVANCHUYENs.FindAsync(id);
            if (dONVANCHUYEN == null)
            {
                return HttpNotFound();
            }
            return View(dONVANCHUYEN);
        }

        // GET: Admin/DONVANCHUYENs/Create
        public ActionResult Create()
        {
            ViewBag.MaChuCuaHang = new SelectList(db.CHUCUAHANGs, "MaCCH", "TenChuCuaHang");
            ViewBag.TrangThaiVanChuyen = new SelectList(db.TRANGTHAIDVCs, "id", "TrangThai");
            return View();
        }

        // POST: Admin/DONVANCHUYENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaDVC,MaChuCuaHang,TrangThaiVanChuyen,TenNguoiNhan,DiaChi,ThanhPho,Quan,Phuong,SDT,GhiChu,TienThuHo,TongTienCuoc")] DONVANCHUYEN dONVANCHUYEN)
        {
            if (ModelState.IsValid)
            {
                db.DONVANCHUYENs.Add(dONVANCHUYEN);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaChuCuaHang = new SelectList(db.CHUCUAHANGs, "MaCCH", "TenChuCuaHang", dONVANCHUYEN.MaChuCuaHang);
            ViewBag.TrangThaiVanChuyen = new SelectList(db.TRANGTHAIDVCs, "id", "TrangThai", dONVANCHUYEN.TrangThaiVanChuyen);
            return View(dONVANCHUYEN);
        }

        // GET: Admin/DONVANCHUYENs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONVANCHUYEN dONVANCHUYEN = await db.DONVANCHUYENs.FindAsync(id);
            if (dONVANCHUYEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuCuaHang = new SelectList(db.CHUCUAHANGs, "MaCCH", "TenChuCuaHang", dONVANCHUYEN.MaChuCuaHang);
            ViewBag.TrangThaiVanChuyen = new SelectList(db.TRANGTHAIDVCs, "id", "TrangThai", dONVANCHUYEN.TrangThaiVanChuyen);
            return View(dONVANCHUYEN);
        }

        // POST: Admin/DONVANCHUYENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaDVC,MaChuCuaHang,TrangThaiVanChuyen,TenNguoiNhan,DiaChi,ThanhPho,Quan,Phuong,SDT,GhiChu,TienThuHo,TongTienCuoc")] DONVANCHUYEN dONVANCHUYEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONVANCHUYEN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaChuCuaHang = new SelectList(db.CHUCUAHANGs, "MaCCH", "TenChuCuaHang", dONVANCHUYEN.MaChuCuaHang);
            ViewBag.TrangThaiVanChuyen = new SelectList(db.TRANGTHAIDVCs, "id", "TrangThai", dONVANCHUYEN.TrangThaiVanChuyen);
            return View(dONVANCHUYEN);
        }

        // GET: Admin/DONVANCHUYENs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONVANCHUYEN dONVANCHUYEN = await db.DONVANCHUYENs.FindAsync(id);
            if (dONVANCHUYEN == null)
            {
                return HttpNotFound();
            }
            return View(dONVANCHUYEN);
        }

        // POST: Admin/DONVANCHUYENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            DONVANCHUYEN dONVANCHUYEN = await db.DONVANCHUYENs.FindAsync(id);
            db.DONVANCHUYENs.Remove(dONVANCHUYEN);
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
