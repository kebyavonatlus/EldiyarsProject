using HelpDeskTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: UserRoles
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var roles = context.Roles.ToList();
                return View(roles);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            using (var context = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                    return View();

                context.Roles.Add(role);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
               
        }

        public ActionResult Users()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = context.Users.ToList();
                var roles = context.Roles.ToList();
                var employe = context.Employes.ToList();
                var userRoles = new List<UserRolesViewModel>();

                foreach (var user in users)
                {
                    var roleNames = new List<string>();
                    foreach (var userRole in user.Roles)
                    {
                        var role = roles.FirstOrDefault(r =>
                            r.Id == userRole.RoleId);
                        roleNames.Add(role.Name);
                    }
                    userRoles.Add(new UserRolesViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        RoleName = string.Join(", ", roleNames)
                    });
                }
                return View(userRoles);
            }

        }

        public ActionResult EditUser(string userID)
        {
            using (var context = new ApplicationDbContext())
            {
                

                var user = context.Users.FirstOrDefault(u =>
                    u.Id == userID);

                var roles = context.Roles.ToList();
                var roleNames = new List<string>();
                foreach (var userRole in user.Roles)
                {
                    var role = roles.First(r =>
                        r.Id == userRole.RoleId);
                    roleNames.Add(role.Name);
                }

                ViewBag.Roles = roles
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id,
                        Text = r.Name
                    }).ToList();

                return View(new UserRolesViewModel
                {
                    UserID = user.Id,
                    UserName = user.UserName,
                    RoleName = string.Join(", ", roleNames)
                });
            }
        }


        [HttpPost]
        public ActionResult AddRoleToUser(string UserName,
            string roleID)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u =>
                    u.UserName == UserName);
                var role = context.Roles.FirstOrDefault(r =>
                    r.Id == roleID);

                UserManager.AddToRole(user.Id, role.Name);

                return RedirectToAction("Users");
            }
        }


    }
}