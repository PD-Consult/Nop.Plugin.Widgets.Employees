using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.Employees.Models
{
    [Table("StatusEmployee")]
    public class EmployeeModel : BaseNopEntityModel
    {
        public EmployeeModel()
        {
            AvailableDepartments = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Department")]
        public int DepartmentId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Title")]
        public string Title { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.PictureId")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Info")]
        [AllowHtml]
        public string Info { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Specialties")]
        [AllowHtml]
        public string Specialties { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Interests")]
        [AllowHtml]
        public string Interests { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.PhoneNumber")]
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.MobileNumber")]
        public string MobileNumber { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.WorkStarted")]
        public DateTime WorkStarted { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.WorkEnded")]
        public DateTime WorkEnded { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Employees.Fields.Deleted")]
        public bool Deleted { get; set; }

        public IList<SelectListItem> AvailableDepartments { get; set; }

        public string PhotoUrl { get; set; }
        public string DepartmentName { get; set; }
    }
}