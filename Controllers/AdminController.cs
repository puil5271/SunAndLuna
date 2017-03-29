/*

        Admin Controller

            - managing Admin Information. Also, Admin can access user information so that they can modify user account information.
                
                1. index
                2. AdminRegister
                3. AdminLogin
                4. AdminLoggedIn
                5. AdminLogout
                6. AdminEditUserInfo
                7. AdminEditInfo
*/

using SunAndLuna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SunAndLuna.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Adminid"] != null)
            {
                using (UserDBContext db = new UserDBContext())  // Get the users info. to Admin index page
                {
                    return View(db.Users.ToList());
                }
            }
            else
            {
                ModelState.AddModelError("", "You can not access this database.");

                return View();
            }
        }

        public ActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminRegister(Admin AdminAccount)
        {
            if (ModelState.IsValid)
            {
                AdminAccount.IsAuthorized = false;

                using (AdminDBContext db = new AdminDBContext())
                {
                    db.Admins.Add(AdminAccount);
                    db.SaveChanges();
                }

                ModelState.Clear();
                ViewBag.Message = AdminAccount.AdminFirstName + "" + AdminAccount.AdminLastName + " Successfully registered!";
            }

            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin AdminAccount)
        {
            using (AdminDBContext db = new AdminDBContext())
            {
                var Adminuser = db.Admins.Single(u => u.AdminEmail == AdminAccount.AdminEmail && u.AdminPassword == AdminAccount.AdminPassword);
                if (Adminuser != null)
                {
                    if (Adminuser.IsAuthorized == true)
                    {
                        Session["Adminid"] = Adminuser.AdminID.ToString();
                        Session["Adminfirstname"] = Adminuser.AdminFirstName.ToString();
                        Session["Adminlastname"] = Adminuser.AdminLastName.ToString();
                        Session["Adminfullname"] = Adminuser.AdminFirstName.ToString() + " " + Adminuser.AdminLastName.ToString();
                        Session["Adminemail"] = Adminuser.AdminEmail.ToString();
                        Session["IsAuthorized"] = "true";

                        return RedirectToAction("AdminLoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your Account is not authorized.");

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

        public ActionResult AdminLoggedIn()
        {
            if (Session["Adminid"] != null)
            {
                return RedirectToAction("index", "Admin", new { area = "" });
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
        }

        public ActionResult AdminLogout()
        {
            Session.Clear();

            return RedirectToAction("index", "Home", new { area = "" });
        }


        public ActionResult AdminEditUserInfo(string id)
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
        public ActionResult AdminEditUserInfo(User UserAccount)
        {
            using (UserDBContext db = new UserDBContext())
            {
                User user = db.Users.Single(u => u.UserEmail == UserAccount.UserEmail);

                user.UserFirstName = UserAccount.UserFirstName;
                user.UserLastName = UserAccount.UserLastName;
                user.UserPassword = UserAccount.UserPassword;
                user.UserEmail = UserAccount.UserEmail;
                user.UserGender = UserAccount.UserGender;
                user.UserDateOfBirth = UserAccount.UserDateOfBirth;
                user.IsActivated = UserAccount.IsActivated;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "Admin", new { area = "" });
            }
        }

        public ActionResult AdminEditInfo(string id)
        {
            int adminid = Convert.ToInt32(id);

            using (AdminDBContext db = new AdminDBContext())
            {
                Admin admin = db.Admins.Single(u => u.AdminID == adminid);

                return View(admin);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminEditInfo(Admin admin)
        {
            using (AdminDBContext db = new AdminDBContext())
            {
                Admin Admindb = db.Admins.Single(u => u.AdminID == admin.AdminID);

                Admindb.AdminFirstName = admin.AdminFirstName;
                Admindb.AdminLastName = admin.AdminLastName;
                Admindb.AdminPassword = admin.AdminPassword;
                Admindb.AdminConfirmPassword = admin.AdminPassword;
                Admindb.AdminEmail = admin.AdminEmail;
                Admindb.AdminGender = admin.AdminGender;
                Admindb.AdminDateOfBirth = admin.AdminDateOfBirth;

                db.Entry(Admindb).State = EntityState.Modified;
                db.SaveChanges();

                Session["Adminfirstname"] = admin.AdminFirstName;
                Session["Adminlastname"] = admin.AdminLastName;
                Session["Adminfullname"] = admin.AdminFirstName + " " + admin.AdminLastName;
                Session["Adminemail"] = admin.AdminEmail;

                return RedirectToAction("index", "Admin", new { area = "" });
            }
        }
    }
}