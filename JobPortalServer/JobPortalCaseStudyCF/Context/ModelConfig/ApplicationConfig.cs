using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JobPortalCaseStudyCF.Context.ModelConfig
{
    public class ApplicationConfig : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Application");

            builder.HasKey(a => a.ApplicationId);

            builder.Property(a => a.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            builder.Property(a => a.ListingId).HasColumnName("ListingId").IsRequired();
            builder.Property(a => a.JobSeekerId).HasColumnName("JobSeekerId").IsRequired();
            builder.Property(a => a.ApplicationDate).HasColumnName("ApplicationDate").HasColumnType("datetime").IsRequired();
            builder.Property(a => a.ApplicationStatus).HasColumnName("ApplicationStatus").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasCheckConstraint("CHK_Status", "ApplicationStatus IN ('Confirmed', 'Pending','Cancelled')");


            builder.HasOne(a => a.Listing)
                .WithMany(l => l.Applications)
                .HasForeignKey(a => a.ListingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Application_JobListing");

            builder.HasOne(a => a.JobSeeker)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Application_JobSeeker");



        }
    }
}
