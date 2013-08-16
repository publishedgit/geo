using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoMVC.Models;
using GeoWPFCreateDbTest.Nhibernate;
using GeoWPFCreateDbTest.BL.Provider;

namespace GeoMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            //return View(NorthwindDataProvider.GetCustomers());	
            using (var data = new Provider())
            {
                return View(data.LocationRepository.GetAllLocation());
            }
        }
		
		public ActionResult GridViewPartialView() 
		{
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
			//return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
            using (var data = new Provider())
            {
                return PartialView("GridViewPartialView", data.LocationRepository.GetAllLocation());
            }
        }
    
	}
}