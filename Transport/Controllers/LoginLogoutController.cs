using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Transport.Models;

namespace Transport.Controllers
{
    public class LoginLogoutController : Controller
    {
        private TransportDBEntities newTranspore;
        public LoginLogoutController()
        {
            newTranspore = new TransportDBEntities();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["userId"] != null)
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Logout" });
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
            else
            {
                try 
                {
                    return View();
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginMV newUser)
        {
            if (Session["userId"] != null)
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Logout" });
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var obj = newTranspore.Users.Where(a => a.userLogin.Equals(newUser.userLogin) && a.passwordLogin.Equals(newUser.passwordLogin)).FirstOrDefault();
                        if (obj != null)
                        {
                            // search for true false
                            FormsAuthentication.SetAuthCookie(obj.userid.ToString(), false);
                            if (!string.IsNullOrEmpty(obj.userid.ToString())) {
                                Session["userId"] = obj.userid.ToString();
                            }
                            else
                            {
                                ModelState.AddModelError("", "يرجى مراجعة مسؤول النظام بسبب عدم تواجد رقم رمزي فريد لهذا الحساب");
                                return View(newUser);
                            }

                            if (!string.IsNullOrEmpty(obj.userFname.ToString()))
                            {
                                Session["userFname"] = obj.userFname.ToString();
                            }
                            else
                            {
                                ModelState.AddModelError("", "يرجى مراجعة مسؤول النظام بسبب عدم تواجد الاسم الاول لهذا الشخص");
                                return View(newUser);
                            }

                            if (!string.IsNullOrEmpty(obj.userLname.ToString()))
                            {
                                Session["userLname"] = obj.userLname.ToString();
                            }
                            else
                            {
                                ModelState.AddModelError("", "يرجى مراجعة مسؤول النظام بسبب عدم تواجد اسم العائلة لهذا الشخص");
                                return View(newUser);
                            }

                            if (!string.IsNullOrEmpty(obj.userIdentifiy.ToString()))
                            {
                                Session["userIdentifiy"] = obj.userIdentifiy.ToString();
                            }
                            else
                            {
                                //............just option Can Delete.....................
                                ModelState.AddModelError("", "يرجى مراجعة مسؤول النظام بسبب عدم رقم الهوية لهذا الشخص");
                                return View(newUser);
                            }
                                return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "هذا الحساب غير متواجد بالنظام, يرجى مراجعة مسؤول النظام");
                            return View(newUser);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "فشل العملية: القيم المدخلة خاطئة");
                        return View(newUser);
                    }
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
        }
        //logout
        [Authorize]
        public ActionResult Logout()
        {
            if (Session["userId"] != null)
            {
                try
                {
                    Session.Abandon();
                    Session.Clear();
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "LoginLogout");
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
        }
        [Authorize]
        public ActionResult Error()
        {
            if (Session["userId"] != null)
            {   
                try
                {
                    return View();
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
        }
        [Authorize]
        public ActionResult ErrorNotAllowed()
        {
            if (Session["userId"] != null)
            {
                try
                {
                    return View();
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "LoginLogout");
                }
            }
        }

    }
}