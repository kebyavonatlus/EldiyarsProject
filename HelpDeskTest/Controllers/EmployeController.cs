using HelpDeskTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;


namespace HelpDeskTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Employe


            //вывод всех сотрудников
        public ActionResult Index()
        {
            var emplo = from employe in db.Employes
                           join department in db.Departments on employe.DepartmentID equals department.ID
                           join user in db.Users on employe.UserId equals user.Id
                           select new EmployeViewModel
                           {
                               EmployeID = employe.EmployeID,
                               Name = employe.Name,
                               PositionName = employe.PositionName,
                               DepartmentID = department.Name,
                               UserLog=user.Email
                           };
            
                           return View(emplo.ToList());
        }

        public ActionResult Create()
        {
            // Формируем список команд для передачи в представление

            SelectList departments = new SelectList(db.Departments, "ID", "Name");
            SelectList users = new SelectList(db.Users, "ID", "Email");

            ViewBag.Departments = departments;
            ViewBag.Users = users;
            return View();
           
        }

        [HttpPost]
        public ActionResult Create(Employe employe)
        {
            db.Employes.Add(employe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}