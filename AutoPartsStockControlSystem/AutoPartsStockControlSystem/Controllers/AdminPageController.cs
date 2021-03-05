using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoPartsStockControlSystem.Controllers
{
    public class AdminPageController : Controller
    {
        // GET: AdminDashBoard
        public ActionResult AdminDashboard()
        {

            return View();
        }

        
        public ActionResult AdminSearchStock()
        {
            return View();
        }

        public ActionResult AdminUsers()
        {
            return View();
        }

        public ActionResult AdminReports()
        {
            return View();
        }

        public ActionResult AdminSuppliers()
        {
            return View();
        }

        public ActionResult AdminSendMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmailConfig()
        {
            return View();
        }

    }
}