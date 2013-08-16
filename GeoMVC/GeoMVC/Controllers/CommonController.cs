using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.ASPxGridView;
using System.Text.RegularExpressions;
using DevExpress.Web.Mvc;
using System.Data.Linq;
using GeoMVC.Models;
using System.Web.UI.WebControls;
using GeoWPFCreateDbTest.BL.Provider;

namespace GeoMVC.Controllers
{
    public partial class CommonController : Controller
    {
        //
        // GET: /Common/

        public string Name { get { return "GridView"; } }

        //public ActionResult Index() {
        //    using (var data = new Provider())
        //    {
        //        return View(data.LocationRepository.GetAllLocation());
        //    }
        //}

    }

    public delegate string TweetsDemoReplaceDelegate(string text, Match match);
    public class TweetsDemoReplaceItem {
        public Regex RegEx { get; set; }
        public TweetsDemoReplaceDelegate ReplaceDelegate { get; set; }
    }

    public delegate ActionResult ExportMethod(GridViewSettings settings, object dataObject);
    public class ExportType {
        public string Title { get; set; }
        public ExportMethod Method { get; set; }
    }
    public class GridViewDemosHelper {
        public const string
            ImageQueryKey = "DXImage",
            PageSizeSessionKey = "ed5e843d-cff7-47a7-815e-832923f7fb09",
            EditingModeSessionKey = "AA054892-1B4C-4158-96F7-894E1545C05C";

        public static int PageSize {
            get {
                if(HttpContext.Current.Session[PageSizeSessionKey] == null)
                    return 2;
                return (int)HttpContext.Current.Session[PageSizeSessionKey];
            }
            set { HttpContext.Current.Session[PageSizeSessionKey] = value; }
        }

        public static GridViewEditingMode EditMode {
            get {
                if(HttpContext.Current.Session[EditingModeSessionKey] == null)
                    HttpContext.Current.Session[EditingModeSessionKey] = GridViewEditingMode.EditFormAndDisplayRow;
                return (GridViewEditingMode)HttpContext.Current.Session[EditingModeSessionKey];
            }
            set { HttpContext.Current.Session[EditingModeSessionKey] = value; }
        }

        static Dictionary<string, ExportType> exportTypes;
        public static Dictionary<string, ExportType> ExportTypes {
            get {
                if(exportTypes == null)
                    exportTypes = CreateExportTypes();
                return exportTypes;
            }
        }
        static Dictionary<string, ExportType> CreateExportTypes() {
            Dictionary<string, ExportType> types = new Dictionary<string, ExportType>();
            types.Add("PDF", new ExportType { Title = "Export to PDF", Method = GridViewExtension.ExportToPdf });
            types.Add("XLS", new ExportType { Title = "Export to XLS", Method = GridViewExtension.ExportToXls });
            types.Add("XLSX", new ExportType { Title = "Export to XLSX", Method = GridViewExtension.ExportToXlsx });
            types.Add("RTF", new ExportType { Title = "Export to RTF", Method = GridViewExtension.ExportToRtf });
            types.Add("CSV", new ExportType { Title = "Export to CSV", Method = GridViewExtension.ExportToCsv });
            return types;
        }

        static GridViewSettings exportGridViewSettings;
        public static GridViewSettings ExportGridViewSettings {
            get {
                if(exportGridViewSettings == null)
                    exportGridViewSettings = CreateExportGridViewSettings();
                return exportGridViewSettings;
            }
        }
        static GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "gvExport";
            settings.CallbackRouteValues = new { Controller = "GridView", Action = "ExportPartial" };
            settings.Width = Unit.Percentage(100);

            settings.Columns.Add("ZipCode");
            settings.Columns.Add("Address");
            settings.Columns.Add("ParkingLot");

            return settings;
        }
    }

    public enum GridViewExportType { None, Pdf, Xls, Xlsx, Rtf, Csv }
}
