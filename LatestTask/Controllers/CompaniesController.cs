using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LatestTask.Models;
using System.IO;

namespace LatestTask.Controllers
{
    public class CompaniesController : Controller
    {
        private ProjectDBEntities db = new ProjectDBEntities();

        // GET: Companies
        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CID,CName,CType,Email,CUrl,Edate,Password,Capital,Logo")] Company company, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                //if (company.Capital < 10000)
                //{
                //    ModelState.AddModelError("Capital", "Capital Less than 10000");
                //    return View(company);
                //}

                string path = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = "~/images/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }
                company.Logo = path;
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Logo,Password,ConfEmail,ConfPass")] Company company, HttpPostedFileBase imgFile)

        {
            string path = "";
            if (imgFile.FileName.Length > 0)
            {
                path = "~/images/" + Path.GetFileName(imgFile.FileName);
                imgFile.SaveAs(Server.MapPath(path));
            }
            company.Logo = path;

            var before = db.Companies.AsNoTracking().Where(x => x.CID == company.CID).ToList().FirstOrDefault();
            company.Password = before.Password;
            company.ConfEmail = before.Email;
            company.ConfPass = before.Password;


            db.Entry(company).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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

        public int Count()
        {

            return db.Companies.ToList().Count;
        }
        public int Max()
        {
            return db.Companies.Max(m => m.Capital).Value;

        }
        public int Min()
        {
            return db.Companies.Min(m => m.Capital).Value;
        }
        public double Avg()
        {
            return db.Companies.Average(m => m.Capital).Value;
        }
        public ActionResult getManyRows()
        {
            // var recs = db.Companies.ToList();
            var recs = db.Companies.ToList().OrderBy(x => x.Capital);
            return View(recs);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login([Bind(Include ="Email,Password")] Company company)
        {
            var rec = db.Companies.Where(x=> x.Email==company.Email && x.Password==company.Password).ToList().FirstOrDefault();
            if(rec != null)
            {
                Session["userName"] = rec.CName;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid User";
                return View(company);
            }
           
        }

        public ActionResult HomePage()
        {
            var resc = db.Companies.ToList();
            return View();
        }
    }
}
