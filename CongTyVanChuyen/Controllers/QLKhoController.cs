using CongTyVanChuyen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;
using System.Net;

namespace CongTyVanChuyen.Controllers
{
    public class QLKhoController : Controller
    {
        CongTyVanChuyenEntities database = new CongTyVanChuyenEntities();
        // GET: QLKho
        public ActionResult Index(int? page, string searchString)
        {
            var dsInfoWeb = database.THONGTINWEBSITEs.ToList();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                dsInfoWeb = database.THONGTINWEBSITEs.Where(c => c.LoaiThongTin.Contains(searchString)).ToList();
            }
            else
            {
                dsInfoWeb = database.THONGTINWEBSITEs.ToList();
            }
            int pageSize = 5;
            int pageNum = (page ?? 1);
            //return View(dsInfoWeb.ToPagedList(pageNum, pageSize));
            var pagedList = dsInfoWeb.ToPagedList(pageNum, pageSize);
            ViewData["searchString"] = searchString;

            return View(pagedList);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LOGINNV nv)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(nv.UserNV))
                    ModelState.AddModelError(string.Empty, "User name không được để trống");
                if (string.IsNullOrEmpty(nv.PassNV))
                    ModelState.AddModelError(string.Empty, "Password không được để trống");
                //
                var nvDB = database.LOGINNVs.Where(a => a.UserNV == nv.UserNV && a.PassNV == nv.PassNV).FirstOrDefault();
                if (nvDB == null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                else
                {
                    Session["Nhanvien"] = nvDB.UserNV;
                    Session["TenNV"] = nvDB.HoTen;
                    ViewBag.ThongBao = "Đăng nhập nhân viên thành công";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(THONGTINWEBSITE infoweb)
        {
            try
            {
                database.THONGTINWEBSITEs.Add(infoweb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("LỖI");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var info = database.THONGTINWEBSITEs.Where(c => c.MaThongTin == id).FirstOrDefault();
            return View(info);
        }
        [HttpPost]
        public ActionResult Edit(int id, THONGTINWEBSITE infoweb)
        {
            database.Entry(infoweb).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var infoweb = database.THONGTINWEBSITEs.Where(c => c.MaThongTin == id).FirstOrDefault();
            if (infoweb == null)
            {
                return HttpNotFound();
            }
            return View(infoweb);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var infoweb = database.THONGTINWEBSITEs.Where(c => c.MaThongTin == id).FirstOrDefault();
                database.THONGTINWEBSITEs.Remove(infoweb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được");
            }
        }
    }
}