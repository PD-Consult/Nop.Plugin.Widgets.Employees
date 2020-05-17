using Nop.Plugin.Widgets.Employees.Domain;
using Nop.Data.Mapping;

namespace Nop.Plugin.Widgets.Employees.Data
{
    public partial class DepartmentRecordMap : NopEntityTypeConfiguration<DepartmentRecord>
    {
        public DepartmentRecordMap()
        {
            this.ToTable("StatusDepartment");
            this.HasKey(x => x.Id);
        }
    }
}