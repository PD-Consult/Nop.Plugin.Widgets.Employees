using Nop.Plugin.Widgets.Employees.Domain;
using Nop.Data.Mapping;

namespace Nop.Plugin.Widgets.Employees.Data
{
    public partial class EmployeesRecordMap : NopEntityTypeConfiguration<EmployeesRecord>
    {
        public EmployeesRecordMap()
        {
            this.ToTable("StatusEmployee");
            this.HasKey(x => x.Id);
        }
    }
}