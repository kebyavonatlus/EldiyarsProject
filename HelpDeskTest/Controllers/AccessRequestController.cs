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
    public class AccessRequestController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AccessRequest
        public ActionResult Index()
        {
            var from = from request in db.Requests
                       join accessrequest in db.AccessRequests on request.RequestId equals accessrequest.RequestId
                       join employe in db.Employes on request.EmployeId equals employe.EmployeID
                       select new ShowAccessRequestViewModel
                       {
                           RequestId = request.RequestId,
                           EmployeId = employe.Name, 
                           DateOfRegistration = request.DateOfRegistration,
                           FIOEmploye = accessrequest.FioEmploye,
                           Rationale = accessrequest.Rationale,
                           Spark = accessrequest.Spark,
                           Resource = accessrequest.Resource,
                           PostOffice = accessrequest.PostOffice,
                           NetworkFolder = accessrequest.NetworkFolder,
                           Period = accessrequest.Period,

                       };
            
            return View(from.ToList());
        }

        // GET: AccessRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
           AccessRequest accessRequest = db.AccessRequests.Find(id);
            if (accessRequest == null)
            {
                return HttpNotFound();
            }
           
           var requests = db.ConfirmRequestEmployes.Where(x => x.RequestId == id);
            ViewBag.request = requests;
            return View(accessRequest);
            
        }

        public ActionResult toModerator(int id) 
        {
            return RedirectToAction("Create", "RequestEmployes", new { id = id });
        }

        // GET: AccessRequest/Create
        public ActionResult Create()
        {
            

            return View();
        }

        // POST: AccessRequest/Create
        [HttpPost]
        public ActionResult Create(AccessRequest accessRequest)
        {
            var username = User.Identity.Name; // ваш username/login
            var userID = db.Users.First(u => u.UserName == username)?.Id; // Id залогиненного пользователя
            var employee = db.Employes.First(e => e.UserId == userID); // сотрудник

            var request = new Request()
            {
                RequestTypeID = RequestType.AccessRequest,
                DateOfRegistration = DateTime.Today,
                StatusId = StatusType.Created,
                EmployeId = employee.EmployeID
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                db.Requests.Add(request);
                db.SaveChanges();
                accessRequest.RequestId = request.RequestId;
                db.AccessRequests.Add(accessRequest);
                db.SaveChanges();
                transaction.Commit();
            }

            return RedirectToAction("Index", "AllRequest");
        }

        // GET: AccessRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            AccessRequest accessRequest = db.AccessRequests.Find(id);
            if (accessRequest==null)
            {
                return HttpNotFound();
            }
            return View(accessRequest);
        }

        // POST: AccessRequest/Edit/5
        [HttpPost]
        public ActionResult Edit(AccessRequest accessRequest)
        {
            db.Entry(accessRequest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AccessRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccessRequest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
