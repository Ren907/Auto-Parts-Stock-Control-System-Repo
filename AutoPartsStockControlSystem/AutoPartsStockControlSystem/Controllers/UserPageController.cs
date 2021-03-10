using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoPartsStockControlSystem.Models;
using System.Data.Entity;

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


        public ActionResult GetData()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<Supplier> empList = db.Suppliers.ToList<Supplier>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Supplier());
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.Suppliers.Where(x => x.SupplierID == id).FirstOrDefault<Supplier>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Supplier emp)
        {
            using (Entities db = new Entities())
            {
                if (emp.SupplierID == 0)
                {
                    db.Suppliers.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Entities db = new Entities())
            {
                Supplier emp = db.Suppliers.Where(x => x.SupplierID == id).FirstOrDefault<Supplier>();
                db.Suppliers.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult UserSendMessage()
        {
            return View();
        }

        public ActionResult SendEmailConfig()
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

      
        


    }
}