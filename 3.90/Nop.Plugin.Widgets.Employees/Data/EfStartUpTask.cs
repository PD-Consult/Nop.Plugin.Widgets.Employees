using System.Data.Entity;
using AutoMapper;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.Employees.Domain;
using Nop.Plugin.Widgets.Employees.Models;

namespace Nop.Plugin.Widgets.Employees.Data
{
    public class EfStartUpTask : IStartupTask
    {
        public void Execute()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<EmployeesObjectContext>(null);

            //Mapper.CreateMap<EmployeeModel, EmployeesRecord>();
            //Mapper.CreateMap<EmployeesRecord, EmployeeModel>();
            //Mapper.CreateMap<DepartmentModel, DepartmentRecord>();
            //Mapper.CreateMap<DepartmentRecord, DepartmentModel>();
        }

        public int Order
        {
            //ensure that this task is run first 
            get { return 0; }
        }
    }
}
