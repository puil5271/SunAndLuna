/*

        Main Controller

            - Display DiaryNotes, Bill tracking system, and Scheduler
                
                1. index
                2. Diary
                3. Schedule
                4. BillTrack

*/

using SunAndLuna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunAndLuna.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Diary()
        {
            if (Session["Userid"] != null)
            {
                using (DiaryDBContext db = new DiaryDBContext())  // Get the users info. to Admin index page
                {
                    var diaryuserid = Convert.ToInt32(Session["Userid"]);
                    var diarynotes = db.DiaryNotes.Where(d => d.UserID == diaryuserid).ToList();
                    return View(diarynotes);
                }
            }

            else
            {
                ModelState.AddModelError("", "You can not access this database.");

                return View();
            }
        }

        public ActionResult Schedule()
        {
            return View();
        }

        public ActionResult BillTrack()
        {
            return View();
        }
    }
}