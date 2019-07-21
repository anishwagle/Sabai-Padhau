using SabaiPadhau.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SabaiPadhau.Controllers
{
    
    public class UserController : Controller
    {
        Sabai_PadhauEntities _db = new Sabai_PadhauEntities();
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            List<tblCourse> tb = _db.tblCourses.ToList();
            return View(tb);
        }

        public ActionResult ApplyForTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ApplyForTeacher(tblCertificate tb)
        {

            HttpPostedFileBase fup = Request.Files["GovernmentVerifiedImage"];
            if (fup != null)
            {
                //string fileName =Convert.ToString( LoginController.UserID )+ fup.FileName;
                tb.GovernmentVerifiedImage = fup.FileName;
                tb.UserId = LoginController.UserID;
                tb.IsVerified = false;
                fup.SaveAs(Path.Combine(Server.MapPath("~/img/certificate/" + fup.FileName)));
            }
            _db.tblCertificates.Add(tb);
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("ResponseCert","User");
            }


            return View();
        }
        public ActionResult ResponseCert()
        {
            UserRole ur = _db.UserRoles.Where(m => m.UserId == LoginController.UserID).FirstOrDefault();
            ur.RoleId =3;
            _db.SaveChanges();

            return View();
        }
        public ActionResult ContactTeacher(int id)
        {
            return View();
        }
    }
}