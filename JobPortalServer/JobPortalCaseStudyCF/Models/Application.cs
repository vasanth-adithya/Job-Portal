using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalCaseStudyCF.Models
{
    public  class Application
    {
        public int ApplicationId { get; set; }
        public int ListingId { get; set; }
        public int JobSeekerId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; }
        public virtual JobSeeker? JobSeeker { get; set; }
        public virtual JobListing? Listing { get; set; }
    }
}
