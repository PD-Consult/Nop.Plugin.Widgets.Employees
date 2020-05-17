using System;
using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.Employees.Domain;

namespace Nop.Plugin.Widgets.Employees.Models
{
    /// <summary>
    /// AutoMapper configuration for Employees models
    /// </summary>
    public class EmployeesMapperConfiguration : IMapperConfiguration
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns>Mapper configuration action</returns>
        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            //TODO remove 'CreatedOnUtc' ignore mappings because now presentation layer models have 'CreatedOn' property and core entities have 'CreatedOnUtc' property (distinct names)

            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<EmployeeModel, EmployeesRecord>()
                     .ForMember(dest => dest.Deleted, mo => mo.Ignore());

                cfg.CreateMap<EmployeesRecord, EmployeeModel>()
                    .ForMember(dest => dest.AvailableDepartments, mo => mo.Ignore())
                    .ForMember(dest => dest.PhotoUrl, mo => mo.Ignore())
                    .ForMember(dest => dest.DepartmentName, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());

                //countries
                cfg.CreateMap<DepartmentModel, DepartmentRecord>();
                cfg.CreateMap<DepartmentRecord, DepartmentModel>()
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            };
            return action;
        }

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}