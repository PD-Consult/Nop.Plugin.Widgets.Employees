using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.Employees.Data;
using Nop.Plugin.Widgets.Employees.Domain;
using Nop.Plugin.Widgets.Employees.Services;

namespace Nop.Plugin.Widgets.Employees
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<EmployeesService>().As<IEmployeesService>().InstancePerLifetimeScope();

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                //register named context
                builder.Register<IDbContext>(c => new EmployeesObjectContext(dataProviderSettings.DataConnectionString))
                    .Named<IDbContext>("nop_object_context_employees_zip")
                    .InstancePerLifetimeScope();

                builder.Register<EmployeesObjectContext>(c => new EmployeesObjectContext(dataProviderSettings.DataConnectionString))
                    .InstancePerLifetimeScope();
            }
            else
            {
                //register named context
                builder.Register<IDbContext>(c => new EmployeesObjectContext(c.Resolve<DataSettings>().DataConnectionString))
                    .Named<IDbContext>("nop_object_context_employees_zip")
                    .InstancePerLifetimeScope();

                builder.Register<EmployeesObjectContext>(c => new EmployeesObjectContext(c.Resolve<DataSettings>().DataConnectionString))
                    .InstancePerLifetimeScope();
            }

            //override required repository with our custom context
            builder.RegisterType<EfRepository<EmployeesRecord>>()
                .As<IRepository<EmployeesRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_employees_zip"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<DepartmentRecord>>()
                .As<IRepository<DepartmentRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_employees_zip"))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
