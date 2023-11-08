﻿using CongTyVanChuyen.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace CongTyVanChuyen.Controllers
{
    public class DonVanChuyenController : Controller
    {
        // GET: DonVanChuyen
        CongTyVanChuyenEntities db = new CongTyVanChuyenEntities();
      
        public JsonResult getVoucher(string voucher)
        {
            var voucherObject = db.VOUCHERs.FirstOrDefault(v => v.code == voucher && v.isActive == true && v.SoLuong > 0);
            if (voucher != null)
            {
                return Json(voucherObject, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Trả về JSON không có dữ liệu nếu không tìm thấy voucher
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TaoDonVanChuyen()
        {
            string msg = "";
            if (TempData["message"] != null)
            {
                msg = TempData["message"].ToString();
                ViewBag.Message = msg;
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult ThanhToanCuocPhi(DONVANCHUYEN dvc)
        {
            try
            {
                Session["DonVanChuyen"] = dvc;
                string url = ConfigurationManager.AppSettings["Url"];
                string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
                string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

                PayLib pay = new PayLib();
                pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
                pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
                pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
                double total = dvc.TongTienCuoc * 100;
                pay.AddRequestData("vnp_Amount", total.ToString("0")); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
                pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
                pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
                pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
                pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
                pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
                pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
                string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                        "/DonVanChuyen/PaymentConfirm?";
                pay.AddRequestData("vnp_ReturnUrl", baseURI); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
                pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

                string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

                return Redirect(paymentUrl);

            }
            catch (Exception e)
            {
                return RedirectToAction("TaoDonVanChuyen");
            }
              
        }
        public ActionResult VNPAY(double TongTienCuoc)
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();
            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            double total = TongTienCuoc * 100;
            pay.AddRequestData("vnp_Amount", total.ToString("0")); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                    "/DonVanChuyen/PaymentConfirm?";
            pay.AddRequestData("vnp_ReturnUrl", baseURI); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }
        public ActionResult PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                string orderId = Convert.ToString(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?
                string responseMessage;
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ThanhCong();
                        responseMessage = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                        TempData["message"] = responseMessage;

                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode

                        responseMessage = "Có lỗi xảy ra trong quá trình xử lý đơn hàng" + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                        TempData["message"] = responseMessage;
                        return RedirectToAction("TaoDonVanChuyen");
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            
            return RedirectToAction("TaoDonVanChuyen");
        }

        private void ThatBai()
        {
            throw new NotImplementedException();
        }

        public void ThanhCong()
        {
            try
            {
                DONVANCHUYEN dvc = Session["DonVanChuyen"] as DONVANCHUYEN;
                dvc.MaDVC = GenerateMaDVC();
                dvc.MaChuCuaHang = "01";
                dvc.TrangThaiVanChuyen = 1;
                dvc.NgayTao = DateTime.Now;
                db.DONVANCHUYENs.Add(dvc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public string GenerateMaDVC()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int maDVCLength = 20;
            var random = new Random();
            var maDVC = new string(
                Enumerable.Repeat(allowedChars, maDVCLength)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray()
            );
            if (char.IsDigit(maDVC[0]))
            {
                maDVC = (char)('A' + random.Next(26)) + maDVC.Substring(1);
            }

            return maDVC;
        }


    }
}