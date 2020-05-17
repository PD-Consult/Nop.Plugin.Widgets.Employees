using System.Data.Entity.ModelConfiguration;
using Nop.Plugin.Widgets.Employees.Domain;

namespace Nop.Plugin.Widgets.Employees.Data
{
    public partial class EmployeesRecordMap : EntityTypeConfiguration<EmployeesRecord>
    {
        public EmployeesRecordMap()
        {
            this.ToTable("StatusEmployee");
            this.HasKey(x => x.Id);
        }
    }
}