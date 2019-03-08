namespace GazServiz.Data.EntityConfigurations
{
    using GazServiz.Models;
    using GazServiz.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> empoyee)
        {
            empoyee.HasKey("id").HasName("EmployeeId");

            //empoyee.HasBaseType<Person>();

            empoyee.Property(e => e.Name)
                   .IsRequired()
                   .IsUnicode()
                   .HasMaxLength(32);

            empoyee.Property(e => e.Speciality)
                   .HasDefaultValue(Speciality.Windows);

            var converter = new EnumToStringConverter<Speciality>(); 

            empoyee.Property(e => e.Speciality)
                   .HasConversion(converter); 
        }
    }
}