using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoPartsStockControlSystem.Controllers
{
    public class UserPageController : Controller
    {
        // GET: UserPage
        public ActionResult UserDashboard()
        {
            return View();
        }

        public ActionResult UserSearchStock()
        {
            return View();
        }

        public ActionResult UserAddNewItem()
        {
            return View();
        }

        public ActionResult UserUpdateStock()
        {
            return View();
        }

        public ActionResult UserSales()
        {
            return View();
        }

       
        public ActionResult UserSuppliers()
        {
            return View();
        }

        public ActionResult UserSendMessage()
        {
            return View();
        }
    }
}