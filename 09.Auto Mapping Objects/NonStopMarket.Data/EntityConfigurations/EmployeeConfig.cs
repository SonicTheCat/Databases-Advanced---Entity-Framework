namespace NonStopMarket.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using NonStopMarket.Models;

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.Manager)
                   .WithMany(m => m.ManagerEmployees)
                   .HasForeignKey(e => e.ManagerId); 
        }
    }
}