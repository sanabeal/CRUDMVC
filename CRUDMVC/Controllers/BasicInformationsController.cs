using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUDMVC.Models;

namespace CRUDMVC.Controllers
{
    public class BasicInformationsController : Controller
    {
        private EmployeeEntities db = new EmployeeEntities();

        // GET: BasicInformations
        public ActionResult Index()
        {
            return View(db.BasicInformations.ToList());
        }

        // GET: BasicInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasicInformation basicInformation = db.BasicInformations.Find(id);
            if (basicInformation == null)
            {
                return HttpNotFound();
            }
            return View(basicInformation);
        }

        // GET: BasicInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,CreatedAt,UpdatedAt")] BasicInformation basicInformation)
        {
            if (ModelState.IsValid)
            {
                var name = basicInformation.Name;
                var count = db.BasicInformations.Where(s => s.Name.Contains(name)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Name already exists";
                    return View();
                }
                basicInformation.CreatedAt = DateTime.Now;
                basicInformation.UpdatedAt = DateTime.Now;
                db.BasicInformations.Add(basicInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.message = "Insert failed!";
            return View();
        }

        // GET: BasicInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasicInformation basicInformation = db.BasicInformations.Find(id);
            if (basicInformation == null)
            {
                return HttpNotFound();
            }
            return View(basicInformation);
        }

        // POST: BasicInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,CreatedAt,UpdatedAt")] BasicInformation basicInformation)
        {
            if (ModelState.IsValid)
            {
                var data = db.BasicInformations.Find(basicInformation.Id);

                data.Name = basicInformation.Name;
                data.Address = basicInformation.Address;
                data.UpdatedAt = DateTime.Now;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var dataEdit = db.BasicInformations.Where(s => s.Id == basicInformation.Id).FirstOrDefault();
            return View(dataEdit);
        }

        // GET: BasicInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasicInformation basicInformation = db.BasicInformations.Find(id);
            if (basicInformation == null)
            {
                return HttpNotFound();
            }
            return View(basicInformation);
        }

        // POST: BasicInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BasicInformation basicInformation = db.BasicInformations.Find(id);
            db.BasicInformations.Remove(basicInformation);
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
