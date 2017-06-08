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
        public ActionResult Index(string searchString, string Selecttype)
        {
            //var garage = contextDB.Vehicles.OrderByDescending(v => v.RegNr)
            // .Where(v => searchString == null || v.RegNr.Contains(searchString));
            var garage = from v in contextDB.Vehicles
            select v;

            switch (Selecttype)
            {
                case "Färg":
                    //var Color = from u in contextDB.Vehicles
                    //            where u.Color == searchString
                    //            select u;
                    //return View(Color);
                    garage = garage.Where(v => v.Color == searchString);
                    break;
                case "RegNr":
                    //var RegNr = from u in contextDB.Vehicles
                    //            where u.RegNr.Contains(searchString)
                    //            select u;
                    //return View(RegNr);
                    garage = garage.Where(v => v.RegNr.Contains(searchString));
                    break;
                case "FordonsTyp":
                    //var Type = from u in contextDB.Vehicles
                    //           where u.Type.ToString() == searchString
                    //           select u;
                    //return View(Type);
                    garage = garage.Where(v => v.Type.ToString() == (searchString));
                    break;
                case "Märke":
                    //var Brand = from u in contextDB.Vehicles
                    //            where u.Brand == searchString
                    //            select u;
                    //return View(Brand);
                    garage = garage.Where(v => v.Brand == searchString);
                        break;
                case "Antalhjul":
                    //var NumberOfWheels = from u in contextDB.Vehicles
                    //                     where u.NumberOfWheels.ToString() == searchString
                    //                     select u;
                    //return View(NumberOfWheels);
                    garage = garage.Where(v => v.NumberOfWheels.ToString() == searchString);
                    break;

                case "Totaltantalhjul":
                    break;
                default:
                    break;
                    
            }           

            return View(garage.ToList());

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    garage = garage.Where(v => v.RegNr.Contains(searchString));
            //}

            //if (!string.IsNullOrEmpty(Selecttype))
            //{
            //    garage = garage.Where(v => v.Color == Selecttype);
            //}
        }
        
        // GET: Vehicle/Details/5
        public ActionResult Detaljvy(int id)
        {
            Garage vehicle = contextDB.Vehicles.Find(id);
            if(vehicle != null)
            {
                return View("Detaljvy", vehicle);
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // GET: Vehicle/Create
        public ActionResult Parkera()
        {
            Garage newVehicle = new Garage();
            return View("Parkera", newVehicle);
        }

        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Parkera(Garage newVehicle)
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
        public ActionResult Hamta_ut(int? id)
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
            delete.Price = Convert.ToInt32(tidsskillnad.TotalMinutes) * 1.25f;
            
            return View(delete);
        }

        // POST: Operas/Delete/5
        [HttpPost, ActionName("Hamta_ut")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Garage delete = contextDB.Vehicles.Find(id);
            var vKvitto = new Receipt() {  };
            contextDB.Vehicles.Remove(delete);
            contextDB.SaveChanges();
            return View("Kvitto");
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
