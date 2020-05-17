using AutoMapper;
using Nop.Plugin.Widgets.Employees.Domain;

namespace Nop.Plugin.Widgets.Employees.Models
{
    public static class ModelExtender
    {
        public static EmployeeModel ToModel(this EmployeesRecord entity)
        {
            return Mapper.Map<EmployeesRecord, EmployeeModel>(entity);
        }

        public static EmployeesRecord ToEntity(this EmployeeModel model)
        {
            return Mapper.Map<EmployeeModel, EmployeesRecord>(model);
        }

        public static EmployeesRecord ToEntity(this EmployeeModel model, EmployeesRecord destination)
        {
            return Mapper.Map(model, destination);
        }

        public static DepartmentModel ToModel(this DepartmentRecord entity)
        {
            return Mapper.Map<DepartmentRecord, DepartmentModel>(entity);
        }

        public static DepartmentRecord ToEntity(this DepartmentModel model)
        {
            return Mapper.Map<DepartmentModel, DepartmentRecord>(model);
        }

        public static DepartmentRecord ToEntity(this DepartmentModel model, DepartmentRecord destination)
        {
            return Mapper.Map(model, destination);
        }
    }
}
