/*

        Account Controller
            
            - managing User Account Information
                
                1. Index
                2. Register
                3. Login
                4. LoggedIn
                5. Logout
                6. UserEditInfo

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunAndLuna.Models;
using System.Data.Entity;

namespace SunAndLuna.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (UserDBContext db = new UserDBContext())
            {
                return View(db.Users.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User UserAccount)
        {
            if (ModelState.IsValid)
            {
                UserAccount.IsActivated = false;

                using (UserDBContext db = new UserDBContext())
                {
                    db.Users.Add(UserAccount);
                    db.SaveChanges();
                }

                ModelState.Clear();
                ViewBag.Message = UserAccount.UserFirstName + "" + UserAccount.UserLastName + " Successfully registered!";
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User UserAccount)
        {
            using (UserDBContext db = new UserDBContext())
            {
                var user = db.Users.Single(u => u.UserEmail == UserAccount.UserEmail && u.UserPassword == UserAccount.UserPassword);
                if (user != null)
                {
                    if (user.IsActivated == true)
                    {
                        Session["Userid"] = user.UserID.ToString();
                        Session["Userfirstname"] = user.UserFirstName.ToString();
                        Session["Userlastname"] = user.UserLastName.ToString();
                        Session["Userfullname"] = user.UserFirstName.ToString() + " " + user.UserLastName.ToString();
                        Session["Useremail"] = user.UserEmail.ToString();
                        Session["IsActivated"] = "true";

                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your Account is not Activated.");

                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email Address or Password is wrong.");

                    return View();
                }
            }
        }

        public ActionResult LoggedIn()
        {
            if (Session["Userid"] != null)
            {
                return RedirectToAction("index", "Main", new { area = "" });
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("index", "Home", new { area = "" });
        }


        public ActionResult UserEditInfo(string id)
        {
            int userid = Convert.ToInt32(id);

            using (UserDBContext db = new UserDBContext())
            {
                User user = db.Users.Single(u => u.UserID == userid);

                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEditInfo(User UserAccount)
        {
            using (UserDBContext db = new UserDBContext())
            {
                User user = db.Users.Single(u => u.UserID == UserAccount.UserID);

                user.UserFirstName = UserAccount.UserFirstName;
                user.UserLastName = UserAccount.UserLastName;
                user.UserPassword = UserAccount.UserPassword;
                user.UserConfirmPassword = UserAccount.UserPassword;
                user.UserGender = UserAccount.UserGender;
                user.UserDateOfBirth = UserAccount.UserDateOfBirth;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                Session["UserFirstName"] = UserAccount.UserFirstName;
                Session["UserLastName"] = UserAccount.UserLastName;
                Session["Userfullname"] = UserAccount.UserFirstName + " " + UserAccount.UserLastName;

                return RedirectToAction("index", "Main", new { area = "" });
            }
        }
    }
}