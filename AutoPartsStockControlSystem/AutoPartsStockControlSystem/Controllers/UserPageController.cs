﻿using System;
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

        public ActionResult UserSales()
        {
            return View();
        }


        public ActionResult UserSendMessage()
        {
            return View();
        }

        public ActionResult SendEmailConfig()
        {
            return View();
        }


        #region Update Stock
        
        public ActionResult UserUpdateStock()
        {

                return View();
        }


        [HttpPost]
        public ActionResult UserUpdateStock(Item itm)
        {
           
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {

                var PartIDmatch = db.Items.Any(x => x.ItemPart.Equals(itm.ItemPart));

                if (PartIDmatch == true && itm.ItemQuantity != null)

                {
                    var abc = db.Items.FirstOrDefault(x => x.ItemPart == itm.ItemPart);

                    abc.ItemQuantity = itm.ItemQuantity.Value + abc.ItemQuantity;

                    db.SaveChanges();

                    ViewBag.Message = String.Format("Part Number Quantity Updated Successfully!");
                    

                }

            else
            {
                    ViewBag.Message = String.Format("Please Enter Quantity And A Valid Part Number");

            }
            return View();
        }
    }


        public ActionResult GetDataLowStockTable()
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<Item> ItmList = db.Items.Where(x => x.ItemQuantity <= 10).ToList<Item>();
                return Json(new { data = ItmList }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion


        #region ITEMS
        public ActionResult UserAddNewItem()
        {
            return View();
        }

        public ActionResult GetDataItem()
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<Item> ItmList = db.Items.ToList<Item>();
                return Json(new { data = ItmList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddItem(int id = 0)
        {
            if (id == 0)
                return View(new Item());
            else
            {
                using (EntitiesAPSCS db = new EntitiesAPSCS())
                {
                    return View(db.Items.Where(x => x.ItemID == id).FirstOrDefault<Item>());

                }
            }
        }



        [HttpPost]
        public ActionResult AddItem(Item itm)
        {

            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                if (itm.ItemID == 0)
                {
                    if (db.Items.Any(x => x.ItemPart == itm.ItemPart))
                    {

                        return Json(new { success = false, message = "Item Part Number Already Exist!" }, JsonRequestBehavior.AllowGet);

                    }

                    else
                    {

                        db.Items.Add(itm);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {

                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }


        [HttpGet]
        public ActionResult EditItem(int id = 0)
        {
            if (id == 0)
                return View(new Item());
            else
            {
                using (EntitiesAPSCS db = new EntitiesAPSCS())
                {
                    return View(db.Items.Where(x => x.ItemID == id).FirstOrDefault<Item>());

                }
            }
        }



        [HttpPost]
        public ActionResult EditItem(Item itm)
        {

            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {

                db.Entry(itm).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

            }


        }


        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                Item itm = db.Items.Where(x => x.ItemID == id).FirstOrDefault<Item>();
                db.Items.Remove(itm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }





        #endregion

        #region UserSuppliers
        public ActionResult UserSuppliers()
        {
            return View();
        }


        public ActionResult GetData()
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<Supplier> empList = db.Suppliers.ToList<Supplier>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEditSupplier(int id = 0)
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
        public ActionResult AddOrEditSupplier(Supplier emp)
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
        public ActionResult Delete(int id)
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