using System.Data.Entity.ModelConfiguration;
using Nop.Plugin.Widgets.Employees.Domain;

namespace Nop.Plugin.Widgets.Employees.Data
{
    public partial class DepartmentRecordMap : EntityTypeConfiguration<DepartmentRecord>
    {
        public DepartmentRecordMap()
        {
            this.ToTable("StatusDepartment");
            this.HasKey(x => x.Id);
        }
    }
}