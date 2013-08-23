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
        public ActionResult EditModesAddNewPartial(Car product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //NorthwindDataProvider.InsertProduct(product);
                    using (var data = new Provider())
                    {
                        data.CarRepository.Add(product);
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
        public ActionResult EditModesDeletePartial(int productID)
        {
            if (productID >= 0)
            {
                try
                {
                    //NorthwindDataProvider.DeleteProduct(productID);
                    using (var data = new Provider())
                    {
                        data.CarRepository.DeleteCarById(productID);
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

        public ActionResult AddCar(Location locationId)
        {
            var c = new Car();
            using (var data = new Provider())
            {
                //c.Location = data.LocationRepository.GetLocationById(locationId);
                c.Location = locationId;
                c.ProductionTime = DateTime.Now;
                data.CarRepository.Add(c);
            }
            

            return View("AddCar", c);
        }

        public void UpdateCarToSave(Car car)
        {
            var c = new Car();
            using (var data = new Provider())
            {
                data.CarRepository.Update(car);
            }
            ListLocationsForCars();
        }
    }
}
