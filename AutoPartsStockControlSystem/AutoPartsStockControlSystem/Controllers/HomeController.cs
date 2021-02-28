
using System.Linq;

using System.Web.Mvc;

using AutoPartsStockControlSystem.Models;


using System;

using System.Data;
using System.Net.Mail;
using System.Net;



namespace AutoPartsStockControlSystem.Controllers
{
    
    public class HomeController : Controller
    {
        //////////////////////////////////////////////////// Declare Data Entitry with Database
        DataEntity1 db = new DataEntity1();

        /////////////////////////////////////////////////////Login Function

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            var checkLoginAdmin = db.Users.Where(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password) && x.UserType.Equals("Admin")).FirstOrDefault();

            var checkLoginUser = db.Users.Where(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password) && x.UserType.Equals("User")).FirstOrDefault();


            if (checkLoginAdmin != null)
            {
                Session["Email"] = model.Email.ToString();
                Session["Password"] = model.Password.ToString();
                return RedirectToAction("AdminDashboard", "AdminPage");
            }


            else if (checkLoginUser != null)
            {
                Session["Email"] = model.Email.ToString();
                Session["Password"] = model.Password.ToString();
                return RedirectToAction("UserDashboard", "UserPage");

            }

            else
            {

                ViewBag.Notification = "Wrong E-Mail or Password";
                ViewBag.EmailNotification = "Please Enter Email Address";
                ViewBag.PasswordNotification = "Please Enter Password ";

            }
            return View();
        }

        ////////////////////////////////////////////////////////////////////Logout Function

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }


        ///////////////////////////////////////////////////////////// Forgot Password Function

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "/Home/ResetPassword/" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            using (var context = new DataEntity1())
            {
                var getUser = (from s in context.Users where s.Email == EmailID select s).FirstOrDefault();
                if (getUser != null)
                {
                    getUser.ResetPasswordCode = resetCode;


                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Password Reset Request";
                    var body = "Hi " + getUser.Name + ", <br/><br/> Below please find the Password Reset Link requested from Auto Parts Stock Control System. Please click on the link to reset your password. " +

                         " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                         "If you no longor need to reset your password, Please ignore this message.<br/><br/> Thank you<br/><br/> Auto Parts Stock Control System";

                    SendEmail(getUser.Email, body, subject);

                    ViewBag.Message1 = "Reset Link was sent to your Email Address.";
                }
                else
                {
                    ViewBag.Message = "Email Address does not exist!. Please check your input!";
                    return View();
                }
            }

            return View();
        }

        ///////////////////////////////////////////////////////////Send Forgot Password Email

        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("renald92@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("renald92@gmail.com", "Ilovecars908908");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

        ////////////////////////////////////////////////////////Reset Password after link
        
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (var context = new DataEntity1())
            {
                var user = context.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (var context = new DataEntity1())
                {
                    var user = context.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        //you can encrypt password here, we are not doing it
                        user.Password = model.NewPassword;
                        //make resetpasswordcode empty string now
                        user.ResetPasswordCode = "";
                        //to avoid validation issues, disable it
                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "New password was successfully updated";
                    }
                }
            }
            else
            {
                message = "Error Updating Password!";
            }
            ViewBag.Message = message;
            return View(model);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}  

       