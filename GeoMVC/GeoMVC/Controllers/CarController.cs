using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GeoMVC.Models;
using GeoWPFCreateDbTest.Nhibernate;
using GeoWPFCreateDbTest.BL.Provider;
using GeoWPFCreateDbTest.Nhibernate.Model;

namespace GeoMVC.Controllers
{
    public class CarController : Controller
    {
        //
        // GET: /Car/

        public ActionResult Index()
        {
            using (var data = new Provider())
            {
                return View("Index", data.CarRepository.GetAllCar());
            }
        }

        public ActionResult EditModes()
        {
            using (var data = new Provider())
            {
                return View("EditModes", data.CarRepository.GetAllCar());
            }
            //return DemoView("EditModes", NorthwindDataProvider.GetEditableProducts());
        }
        [ValidateInput(false)]
        public ActionResult EditCarPartial()
        {
            using (var data = new Provider())
            {
                return PartialView("EditCarPartial", data.CarRepository.GetAllCar());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }
        public ActionResult ChangeEditModePartial(GridViewEditingMode editMode)
        {
            GridViewDemosHelper.EditMode = editMode;
            using (var data = new Provider())
            {
                return PartialView("EditCarPartial", data.CarRepository.GetAllCar());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult EditModesAddNewPartial(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //NorthwindDataProvider.InsertProduct(product);
                    using (var data = new Provider())
                    {
                        car.Location = data.LocationRepository.GetLocationById(car.Location.Id);
                        data.CarRepository.Add(car);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            using (var data = new Provider())
            {
                return PartialView("EditCarPartial", data.CarRepository.GetAllCar());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditModesUpdatePartial(Car car)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    //NorthwindDataProvider.UpdateProduct(product);
                    using (var data = new Provider())
                    {
                        var location = data.LocationRepository.GetLocationById(car.Location.Id);
                        car.Location = location;
                        data.CarRepository.Update(car);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            using (var data = new Provider())
            {
                return PartialView("EditCarPartial", data.CarRepository.GetAllCar());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditModesDeletePartial(Car car)
        {
            if (car.Id >= 0)
            {
                try
                {
                    //NorthwindDataProvider.DeleteProduct(productID);
                    using (var data = new Provider())
                    {
                        data.CarRepository.DeleteCarById(car.Id);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
            using (var data = new Provider())
            {
                return PartialView("EditCarPartial", data.CarRepository.GetAllCar());
            }
        }

        public ActionResult ListLocationsForCars()
        {
            using(var data = new Provider())
            {
                return View("ListLocationsForCars", data.LocationRepository.GetAllLocation());
            }
        }

        public ActionResult AddCar(int locationId)
        {
            var c = new CarModel();
            
            //c.Location = data.LocationRepository.GetLocationById(locationId);
            c.LocationId = locationId;
            c.ProductionTime = DateTime.Now;
            

            return View("AddCar", c);
        }

        public ActionResult AddCarSave(CarModel car)
        {
            var c = new Car();
            
            c.Made = car.Made;
            c.NumberOfOwners = car.NumberOfOwners;
            c.ProductionTime = DateTime.Now;
            c.Type = car.Type;
            c.Condition = car.Condition;

            using (var data = new Provider())
            {

                c.Location = data.LocationRepository.GetLocationById( car.LocationId );
                data.CarRepository.Add(c);
                return View("ListLocationsForCars", data.LocationRepository.GetAllLocation());
            }
        }

    }
}
