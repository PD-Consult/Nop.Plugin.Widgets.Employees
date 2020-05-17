using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Widgets.Employees
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapRoute("Plugin.Widgets.Employees.SaveGeneralSettings",
            //     "Plugins/Employees/SaveGeneralSettings",
            //     new { controller = "Employees", action = "SaveGeneralSettings", },
            //     new[] { "Nop.Plugin.Widgets.Employees.Controllers" }
            //);

            //routes.MapRoute("Plugin.Widgets.Employees.AddPopup",
            //     "Plugins/Employees/AddPopup",
            //     new { controller = "Employees", action = "AddPopup" },
            //     new[] { "Nop.Plugin.Widgets.Employees.Controllers" }
            //);
            //routes.MapRoute("Plugin.Widgets.Employees.EditPopup",
            //     "Plugins/Employees/EditPopup",
            //     new { controller = "Employees", action = "EditPopup" },
            //     new[] { "Nop.Plugin.Widgets.Employees.Controllers" }
            //);
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
