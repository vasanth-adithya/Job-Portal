using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Context.ModelConfig
{
    public class ResumeConfig : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {

            builder.ToTable("Resume");

            builder.HasKey(r => r.ResumeId);

            builder.Property(r => r.ResumeId).HasColumnName("ResumeId").IsRequired();
            builder.Property(r => r.JobSeekerId).HasColumnName("JobSeekerId").IsRequired();
            builder.Property(r => r.ResumeUrl).HasColumnName("ResumeUrl").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(r => r.UploadedDate).HasColumnName("UploadedDate").HasColumnType("datetime").IsRequired();
            builder.Property(r => r.Status).HasColumnName("Status").HasColumnType("nvarchar(100)").IsRequired();
            builder.HasCheckConstraint("CHK_Status", "Status IN ('Active', 'Inactive')");

            builder.HasOne(r => r.JobSeeker)
            .WithMany(j => j.Resumes)
            .HasForeignKey(r => r.JobSeekerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Resume_JobSeeker");
        }
    }
}



