using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace JobPortalCaseStudyCF.Context.ModelConfig
{
    public class EmployerConfig : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder) 
        {
            builder.ToTable("Employer");

            builder.HasKey(e => e.EmployerId);

            builder.Property(e => e.EmployerId).HasColumnName("EmployerId").IsRequired();
            builder.Property(e => e.UserName).HasColumnName("Username").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(e => e.UserName).IsUnique();
            builder.Property(e => e.Password).HasColumnName("Password").HasColumnType("nvarchar(100)").IsRequired() ;
            builder.Property(e => e.Gender).HasColumnName("Gender").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasCheckConstraint("CHK_GENDERS", "Gender IN ('Male', 'Female')");
            builder.Property(e => e.EmployerName).HasColumnName("EmployerName").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(e => e.CompanyName).HasColumnName("CompanyName").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(e => e.Email).HasColumnName("Email").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.ContactPhone).HasColumnName("ContactPhone").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(e => e.ContactPhone).IsUnique();
            builder.Property(e => e.CwebsiteUrl).HasColumnName("CwebsiteUrl").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(e => e.Role).HasColumnName("Role").HasColumnType("nvarchar(50)").HasDefaultValue("Employer");
            builder.Property(e => e.Token).HasColumnName("Token").IsRequired(false);
            builder.Property(e => e.ResetPasswordExpires).HasColumnName("ResetPasswordExpires").IsRequired(false);

        }
    }
}
