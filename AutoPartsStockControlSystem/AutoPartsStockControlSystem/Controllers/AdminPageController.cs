using AutoPartsStockControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
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


       


        public ActionResult AdminSendMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmailConfig()
        {
            return View();
        }



        #region UserStockOut


        public ActionResult AdminStockOut()
        {
            return View();
        }



        public ActionResult GetStockOutData()
        {
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<StockOut> StockOutList = db.StockOuts.ToList<StockOut>();
                return Json(new { data = StockOutList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddStockOut(int id = 0)
        {
            if (id == 0)
                return View(new StockOut());
            else
            {
                using (EntitiesAPSCS db = new EntitiesAPSCS())
                {
                    return View(db.StockOuts.Where(x => x.StockOutID == id).FirstOrDefault<StockOut>());

                }
            }
        }



        [HttpPost]
        public ActionResult AddStockOut(StockOut stkout)
        {

            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {

                var PartIDmatch = db.Items.Any(x => x.ItemPart.Equals(stkout.StockOutPart));
                var GetItem = db.Items.FirstOrDefault(x => x.ItemPart == stkout.StockOutPart);


                if (stkout.StockOutID == 0)
                {
                    if (!PartIDmatch)
                    {

                        return Json(new { success = false, message = "Item Part Number Not Found!" }, JsonRequestBehavior.AllowGet);

                    }

                    //else if (GetItem.ItemQuantity <= 10)
                    //{


                    //}

                    else if (stkout.StockOutQuantity > GetItem.ItemQuantity)
                    {


                        return Json(new { success = false, message = "Not Enough Stock Quantity!" }, JsonRequestBehavior.AllowGet);


                    }

                    else if (PartIDmatch == true && stkout.StockOutQuantity != null)
                    {

                        stkout.StockOutDescription = GetItem.ItemDescription;

                        GetItem.ItemQuantity = GetItem.ItemQuantity - stkout.StockOutQuantity.Value;
                        db.StockOuts.Add(stkout);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    }

                }

            }
            return View();

        }


        [HttpGet]
        public ActionResult EditStockOut(int id = 0)
        {
            if (id == 0)
                return View(new StockOut());
            else
            {
                using (EntitiesAPSCS db = new EntitiesAPSCS())
                {
                    return View(db.StockOuts.Where(x => x.StockOutID == id).FirstOrDefault<StockOut>());

                }
            }
        }



        [HttpPost]
        public ActionResult EditStockOut(StockOut stkout)
        {

            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                var stkoutID = db.StockOuts.Find(stkout.StockOutID);

                var PartIDmatch = db.Items.Any(x => x.ItemPart.Equals(stkout.StockOutPart));
                var GetItem = db.Items.FirstOrDefault(x => x.ItemPart == stkout.StockOutPart);
                var GetSqty = db.StockOuts.FirstOrDefault(x => x.StockOutPart == stkout.StockOutPart);


                if (!db.Items.Any(x => x.ItemPart == stkout.StockOutPart))
                {

                    return Json(new { success = false, message = "Item Part Number Not Found!" }, JsonRequestBehavior.AllowGet);

                }

                //else if (GetItem.ItemQuantity <= 10)
                //{


                //}

                else if (stkout.StockOutQuantity > GetItem.ItemQuantity)
                {

                    return Json(new { success = false, message = "Not Enough Stock Quantity!" }, JsonRequestBehavior.AllowGet);

                }

                else if (PartIDmatch == true && stkout.StockOutQuantity != null)
                {

                    GetItem.ItemQuantity = (GetItem.ItemQuantity + GetSqty.StockOutQuantity.Value) - stkout.StockOutQuantity.Value;

                    db.Entry(stkoutID).CurrentValues.SetValues(stkout);
                    db.Entry(stkoutID).State = EntityState.Modified;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Record & Stock Quantity Updated Successfully!" }, JsonRequestBehavior.AllowGet);
                }

                return View();

            }
        }


        [HttpPost]
        public ActionResult DeleteStockOut(int id, StockOut stkout)
        {

            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                stkout = db.StockOuts.Where(x => x.StockOutID == id).FirstOrDefault<StockOut>();

                var GetItem = db.Items.FirstOrDefault(x => x.ItemPart == stkout.StockOutPart);
                var GetSqty = db.StockOuts.FirstOrDefault(x => x.StockOutPart == stkout.StockOutPart);

                GetItem.ItemQuantity = GetItem.ItemQuantity + GetSqty.StockOutQuantity.Value;
                db.StockOuts.Remove(stkout);
                db.SaveChanges();
                return Json(new { success = true, message = "Record Deleted Successfully. Stock Quantity Updated!" }, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion




        #region ITEMS
        public ActionResult AdminSearchStock()
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
        public ActionResult AddUsers(int id = 0)
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
        public ActionResult AddUsers(User usr)
        {
          
            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {
                if (usr.UserID == 0)
                {
                    if (db.Users.Any(x => x.IDCard == usr.IDCard))
                    {

                        return Json(new { success = false,  message = "IDCard Number Already Exist!"}, JsonRequestBehavior.AllowGet);
                       
                    }

                    else
                    {

                        db.Users.Add(usr);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {

                        db.Entry(usr).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet); 
                }
            }


        }


        [HttpGet]
        public ActionResult EditUsers(int id = 0)
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
        public ActionResult EditUsers(User usr)
        {

            using (EntitiesAPSCS db = new EntitiesAPSCS())
            {

                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                
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