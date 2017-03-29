using SunAndLuna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunAndLuna.Controllers
{
    public class DiaryController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Userid"] != null)
            {
                return RedirectToAction("Diary", "Main", new { area = "" });
            }
            else
            {
                return RedirectToAction("index", "Home", new { area = "" });
            }
        }

        public ActionResult CreateDiary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDiary(Diary diary)
        {
            if (ModelState.IsValid)
            {
                diary.IsDelete = false;
                diary.UserID = Convert.ToInt32(Session["Userid"]);

                using (DiaryDBContext db = new DiaryDBContext())
                {
                    db.DiaryNotes.Add(diary);
                    db.SaveChanges();
                }

                ModelState.Clear();
            }

            return View();
        }
    }
}