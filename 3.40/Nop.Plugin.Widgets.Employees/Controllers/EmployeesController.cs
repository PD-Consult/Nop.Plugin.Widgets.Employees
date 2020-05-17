using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Plugin.Widgets.Employees.Domain;
using Nop.Plugin.Widgets.Employees.Models;
using Nop.Plugin.Widgets.Employees.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Core;
using Nop.Services.Media;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.Employees.Controllers
{
    //[AdminAuthorize]
    public class EmployeesController : Controller
    {
        private readonly EmployeesSettings _employeeSettings;
        private readonly IEmployeesService _employeeService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;


        public EmployeesController(EmployeesSettings employeeSettings,
            IEmployeesService employeeService, ISettingService settingService,
            ILocalizationService localizationService, IPermissionService permissionService, IWorkContext workContext, IPictureService pictureService)
        {
            this._employeeSettings = employeeSettings;
            this._employeeService = employeeService;
            this._settingService = settingService;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._workContext = workContext;
            this._pictureService = pictureService;
        }
        
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            CommonHelper.SetTelerikCulture();

            base.Initialize(requestContext);
        }

        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new EmployeeModel();
            //other settings

            return View("~/Plugins/Widgets.Employees/Views/Employees/Configure.cshtm", model);
        }

        //[HttpPost]
        //public ActionResult SaveGeneralSettings(EmployeesListModel model)
        //{
        //    //save settings
        //    _employeeSettings.LimitMethodsToCreated = model.LimitMethodsToCreated;
        //    _settingService.SaveSetting(_employeeSettings);

        //    return Json(new { Result = true });
        //}

        [HttpPost]
        public ActionResult EmployeeList(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return Content("Access denied");

            var records = _employeeService.GetAll(command.Page - 1, command.PageSize);
            var sbwModel = records.Select(x =>
                {
                    return x.ToModel();
                })
                .ToList();
            var model = new DataSourceResult
            {
                Data = sbwModel,
                Total = records.TotalCount
            };

            return Json(model);
        }

        [HttpPost]
        public ActionResult DepartmentList(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageLanguages))
                return AccessDeniedView();

            var records = _employeeService.GetAllDepartments();
            var sbwModel = records.Select(x => x.ToModel()).ToList();
            var model = new DataSourceResult
            {
                Data = sbwModel,
                Total = records.Count
            };

            return Json(model);
        }

        public ActionResult EmployeeInfo(int id)
        {
            var model = new EmployeeInfoModel();
            model.IsAdmin = _permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel);
            var e = _employeeService.GetById(id);
            model.Employee = e.ToModel();
            if (e.PictureId > 0)
                model.Employee.PhotoUrl = _pictureService.GetPictureUrl(e.PictureId, 200);
            else
                model.Employee.PhotoUrl = null;
            var department = _employeeService.GetDepartmentByDepartmentId(e.DepartmentId);
            if (department != null)
            {
                model.Employee.DepartmentName = department.Name;
            }
            return View("~/Plugins/Widgets.Employees/Views/Employees/EmployeeInfo.cshtml", model);

        }

        [HttpPost]
        public ActionResult EmployeeDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return Content("Access denied");

            var sbw = _employeeService.GetById(id);
            if (sbw != null)
                _employeeService.DeleteEmployeesRecord(sbw);

            return new NullJsonResult();
        }

        //add
        public ActionResult AddPopup()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return Content("Access denied");

            var model = new EmployeeModel();

            var departments = _employeeService.GetAllDepartments();
            foreach (var c in departments)
                model.AvailableDepartments.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == model.DepartmentId) });

            return View("~/Plugins/Widgets.Employees/Views/Employees/AddPopup.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddPopup(string btnId, string formId, EmployeeModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return Content("Access denied");

            var employee = model.ToEntity();
            _employeeService.InsertEmployeesRecord(employee);

            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;
            return View("~/Plugins/Widgets.Employees/Views/Employees/AddPopup.cshtml", model);
        }

        //edit
        public ActionResult EditPopup(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return Content("Access denied");

            var sbw = _employeeService.GetById(id);
            if (sbw == null)
                //No record found with the specified id
                return RedirectToAction("Configure");

            var model = sbw.ToModel();
            
            var departments = _employeeService.GetAllDepartments();
            foreach (var c in departments)
                model.AvailableDepartments.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == model.DepartmentId) });

            return View("~/Plugins/Widgets.Employees/Views/Employees/EditPopup.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditPopup(string btnId, string formId, EmployeeModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return Content("Access denied");

            var employee = _employeeService.GetById(model.Id);
            if (employee == null)
                //No record found with the specified id
                return RedirectToAction("Configure");

            employee = model.ToEntity();
            _employeeService.UpdateEmployeesRecord(employee);

            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;
            return View("~/Plugins/Widgets.Employees/Views/Employees/EditPopup.cshtml", model);
        }

        public ActionResult PublicInfo()
        {
            return View("~/Plugins/Widgets.Employees/Views/Employees/PublicInfo.cshtml");
        }

        public ActionResult List()
        {
            var model = new DepartmentEmployeeModel
            {
                IsAdmin = _permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel)
            };
            var departments = _employeeService.GetAllDepartments();
            foreach (var d in departments)
            {
                var employeesModel = new EmployeesListModel {DepartmentName = d.Name};
                var employees = _employeeService.GetEmployeeByDepartmentId(d.Id);
                foreach (var e in employees)
                {
                    employeesModel.Employees.Add(e.ToModel());
                }
                model.EmployeesList.Add(employeesModel);
            }

            return View("~/Plugins/Widgets.Employees/Views/Employees/List.cshtml", model);
        }

        #region admin area

        public ActionResult CreateEmployee()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var model = new EmployeeModel();
            var departments = _employeeService.GetAllDepartments();
            foreach (var c in departments)
                model.AvailableDepartments.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == model.DepartmentId) });
            return View("~/Plugins/Widgets.Employees/Views/Employees/CreateEmployee.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult CreateEmployee(EmployeeModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var employee = model.ToEntity();

                _employeeService.InsertEmployeesRecord(employee);
                return continueEditing ? RedirectToAction("EditEmployee", new { id = employee.Id }) : RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            var departments = _employeeService.GetAllDepartments();
            foreach (var c in departments)
                model.AvailableDepartments.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == model.DepartmentId) });
            return View("~/Plugins/Widgets.Employees/Views/Employees/CreateEmployee.cshtml", model);
        }

        public ActionResult EditEmployee(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var employee = _employeeService.GetById(id);
            var model = employee.ToModel();
            var departments = _employeeService.GetAllDepartments();
            foreach (var c in departments)
                model.AvailableDepartments.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == model.DepartmentId) });
            return View("~/Plugins/Widgets.Employees/Views/Employees/EditEmployee.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult EditEmployee(EmployeeModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee != null)
                {
                    employee = model.ToEntity(employee);
                }

                _employeeService.UpdateEmployeesRecord(employee);
                return continueEditing ? RedirectToAction("EditEmployee", new { id = employee.Id }) : RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            var departments = _employeeService.GetAllDepartments();
            foreach (var c in departments)
                model.AvailableDepartments.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == model.DepartmentId) });
            return View("~/Plugins/Widgets.Employees/Views/Employees/EditEmployee.cshtml", model);
        }

        public ActionResult CreateDepartment()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var model = new DepartmentModel();
            return View("~/Plugins/Widgets.Employees/Views/Employees/CreateDepartment.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult CreateDepartment(DepartmentModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var department = model.ToEntity();
                _employeeService.InsertDepartmentRecord(department);
                return continueEditing ? RedirectToAction("EditDepartment", new { id = department.Id }) : RedirectToAction("DepartmentList");
            }

            //If we got this far, something failed, redisplay form
            return View("~/Plugins/Widgets.Employees/Views/Employees/CreateDepartment.cshtml", model);
        }

        public ActionResult EditDepartment(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var department = _employeeService.GetDepartmentByDepartmentId(id);
            var model = department.ToModel();
            return View("~/Plugins/Widgets.Employees/Views/Employees/EditDepartment.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult EditDepartment(DepartmentModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var department = _employeeService.GetDepartmentByDepartmentId(model.Id);
                if(department != null)
                {
                    department = model.ToEntity(department);
                }

                _employeeService.UpdateDepartmentRecord(department);
                return continueEditing ? RedirectToAction("EditDepartment", new { id = department.Id }) : RedirectToAction("DepartmentList");
            }

            //If we got this far, something failed, redisplay form
            return View("~/Plugins/Widgets.Employees/Views/Employees/CreateDepartment.cshtml", model);
        }


        public ActionResult DepartmentList()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var model = new List<DepartmentModel>();
            var departments = _employeeService.GetAllDepartments(true);
            foreach (var item in departments)
            {
                model.Add(item.ToModel());
            }

            return View("~/Plugins/Widgets.Employees/Views/Employees/DepartmentList.cshtml", model);
        }
        #endregion

        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected ActionResult AccessDeniedView()
        {
            //return new HttpUnauthorizedResult();
            //return RedirectToAction("AccessDenied", "Security", new { pageUrl = this.Request.RawUrl });
            return Content("Access denied");
        }
    }
}
