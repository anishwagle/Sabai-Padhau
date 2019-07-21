using SabaiPadhau.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SabaiPadhau.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }
        Sabai_PadhauEntities _db = new Sabai_PadhauEntities();
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(tblCourse tb)
        {

            HttpPostedFileBase fup = Request.Files["VideoPath"];
            if (fup != null)
            {
                //string fileName =Convert.ToString( LoginController.UserID )+ fup.FileName;
                tb.VideoPath = fup.FileName;
                tb.UserId = LoginController.UserID;
                fup.SaveAs(Path.Combine(Server.MapPath("~/Videos/" + fup.FileName)));
            }
            _db.tblCourses.Add(tb);
            if (_db.SaveChanges() > 0)
            {
                return RedirectToAction("Index", "User");
            }


            return View();
        }
    }
}