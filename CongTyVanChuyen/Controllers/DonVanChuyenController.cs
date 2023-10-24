using CongTyVanChuyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongTyVanChuyen.Controllers
{
    public class DonVanChuyenController : Controller
    {
        // GET: DonVanChuyen

        public ActionResult TaoDonVanChuyen()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddDonVanChuyen(DONVANCHUYEN dvc)
        {
            return Json("add success");
        }
    }
}