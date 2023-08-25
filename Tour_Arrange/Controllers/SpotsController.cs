using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using Tour_Arrange.Models;
using Tour_Arrange.Models.ViewModels;

namespace Tour_Arrange.Controllers
{
    public class SpotsController : Controller
    {
        private readonly TourArrangeDbContext db = new TourArrangeDbContext();

        // GET: Spots
        public ActionResult Index()
        {
            var spots = db.Spots.ToList();
            return View(spots);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SpotVM spotVM, int[] spotId)
        {
            if (ModelState.IsValid)
            {
                Spot spot = new Spot
                {
                    SpotName = spotVM.SpotName
                };
                //For Image
                HttpPostedFileBase file = spotVM.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    spot.SpotPicture = filePath;
                }
                //foreach (var item in spotId)
                //{
                //    BookingEntry bookingEntry = new BookingEntry()
                //    {
                //        Spot = spot,
                //        SpotId = spot.SpotId,
                //        ClientId = item,
                //    };
                //    db.BookingEntries.Add(bookingEntry);
                //}
                db.Spots.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            Spot spot = db.Spots.First(x => x.SpotId == id);

            SpotVM spotVM = new SpotVM
            {
                SpotId = spot.SpotId,
                SpotName = spot.SpotName,
                SpotPicture = spot.SpotPicture
            };
            return View(spotVM);
        }
        [HttpPost]
        public ActionResult Edit(SpotVM spotVM, int[] spotId)
        {
            if (ModelState.IsValid)
            {
                Spot spot = new Spot()
                {
                    SpotId = spotVM.SpotId,
                    SpotName = spotVM.SpotName
                };
                //For Image
                HttpPostedFileBase file = spotVM.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    spot.SpotPicture = filePath;
                }
                else
                {
                    spot.SpotPicture = spotVM.SpotPicture;
                }
                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }
        // GET: Spots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spot spot = db.Spots.Find(id);
            db.Spots.Remove(spot);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}