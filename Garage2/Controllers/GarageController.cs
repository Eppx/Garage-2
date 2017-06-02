using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Data.Entity;
using Garage2.Models;
using System.Net;

namespace Garage2.Controllers
{
    public class GarageController : Controller
    {
        private GarageDB contextDB = new GarageDB();

        // GET: Vehicle
        public ActionResult Index(string searchString)
        {
            var garage = contextDB.Vehicles.OrderByDescending(v => v.RegNr)
                                            .Where(v => searchString == null || v.RegNr.Contains(searchString))
                                            .Take(5);
                                            
            return View(garage.ToList());
        }

        // GET: Vehicle/Details/5
        public ActionResult Details(int id)
        {
            Garage vehicle = contextDB.Vehicles.Find(id);
            if(vehicle != null)
            {
                return View("Details", vehicle);
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            Garage newVehicle = new Garage();
            return View("Create", newVehicle);
        }

        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Create(Garage newVehicle)
        {
            if (ModelState.IsValid)
            {
                newVehicle.Parkerad = DateTime.Now;
                contextDB.Vehicles.Add(newVehicle);
                contextDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garage delete = contextDB.Vehicles.Find(id);
            if (delete == null)
            {
                return HttpNotFound();
            }

            TimeSpan tidsskillnad = DateTime.Now.Subtract(contextDB.Vehicles.AsNoTracking().First(v => v.Id == delete.Id).Parkerad);
            delete.TotalParkedTime = Convert.ToInt32(tidsskillnad.TotalMinutes);
            delete.Price = Convert.ToInt32(tidsskillnad.TotalMinutes) * 0.75f;
            
            return View(delete);
        }

        // POST: Operas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
           Garage delete = contextDB.Vehicles.Find(id);
            contextDB.Vehicles.Remove(delete);
            contextDB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contextDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
