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
    public class JobListingTests
    {
        private Mock<ILogger<JobListingController>> _loggerMock;
        private Mock<IRepository<JobListing>> _jobListingRepositoryMock;
        private JobListingServices _jobListingServices;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<JobListingController>>();
            _jobListingRepositoryMock = new Mock<IRepository<JobListing>>();
            _jobListingServices = new JobListingServices(_jobListingRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllJobListingsAsync()
        {
            // Arrange
            var listings = new List<JobListing> { new JobListing { EmployerId=1, JobTitle="awd", JobDescription="wda", CompanyName="wdafesg", HiringWorkflow="wa", EligibilityCriteria="wafe", RequiredSkills="wad", AboutCompany="wadfe", Location="wad", Salary=89000, PostedDate=DateTime.Today, Deadline=DateTime.Today} };
            _jobListingRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(listings);

            // Act
            var result = await _jobListingServices.GetAllJobListingsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(listings.Count));
        }

        [Test]
        public async Task GetJobListingByIdAsync()
        {
            // Arrange
            int listingId = 1;
            var resume = new JobListing { ListingId=listingId, EmployerId = 1, JobTitle = "awd", JobDescription = "wda", CompanyName = "wdafesg", HiringWorkflow = "wa", EligibilityCriteria = "wafe", RequiredSkills = "wad", AboutCompany = "wadfe", Location = "wad", Salary = 89000, PostedDate = DateTime.Today, Deadline = DateTime.Today };
            _jobListingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<JobListing, bool>>>(), false)).ReturnsAsync(resume);

            // Act
            var result = await _jobListingServices.GetJobListingByIdAsync(listingId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(listingId, result.ListingId);

        }

        [Test]
        public async Task CreateJobListingAsync()
        {
            // Arrange
            var validJobListing = new JobListing { ListingId = 1, EmployerId = 1, JobTitle = "awd", JobDescription = "wda", CompanyName = "wdafesg", HiringWorkflow = "wa", EligibilityCriteria = "wafe", RequiredSkills = "wad", AboutCompany = "wadfe", Location = "wad", Salary = 89000, PostedDate = DateTime.Today, Deadline = DateTime.Today };
            _jobListingRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<JobListing>())).ReturnsAsync(validJobListing);

            // Act
            var result = await _jobListingServices.CreateJobListingAsync(validJobListing);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(validJobListing, result);
        }

        [Test]
        public async Task UpdateJobListingAsync()
        {
            // Arrange
            var existingJobListing = new JobListing { ListingId = 1, EmployerId = 1, JobTitle = "awd", JobDescription = "wda", CompanyName = "wdafesg", HiringWorkflow = "wa", EligibilityCriteria = "wafe", RequiredSkills = "wad", AboutCompany = "wadfe", Location = "wad", Salary = 89000, PostedDate = DateTime.Today, Deadline = DateTime.Today };

            _jobListingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<JobListing, bool>>>(), true)).ReturnsAsync(existingJobListing);

            // Act
            var result = await _jobListingServices.UpdateJobListingAsync(existingJobListing);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteJobListingAsync()
        {
            // Arrange
            var existingJobListingId = 1;
            var existingJobListing = new JobListing { ListingId = existingJobListingId };
            _jobListingRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<JobListing, bool>>>(), false)).ReturnsAsync(existingJobListing);

            // Act
            var result = await _jobListingServices.DeleteJobListingAsync(existingJobListingId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
