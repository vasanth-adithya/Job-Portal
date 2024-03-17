using JobPortalCaseStudyCF.Context.ModelConfig;
using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortalCaseStudyCF.Context
{
    public class JobPortalCFContext : DbContext
    {
        public JobPortalCFContext(DbContextOptions<JobPortalCFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employer> Employers { get; set; } = null!;
        public virtual DbSet<JobSeeker> JobSeekers { get; set; } = null!;
        public virtual DbSet<JobListing> JobListings { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<Resume> Resumes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployerConfig());
            modelBuilder.ApplyConfiguration(new JobSeekerConfig());
            modelBuilder.ApplyConfiguration(new JobListingConfig());
            modelBuilder.ApplyConfiguration(new ApplicationConfig());
            modelBuilder.ApplyConfiguration(new ResumeConfig());

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
    }
}
