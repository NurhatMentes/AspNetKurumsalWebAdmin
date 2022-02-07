using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class ServiceController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Service
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Service service, HttpPostedFileBase ImgUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImgUrl != null)//önceden resim varsa sil
                {
                    WebImage image = new WebImage(ImgUrl.InputStream);
                    FileInfo fileInfo = new FileInfo(ImgUrl.FileName);

                    string imgName = Guid.NewGuid() + fileInfo.Extension;
                    image.Resize(500, 500);
                    image.Save("~/Uploads/Service/" + imgName);

                    service.ImgUrl = "/Uploads/Service/" + imgName;
                }

                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Warning = "Güncellenecek hizmet bulunamadı.";
            }

            var service = db.Services.Find(id);

            if (service==null)
            {
                return HttpNotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Service service, HttpPostedFileBase ImgUrl)
        {
            if (ModelState.IsValid)
            {
                var serviceId = db.Services.Where(x => x.ServiceId == id).SingleOrDefault();
                if (ImgUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(serviceId.ImgUrl)))
                    {
                        System.IO.File.Delete((Server.MapPath((serviceId.ImgUrl))));
                    }

                    WebImage image = new WebImage(ImgUrl.InputStream);
                    FileInfo fileInfo = new FileInfo(ImgUrl.FileName);

                    string imgName = Guid.NewGuid() + fileInfo.Extension;
                    image.Resize(500, 500);
                    image.Save("~/Uploads/Service/" + imgName);

                    serviceId.ImgUrl = "/Uploads/Service/" + imgName;
                }

                serviceId.Description = service.Description;
                serviceId.Title = service.Title;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var serviceId = db.Services.Find(id);

            if (serviceId == null)
            {
                return HttpNotFound();
            }


            db.Services.Remove(serviceId);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}