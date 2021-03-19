
using System.Linq;
using System.Web.Mvc;
using System;
using System.Data;
using System.Net.Mail;
using System.Net;
using AutoPartsStockControlSystem.Models;
using System.Security.Cryptography;
using System.Text;

namespace AutoPartsStockControlSystem.Controllers
{
    
    public class HomeController : Controller
    {
        //////////////////////////////////////////////////// Declare Data Entitry with Database
        EntitiesAPSCS db = new EntitiesAPSCS();

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
           
            string HashedPassword = GenerateSHA256Hash(model.Password);

            var checkLoginAdmin = db.Users.Where(x => x.Email.Equals(model.Email) && x.Password.Equals(HashedPassword) && x.UserType.Equals("Admin")).FirstOrDefault();

            var checkLoginUser = db.Users.Where(x => x.Email.Equals(model.Email) && x.Password.Equals(HashedPassword) && x.UserType.Equals("User")).FirstOrDefault();


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
            using (var context = new EntitiesAPSCS())
            {
                var getUser = (from gu in context.Users where gu.Email == EmailID select gu).FirstOrDefault();
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

            using (var context = new EntitiesAPSCS())
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
                using (var context = new EntitiesAPSCS())
                {
                    var user = context.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        //Hash and Salt password before saving changes

                        //string Salt = CreateSalt(10);
                        string HashedPassword = GenerateSHA256Hash(model.ConfirmPassword);

               
                        user.Password = HashedPassword;
                            
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


        ////  Create salt bytes 

        //public string CreateSalt(int size)
        //{
        //    var rng = new RNGCryptoServiceProvider();
        //    var buff = new byte[size];
        //    rng.GetBytes(buff);
        //    return Convert.ToBase64String(buff);
        //}



        // Generating the 256 hash

        public string GenerateSHA256Hash(string input)
        {

            if (string.IsNullOrEmpty(input)) return null;

            byte[] bytes = Encoding.UTF8.GetBytes(input);
            SHA256Managed sha256hashstring = new SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ByteArrayToHexString(hash);



        }


        // convert byte array to hex strings

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }


       
    }
}  

       