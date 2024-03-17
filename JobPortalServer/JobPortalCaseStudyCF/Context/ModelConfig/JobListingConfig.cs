using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JobPortalCaseStudyCF.Context.ModelConfig
{
    public class JobListingConfig : IEntityTypeConfiguration<JobListing>
    {
        public void Configure(EntityTypeBuilder<JobListing> builder)
        {
            builder.ToTable("JobListing");

            builder.HasKey(l => l.ListingId);

            builder.Property(l => l.ListingId).HasColumnName("ListingId").IsRequired();
            builder.Property(l => l.EmployerId).HasColumnName("EmployerId").IsRequired();
            builder.Property(l => l.JobTitle).HasColumnName("JobTitle").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(l => l.JobDescription).HasColumnName("JobDescription").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(l => l.CompanyName).HasColumnName("CompanyName").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(l => l.HiringWorkflow).HasColumnName("HiringWorkflow").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(l => l.EligibilityCriteria).HasColumnName("EligibilityCriteria").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(l => l.RequiredSkills).HasColumnName("RequiredSkills").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(l => l.AboutCompany).HasColumnName("AboutCompany").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(l => l.Location).HasColumnName("Location").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(l => l.Salary).HasColumnName("Salary").HasColumnType("decimal(10, 2)").IsRequired();
            builder.HasCheckConstraint("CHK_Salary", "Salary > 0");
            builder.Property(l => l.PostedDate).HasColumnName("PostedDate").HasColumnType("datetime").IsRequired();
            builder.Property(l => l.Deadline).HasColumnName("Deadline").HasColumnType("datetime").IsRequired();
            builder.Property(l => l.VacancyOfJob).HasColumnName("VacancyOfJob").HasColumnType("bit").IsRequired();

            builder.HasOne(l => l.Employer)
            .WithMany(e => e.JobListings)
            .HasForeignKey(l => l.EmployerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_JobListing_Employer");

        }
    }
}
