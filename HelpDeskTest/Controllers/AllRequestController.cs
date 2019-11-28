using HelpDeskTest.Enums;
using HelpDeskTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskTest.Controllers
{
    [Authorize]
    public class AllRequestController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            //var allRequest = new List<AllRequestViewModel>();
            //var request = db.Requests.ToList();
            //var employee = db.Employes.ToList();
            //foreach (var req in request)
            //{
            //    var employeeId = employee
            //        .FirstOrDefault(i => 
            //    i.EmployeID == req.EmployeId);

            //    allRequest.Add(new AllRequestViewModel {
            //        RequestId = req.RequestId,

            //        RequestType = req.RequestTypeID,

            //        StatusType = req.StatusId,
            //        EmployeId = employeeId.Name,
            //        DateOfRegistration = req.DateOfRegistration,
            //        DateOfExit = req.DateOfExit

            //    });
            //}
            var allRequest = from request in db.Requests
                             join employe in db.Employes on request.EmployeId equals employe.EmployeID

                             select new AllRequestViewModel
                             {
                                 RequestId = request.RequestId,

                                 RequestType = request.RequestTypeID,

                                 StatusType = request.StatusId,
                                 EmployeId = employe.Name,
                                 DateOfRegistration = request.DateOfRegistration,
                                 DateOfExit = request.DateOfExit
                             };

            return View(allRequest.ToList());
        }

        public ActionResult to_reqEmpl(int id)
        {
            return RedirectToAction("Create", "RequestEmployes", new { id = id });
        }

        [HttpGet]
        public ActionResult Create(RequestType requestTypeID)
        {
            if (requestTypeID == RequestType.IBankRequest)
                return RedirectToAction("Create", "IBank");

            return RedirectToAction("Create", "AccessRequest");
        }

        [HttpPost]
        public ActionResult Create(Request request)
        {
            //db.Requests.Add(request);
            //  db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Close(int? RequestId)
        {
            if (RequestId == null)
                return HttpNotFound();
            Request request = db.Requests.Find(RequestId);
            if (request == null)
                return HttpNotFound();
            return View(request);
        }
        [HttpPost]
        public ActionResult Close(Request request)
        {
            var username = User.Identity.Name; // ваш username/login
            var userID = db.Users.First(u => u.UserName == username)?.Id; // Id залогиненного пользователя
            var employee = db.Employes.First(e => e.UserId == userID); // сотрудник

            request.ExecutorId = employee.EmployeID;
            var b = request.ExecutorId;
            ViewBag.Employe = b;
            request.DateOfRegistration = request.DateOfRegistration;
            request.DateOfExit = DateTime.Now;
            request.StatusId = StatusType.Finish;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Report(int? RequestId)
        {
            if (RequestId == null)
                return HttpNotFound();
            Request request = db.Requests.Find(RequestId);
            var employe = request.ExecutorId;
            var executor = request.EmployeId;
            if (request == null)
                return HttpNotFound();

            Employe employeName = db.Employes.FirstOrDefault(a => a.EmployeID == employe);
            ViewBag.employe = employeName;

            Employe executorName = db.Employes.FirstOrDefault(a => a.EmployeID == executor);
            ViewBag.executor = executorName;

            var allExecutor = db.ConfirmRequestEmployes.Where(x => x.RequestId == RequestId).ToList();
                ViewBag.ex = executor;
            return View(request);
        }

        public ActionResult EditAllRequest(int? id)
        {
            if (id == null)
                return HttpNotFound();

            Request request = db.Requests.Find(id);
            if (request.RequestTypeID == RequestType.IBankRequest)
                RedirectToAction("Details", "IBank", new { id });

            else

                return RedirectToAction("Create", "AccessRequest");

            return View(); 
        }

        [HttpPost]
        public ActionResult EditAllRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowAllRequest");
            }
            return View(request);
        }
        [HttpGet]
        public ActionResult Details(int? RequestId)
        {
            if (RequestId == null)
                return HttpNotFound();
            Request request = db.Requests.Find(RequestId);

            if (request.RequestTypeID == RequestType.IBankRequest)
                return RedirectToAction("Details", "IBank",new { id = RequestId });

            else

                return RedirectToAction("Details", "AccessRequest",new { id = RequestId });
         
         
        }

        public ActionResult ShowDetailsAllRequest(int? RequestId)
        {
            if (RequestId == null)
                return HttpNotFound();
            Request request = db.Requests.Find(RequestId);

            if (RequestId == null)
            {
                return HttpNotFound();
            }
            return View(RequestId);


        }
    }
}