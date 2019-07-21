using SabaiPadhau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication74.Models;

namespace SabaiPadhau.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public bool RememberMe { get; set; }
        Sabai_PadhauEntities _db = new Sabai_PadhauEntities();
        public static int UserID;
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(tblUser l, string ReturnUrl = "")
        {

            string password = Utilities.Base64Encode(l.Password);
                var users = _db.tblUsers.Where(a => a.Username == l.Username && a.Password == password).FirstOrDefault();
            if (users.Username == "admin")
            {
                UserID = users.UserId;
                return RedirectToAction("AdminPanel", "Login");
            }
                if (users != null)
                {
                    FormsAuthentication.SetAuthCookie(l.Username, RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                    UserID = users.UserId;
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }
            
            return View();

        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(tblUser td)
        {
            string password = Utilities.Base64Encode(td.Password);
            tblUser tb = new tblUser();
            UserRole rb = new UserRole();
            //tblCertificate ce = new tblCertificate();

            tb.Username = td.Username;
            tb.FullName = td.FullName;
            tb.Email = td.Email;
            tb.Password = password;
            _db.tblUsers.Add(tb);

            rb.UserId = tb.UserId;
            rb.RoleId = 1;
            _db.UserRoles.Add(rb);

            tblCertificate tc = new tblCertificate();
            tc.IsVerified = false;

            //ce.UserId = td.UserId;
            //ce.GovernmentVerifiedImage = "";
            //_db.tblCertificates.Add(ce);

            _db.SaveChanges();
            return RedirectToAction("Index","Login");
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult AdminPanel()
        {
            List<tblCertificate> ur = _db.tblCertificates.Where(m=>m.IsVerified==false).ToList();
            

            return View(ur);
        }
        public ActionResult Confirm(int id)
        {
            UserRole ur = _db.UserRoles.Where(m => m.UserId == id).FirstOrDefault();
            ur.RoleId = 2;
            _db.SaveChanges();
            return View();
        }
        public ActionResult Cancel(int id)
        { 

            UserRole ur = _db.UserRoles.Where(m => m.UserId == LoginController.UserID).FirstOrDefault();
            ur.RoleId =1;
           _db.SaveChanges();

            return View();
        }
    }
}