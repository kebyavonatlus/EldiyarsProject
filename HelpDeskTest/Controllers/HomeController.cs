using HelpDeskTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskTest.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return RedirectToAction("Index", "AllRequest");
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Request request)
        {

            var username = User.Identity.Name; // ваш username/login
            var userID = db.Users.First(u => u.UserName == username)?.Id; // Id залогиненного пользователя
            var employee = db.Employes.First(e => e.UserId == userID); // сотрудник
           
            db.Requests.Add(request);
            db.SaveChanges();
            return RedirectToAction("Index");

            
        }
    }
}