using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalCaseStudyCF.Models
{
    public  class Resume
    {
        public int ResumeId { get; set; }
        public int JobSeekerId { get; set; }
        public string ResumeUrl { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Status { get; set; }

        public virtual JobSeeker? JobSeeker { get; set; }
    }
}
