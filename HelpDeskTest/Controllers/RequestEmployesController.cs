using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDeskTest.Enums;
using HelpDeskTest.Models;
using Microsoft.AspNet.Identity.Owin;

namespace HelpDeskTest.Controllers
{
    [Authorize]
    public class RequestEmployesController : Controller
    {
        private ApplicationUserManager _userManager;

        public RequestEmployesController()
        {
        }

        public RequestEmployesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

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

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestEmployes
        public ActionResult Index()
        {

            return View(db.ConfirmRequestEmployes.ToList());
        }

        //public ActionResult Delete(int? id)
        //{

        //}
        // GET: RequestEmployes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            if (request.RequestTypeID == RequestType.IBankRequest)
            {
                return RedirectToAction("DetailsIBank", new { id = id });

            }
            else if (request.RequestTypeID == RequestType.AccessRequest)
            {
                return RedirectToAction("DetailsAccesRequest", new { id = id });

            }

            else return HttpNotFound();
        }

        public ActionResult DetailsIBank(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            var employe = request.EmployeId;
            if (request == null)
            {
                return HttpNotFound();
            }
            ResetUserPasswordRequest r = db.IBanks.FirstOrDefault(a => a.RequestId == id);
            ViewBag.r = r;
            Employe employeName = db.Employes.FirstOrDefault(a => a.EmployeID == employe);
            ViewBag.employe = employeName;
            return View(request);
        }

        public ActionResult DetailsAccesRequest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            var employe = request.EmployeId;
            if (request == null)
            {
                return HttpNotFound();
            }
            AccessRequest accessRequest = db.AccessRequests.FirstOrDefault(a => a.RequestId == id);
            ViewBag.accessRequest = accessRequest;
            Employe employeName = db.Employes.FirstOrDefault(a => a.EmployeID == employe);
            ViewBag.employe = employeName;
            return View(request);
        }

        [Authorize(Roles ="moderator")]
        public ActionResult MyApprof()
        {
            var username = User.Identity.Name; // ваш username/login
            var userID = db.Users.First(u => u.UserName == username)?.Id; // Id залогиненного пользователя
            var employee = db.Employes.First(e => e.UserId == userID);


            var request = db.ConfirmRequestEmployes.Where(a => a.ExecutorId == employee.EmployeID);

            return View(request);
        }
      
        public ActionResult DetailsMyApprof(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            if (request.RequestTypeID == RequestType.IBankRequest)
            {
                return RedirectToAction("DetailsIBankApprof", new { id = id });

            }
            else if (request.RequestTypeID == RequestType.AccessRequest)
            {
                return RedirectToAction("DetailsAccesRequestApprof", new { id = id });

            }

            else return HttpNotFound();
        }

        public ActionResult DetailsIBankApprof(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            var employe = request.EmployeId;
            if (request == null)
            {
                return HttpNotFound();
            }
            ResetUserPasswordRequest r = db.IBanks.FirstOrDefault(a => a.RequestId == id);
            ViewBag.r = r;
            Employe employeName = db.Employes.FirstOrDefault(a => a.EmployeID == employe);
            ViewBag.employe = employeName;
            return View(request);
        }

        public ActionResult DetailsAccesRequestApprof(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            var employe = request.EmployeId;
            if (request == null)
            {
                return HttpNotFound();
            }
            AccessRequest accessRequest = db.AccessRequests.FirstOrDefault(a => a.RequestId == id);
            ViewBag.accessRequest = accessRequest;
            Employe employeName = db.Employes.FirstOrDefault(a => a.EmployeID == employe);
            ViewBag.employe = employeName;
            return View(request);
        }
        /// <summary>
        /// Requests sent to moderator
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "moderator")]
        public ActionResult Requests()
        {
            var username = User.Identity.Name; // ваш username/login
            var userID = db.Users.First(u => u.UserName == username)?.Id; // Id залогиненного пользователя
                                                                          //  var employeeId = db.Employes.First(employe => employe.UserId == userID)?.EmployeID;
            var moderator = db.Employes.First(e => e.UserId == userID);// 

            var employeeRequestIds = db.RequestEmployes
                .Where(requestEmployee => requestEmployee.EmployeId == moderator.EmployeID)
                .Select(requestEmployee => requestEmployee.RequestId)
                .ToList();

            var requests = db.Requests.Where(request => employeeRequestIds.Contains(request.RequestId)).ToList();


            return View(requests);
        }


        [HttpGet]
        // GET: RequestEmployes/Create
        public ActionResult Create(int id)
        {
            var moderatorRole = db.Roles.FirstOrDefault(r => r.Name == "moderator");
            var moderatorUserIDs = moderatorRole.Users.Select(u => u.UserId).ToList();
            var moderatorEmployess = db.Employes.Where(e => moderatorUserIDs.Contains(e.UserId)).ToList();

            SelectList employe = new SelectList(moderatorEmployess, "EmployeID", "Name");
            ViewBag.Employes = employe;
            ViewBag.reqId = id;
            return View();
        }

        // POST: RequestEmployes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlinkВыполнение данной инструкции /?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestId,EmployeId")] RequestEmploye requestEmploye)
        {
            if (ModelState.IsValid)
            {
                db.RequestEmployes.Add(requestEmploye);
                db.SaveChanges();
            }
            var a = requestEmploye.RequestId;
            Request request = db.Requests.Find(a);
            if (request.RequestTypeID == RequestType.IBankRequest)
                return RedirectToAction("Details", "IBank", new { id = request.RequestId });

            else

                return RedirectToAction("Details", "AccessRequest", new { id = request.RequestId });

        }

        [Authorize(Roles = "moderator")]
        [HttpGet]
        public ActionResult CreateAccess() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccess(int id, int employeId)
        {
            var username = User.Identity.Name; // ваш username/login
            var userID = db.Users.First(u => u.UserName == username)?.Id; // Id залогиненного пользователя
            var employee = db.Employes.First(e => e.UserId == userID);

            var requestEmployee = new ConfirmRequestEmploye
            {
                RequestId = id,
                EmployeId = employeId,
                DateOfExit = DateTime.Now,
                Comment = "Я согласен",
                Confirmed = true,
                ExecutorId = employee.EmployeID
            };
            db.ConfirmRequestEmployes.Add(requestEmployee);
            
            db.SaveChanges();

            var entity = db.RequestEmployes.First(x => x.RequestId == id && x.EmployeId == employeId);
            db.RequestEmployes.Remove(entity);
            db.SaveChanges();
                return RedirectToAction("Requests");
        }

    
        // GET: RequestEmployes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestEmploye requestEmploye = db.RequestEmployes.Find(id);
            if (requestEmploye == null)
            {
                return HttpNotFound();
            }
            return View(requestEmploye);
        }

        // POST: RequestEmployes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,EmployeId")] RequestEmploye requestEmploye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestEmploye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestEmploye);
        }

        // GET: RequestEmployes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RequestEmploye requestEmploye = db.RequestEmployes.Find(id);
        //    if (requestEmploye == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(requestEmploye);
        //}

        // POST: RequestEmployes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestEmploye requestEmploye = db.RequestEmployes.Find(id);
            db.RequestEmployes.Remove(requestEmploye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
