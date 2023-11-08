using CongTyVanChuyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongTyVanChuyen.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Shipper
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
        public ActionResult Index()
        {
            var listVanChuyen = db.VANCHUYENs.Where(m => m.MaShipper == 1).ToList();
            return View(listVanChuyen);
        }
        public ActionResult Details(int id)
        {
            return View(db.VANCHUYENs.FirstOrDefault(m => m.id == id));
        }
    }
}