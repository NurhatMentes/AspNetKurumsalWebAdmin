using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class AdminProfilController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        // GET: AdminProfil
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: AdminProfil/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: AdminProfil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminProfil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,FullName,Job,Phone,Email,Password,Auth")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: AdminProfil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: AdminProfil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Admin admin, string password)
        {
            if (ModelState.IsValid)
            {
                var adm = db.Admins.Where(x => x.AdminId == id).SingleOrDefault();
                if (password != adm.Password)
                {
                    adm.Password = Crypto.Hash(password, "MD5");
                }
                adm.Phone = admin.Phone;
                adm.Job = admin.Job;
                adm.FullName = admin.FullName;
                adm.Email = admin.Email;
                adm.Auth = adm.Auth;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        public ActionResult PasswordPartial(Admin admin, int id)
        {
            var user = db.Admins.Where(x => x.AdminId == id).SingleOrDefault();

            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PasswordPartial(Admin admin, int id, string password)
        {
            if (ModelState.IsValid)
            {
                var adm = db.Admins.Where(x => x.AdminId == id).SingleOrDefault();

                        adm.Password = Crypto.Hash(password, "MD5");
                        adm.Phone = adm.Phone;
                        adm.Job = adm.Job;
                        adm.FullName = adm.FullName;
                        adm.Email = adm.Email;
                        adm.Auth = adm.Auth;

                    db.SaveChanges();
                    return RedirectToAction("Index", "AdminProfil/"); 
            }

            return View(admin);
        }

        // GET: AdminProfil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: AdminProfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
