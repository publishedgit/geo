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
    public class LocationController : CommonController
    {
        //
        // GET: /Location/

        public ActionResult Index()
        {
            using (var data = new Provider())
            {
                return View("Index", data.LocationRepository.GetAllLocation());
            }
        }

        public ActionResult EditModes()
        {
            using (var data = new Provider())
            {
                return View("EditModes",data.LocationRepository.GetAllLocation());
            }
            //return DemoView("EditModes", NorthwindDataProvider.GetEditableProducts());
        }
        [ValidateInput(false)]
        public ActionResult EditModesPartial()
        {
            using (var data = new Provider())
            {
                return PartialView("EditModesPartial", data.LocationRepository.GetAllLocation());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }
        public ActionResult ChangeEditModePartial(GridViewEditingMode editMode)
        {
            GridViewDemosHelper.EditMode = editMode;
            using (var data = new Provider())
            {
                return PartialView("EditModesPartial", data.LocationRepository.GetAllLocation());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditModesAddNewPartial(Location product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //NorthwindDataProvider.InsertProduct(product);
                    using (var data = new Provider())
                    {
                        data.LocationRepository.Add(product);
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
                return PartialView("EditModesPartial", data.LocationRepository.GetAllLocation());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditModesUpdatePartial(Location product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //NorthwindDataProvider.UpdateProduct(product);
                    using (var data = new Provider())
                    {
                        data.LocationRepository.Update(product);
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
                return PartialView("EditModesPartial", data.LocationRepository.GetAllLocation());
            }
            //return PartialView("EditModesPartial", NorthwindDataProvider.GetEditableProducts());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditModesDeletePartial(Location productID)
        {
            if (productID.Id >= 0)
            {
                try
                {
                    //NorthwindDataProvider.DeleteProduct(productID);
                    using (var data = new Provider())
                    {
                        var product = data.LocationRepository.GetLocationById(((Location)productID).Id);
                        data.LocationRepository.Delete(product);
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
                return PartialView("EditModesPartial", data.LocationRepository.GetAllLocation());
            }
        }

    }
}
