using JobPortalCaseStudyCF.Controllers;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace TestProject
{
    [TestFixture]
    public class JobSeekerTests
    {
        private Mock<ILogger<JobSeekerController>> _loggerMock;
        private Mock<IRepository<JobSeeker>> _jobSeekerRepositoryMock;
        private Mock<IAuthServices> _authServicesMock;
        private JobSeekerServices _jobSeekerServices;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<JobSeekerController>>();
            _jobSeekerRepositoryMock = new Mock<IRepository<JobSeeker>>();
            _authServicesMock = new Mock<IAuthServices>();
            _jobSeekerServices = new JobSeekerServices(_jobSeekerRepositoryMock.Object, _loggerMock.Object, _authServicesMock.Object);
        }

        [Test]
        public async Task GetAllJobSeekersAsync()
        {
            // Arrange
            var jobSeeker = new List<JobSeeker> { new JobSeeker {
                JobSeekerName = "wafe",
                UserName = "awdes",
                Email = "wadf@awd.com",
                Password = "wadfes",
                Gender = "female",
                ContactPhone = "1234568790",
                Address = "awdf",
                Description = "awf",
                DateOfBirth = new DateTime(2001, 12, 28),
                Qualification = "wadfe",
                Specialization = "waf",
                Institute = "waf",
                Year = 2019,
                CGPA = 8.9m,
                CompanyName = "wdafe",
                Position = "wafes",
                Responsibilities = "wadf",
                StartDate = new DateTime(2023, 01, 01),
                EndDate = new DateTime(2024, 02, 01)
            } };
            new JobSeeker();
            _jobSeekerRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jobSeeker);

            // Act
            var result = await _jobSeekerServices.GetAllJobSeekersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(jobSeeker.Count));
        }

        [Test]
        public async Task GetJobSeekerByIdAsync()
        {
            // Arrange
            int jobSeekerId = 1;
            var jobSeeker = new JobSeeker
            {
                JobSeekerId = jobSeekerId,
                JobSeekerName = "wafe",
                UserName = "awdes",
                Email = "wadf@awd.com",
                Password = "wadfes",
                Gender = "female",
                ContactPhone = "1234568790",
                Address = "awdf",
                Description = "awf",
                DateOfBirth = new DateTime(2001, 12, 28),
                Qualification = "wadfe",
                Specialization = "waf",
                Institute = "waf",
                Year = 2019,
                CGPA = 8.9m,
                CompanyName = "wdafe",
                Position = "wafes",
                Responsibilities = "wadf",
                StartDate = new DateTime(2023, 01, 01),
                EndDate = new DateTime(2024, 02, 01)
            };
            _jobSeekerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<JobSeeker, bool>>>(), false)).ReturnsAsync(jobSeeker);

            // Act
            var result = await _jobSeekerServices.GetJobSeekerByIdAsync(jobSeekerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(jobSeekerId, result.JobSeekerId);

        }

        [Test]
        public async Task CreateJobSeekerAsync()
        {
            // Arrange
            var validJobSeeker = new JobSeeker
            {
                JobSeekerId = 1,
                JobSeekerName = "wafe",
                UserName = "awdes",
                Email = "wadf@awd.com",
                Password = "wadfes",
                Gender = "female",
                ContactPhone = "1234568790",
                Address = "awdf",
                Description = "awf",
                DateOfBirth = new DateTime(2001, 12, 28),
                Qualification = "wadfe",
                Specialization = "waf",
                Institute = "waf",
                Year = 2019,
                CGPA = 8.9m,
                CompanyName = "wdafe",
                Position = "wafes",
                Responsibilities = "wadf",
                StartDate = new DateTime(2023, 01, 01),
                EndDate = new DateTime(2024, 02, 01)
            };
            _jobSeekerRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<JobSeeker>())).ReturnsAsync(validJobSeeker);

            // Act
            var result = await _jobSeekerServices.CreateJobSeekerAsync(validJobSeeker);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(validJobSeeker, result);
        }

        [Test]
        public async Task UpdateJobSeekerAsync()
        {
            // Arrange
            var existingJobSeeker = new JobSeeker
            {
                JobSeekerId = 1,
                JobSeekerName = "wafe",
                UserName = "awdes",
                Email = "wadf@awd.com",
                Password = "wadfes",
                Gender = "female",
                ContactPhone = "1234568790",
                Address = "awdf",
                Description = "awf",
                DateOfBirth = new DateTime(2001, 12, 28),
                Qualification = "wadfe",
                Specialization = "waf",
                Institute = "waf",
                Year = 2019,
                CGPA = 8.9m,
                CompanyName = "wdafe",
                Position = "wafes",
                Responsibilities = "wadf",
                StartDate = new DateTime(2023, 01, 01),
                EndDate = new DateTime(2024, 02, 01)
            };

            _jobSeekerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<JobSeeker, bool>>>(), true)).ReturnsAsync(existingJobSeeker);

            // Act
            var result = await _jobSeekerServices.UpdateJobSeekerAsync(existingJobSeeker,true);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteJobSeekerAsync()
        {
            // Arrange
            var existingJobSeekerId = 1;
            var existingJobSeeker = new JobSeeker { JobSeekerId = existingJobSeekerId };
            _jobSeekerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<JobSeeker, bool>>>(), false)).ReturnsAsync(existingJobSeeker);

            // Act
            var result = await _jobSeekerServices.DeleteJobSeekerAsync(existingJobSeekerId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
