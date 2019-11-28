using HelpDeskTest.Enums;
using HelpDeskTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskTest.Controllers
{
    [Authorize]
    public class IBankController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: IBank
        public ActionResult Index()
        { var requestInternetBank = from request in db.Requests
                                    join requestIbank in db.IBanks on request.RequestId equals requestIbank.RequestId
                                    join employe in db.Employes on request.EmployeId equals employe.EmployeID
                                    select new ShowInternetBankingingRequestViewModel
                                    {
                                        RequestId = request.RequestId,

                                        RequestType = RequestType.IBankRequest,

                                        StatusType = StatusType.Created,

                                        EmployeId = employe.Name,

                                        DateOfRegistration = request.DateOfRegistration,

                                        CustomerUsername = requestIbank.NameOfWorks,

                                        InternetBankingPin = requestIbank.Cause
                                    };
            return View(requestInternetBank.ToList());
        }

        public ActionResult Home()
        {
           return RedirectToAction("Index", "AllRequest");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ResetUserPasswordRequest resetUserPassword)
        {
            // var employeeID = 0; // 

            var username = User.Identity.Name; // ваш username/login
            var userID = db.Users.First(u => u.UserName == username)?.Id; // Id залогиненного пользователя
            var employee = db.Employes.First(e => e.UserId == userID); // сотрудник

            var request = new Request()
            {
                RequestTypeID = RequestType.IBankRequest,
                DateOfRegistration = DateTime.Today,
                StatusId = StatusType.Created,
                EmployeId = employee.EmployeID
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                db.Requests.Add(request);
                db.SaveChanges();
                resetUserPassword.RequestId = request.RequestId;
                db.IBanks.Add(resetUserPassword);
                db.SaveChanges();
                transaction.Commit();
            }

            return RedirectToAction("Index", "Allrequest");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();

            ResetUserPasswordRequest resetUser = db.IBanks.Find(id);
            if (resetUser != null)
            {
                return View(resetUser);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(ResetUserPasswordRequest resetUser)
        {
            db.Entry(resetUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("EditAllRequest", "AllRequest");
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResetUserPasswordRequest req = db.IBanks.Find(id);
            if (req == null)
            {
                return HttpNotFound();
            }
            return View(req);
        }

        public ActionResult To_reqEmpl(int id)
        {
            return RedirectToAction("Create", "RequestEmployes", new { id = id });
        }


    }
}