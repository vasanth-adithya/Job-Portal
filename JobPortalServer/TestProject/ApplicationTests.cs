using JobPortalCaseStudyCF.Controllers;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq.Expressions;

namespace TestProject
{
    [TestFixture]
    public class ApplicationTests
    {
        private Mock<ILogger<ApplicationController>> _loggerMock;
        private Mock<IRepository<Application>> _applicationRepositoryMock;
        private Mock<IRepository<JobListing>> _jobListingRepositoryMock;
        private ApplicationServices _applicationServices;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ApplicationController>>();
            _applicationRepositoryMock = new Mock<IRepository<Application>>();
            _jobListingRepositoryMock = new Mock<IRepository<JobListing>>();
            _applicationServices = new ApplicationServices(_applicationRepositoryMock.Object, _jobListingRepositoryMock.Object, _loggerMock.Object );
        }

        [Test]
        public async Task GetAllApplicationsAsync()
        {
            // Arrange
            var applications = new List<Application> { new Application { ListingId=1, JobSeekerId=1, ApplicationDate=DateTime.Today, ApplicationStatus="confirmed"} };
            _applicationRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(applications);

            // Act
            var result = await _applicationServices.GetAllApplicationsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(applications.Count));
        }

        [Test]
        public async Task GetApplicationByIdAsync()
        {
            // Arrange
            int applicationId = 1;
            var application = new Application { ApplicationId=applicationId, ListingId = 1, JobSeekerId = 1, ApplicationDate = DateTime.Today, ApplicationStatus = "confirmed" };
            _applicationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Application, bool>>>(), false)).ReturnsAsync(application);

            // Act
            var result = await _applicationServices.GetApplicationByIdAsync(applicationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(applicationId, result.ListingId);

        }

        [Test]
        public async Task CreateApplicationAsync()
        {
            // Arrange
            var validApplication = new Application { ApplicationId = 1, ListingId = 1, JobSeekerId = 1, ApplicationDate = DateTime.Today, ApplicationStatus = "confirmed" };
            _applicationRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Application>())).ReturnsAsync(validApplication);

            // Act
            var result = await _applicationServices.CreateApplicationAsync(validApplication);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(validApplication, result);
        }

        [Test]
        public async Task UpdateApplicationAsync()
        {
            // Arrange
            var existingApplication = new Application { ApplicationId = 1, ListingId = 1, JobSeekerId = 1, ApplicationDate = DateTime.Today, ApplicationStatus = "confirmed" };

            _applicationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Application, bool>>>(), true)).ReturnsAsync(existingApplication);

            // Act
            var result = await _applicationServices.UpdateApplicationAsync(existingApplication);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteApplicationAsync()
        {
            // Arrange
            var existingApplicationId = 1;
            var existingApplication = new Application { ApplicationId = existingApplicationId };
            _applicationRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Application, bool>>>(), false)).ReturnsAsync(existingApplication);

            // Act
            var result = await _applicationServices.DeleteApplicationAsync(existingApplicationId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
