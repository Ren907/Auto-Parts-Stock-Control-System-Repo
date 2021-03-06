﻿using AutoPartsStockControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

       

        public ActionResult AdminReports()
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

        #region Users
        public ActionResult AdminUsers()
        {
            return View();
        }

        public ActionResult GetDataUsers()
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<User> UsrList = db.Users.ToList<User>();
                return Json(new { data = UsrList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEditUsers(int id = 0)
        {
            if (id == 0)
                return View(new User());
            else
            {
                using (EntitiesAPSCS db = new EntitiesAPSCS())
                {
                    return View(db.Users.Where(x => x.UserID == id).FirstOrDefault<User>());

              }
            }
        }



        [HttpPost]
        public ActionResult AddOrEditUsers(User usr)
        {
          
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                if (usr.UserID == 0)
                {
                    db.Users.Add(usr);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult DeleteUsers(int id)
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                User usr = db.Users.Where(x => x.UserID == id).FirstOrDefault<User>();
                db.Users.Remove(usr);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion


        #region AdminSupplier


        public ActionResult AdminSuppliers()
        {
            return View();
        }


        public ActionResult GetDataSuppliers()
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<Supplier> empList = db.Suppliers.ToList<Supplier>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEditSuppliers(int id = 0)
        {
            if (id == 0)
                return View(new Supplier());
            else
            {
                using (EntitiesAPSCS db = new EntitiesAPSCS())
                {
                    return View(db.Suppliers.Where(x => x.SupplierID == id).FirstOrDefault<Supplier>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditSuppliers(Supplier emp)
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
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
        public ActionResult DeleteSuppliers(int id)
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                Supplier emp = db.Suppliers.Where(x => x.SupplierID == id).FirstOrDefault<Supplier>();
                db.Suppliers.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


    }
}