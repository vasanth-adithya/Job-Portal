using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JobPortalCaseStudyCF.Context.ModelConfig
{
    public class JobSeekerConfig : IEntityTypeConfiguration<JobSeeker>
    {
        public void Configure(EntityTypeBuilder<JobSeeker> builder)
        {
            builder.ToTable("JobSeeker");

            builder.HasKey(j => j.JobSeekerId);

            builder.Property(j => j.JobSeekerId).HasColumnName("JobSeekerId").IsRequired();
            builder.Property(j => j.UserName).HasColumnName("Username").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(j => j.UserName).IsUnique();
            builder.Property(j => j.Password).HasColumnName("Password").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.JobSeekerName).HasColumnName("JobSeekerName").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Description).HasColumnName("Description").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Email).HasColumnName("ContactEmail").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(j => j.Email).IsUnique();
            builder.Property(j => j.ContactPhone).HasColumnName("ContactPhone").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(j => j.ContactPhone).IsUnique();
            builder.Property(j => j.DateOfBirth).HasColumnName("DateOfBirth").HasColumnType("datetime").IsRequired();
            builder.Property(j => j.Gender).HasColumnName("Gender").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasCheckConstraint("CHK_GENDER", "Gender IN ('Male', 'Female')");
            builder.Property(j => j.Address).HasColumnName("Address").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Qualification).HasColumnName("Qualification").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Specialization).HasColumnName("Specialization").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Institute).HasColumnName("Institute").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Year).HasColumnName("Year").HasColumnType("int").IsRequired();
            builder.HasCheckConstraint("CHK_Year", "Year >= 1950 AND Year <= 2024");
            builder.Property(j => j.CGPA).HasColumnName("CGPA").HasColumnType("decimal(4,2)").IsRequired();
            builder.HasCheckConstraint("CHK_CGPA", "CGPA >= 0.0 AND CGPA <= 10.0");
            builder.Property(j => j.CompanyName).HasColumnName("CompanyName").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Position).HasColumnName("Position").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.Responsibilities).HasColumnName("Responsibilities").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(j => j.StartDate).HasColumnName("StartDate").HasColumnType("datetime").IsRequired();
            builder.Property(j => j.EndDate).HasColumnName("EndDate").HasColumnType("datetime").IsRequired();
            builder.Property(j => j.Role).HasColumnName("Role").HasColumnType("nvarchar(50)").HasDefaultValue("JobSeeker");
            builder.Property(j => j.Token).HasColumnName("Token").IsRequired(false);
            builder.Property(j => j.ResetPasswordExpires).HasColumnName("ResetPasswordExpires").IsRequired(false);

        }
    }
}
