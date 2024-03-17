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
    public class EmployerTests
    {
        private Mock<ILogger<EmployerController>> _loggerMock;
        private Mock<IRepository<Employer>> _employerRepositoryMock;
        private Mock<IAuthServices> _authSevicesMock;
        private EmployerServices _employerServices;
        
        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<EmployerController>>();
            _employerRepositoryMock = new Mock<IRepository<Employer>>();
            _authSevicesMock = new Mock<IAuthServices>();
            _employerServices = new EmployerServices(_employerRepositoryMock.Object, _loggerMock.Object, _authSevicesMock.Object);

        }

        [Test]
        public async Task GetAllEmployersAsync()
        {
            // Arrange
            var employer = new List<Employer> { new Employer { CompanyName = "adw", ContactPhone = "1234567890", CwebsiteUrl = "adwf", Email = "wadf@adw.com", EmployerName = "wadf", Gender = "male", Password = "adwfe", UserName = "adwf" } };
            _employerRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employer);

            // Act
            var result = await _employerServices.GetAllEmployersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(employer.Count));
        }

        [Test]
        public async Task GetEmployerByIdAsync()
        {
            // Arrange
            int employerId = 1;
            var employer = new Employer { EmployerId = employerId, CompanyName = "adw", ContactPhone = "1234567890", CwebsiteUrl = "adwf", Email = "wadf@adw.com", EmployerName = "wadf", Gender = "male", Password = "adwfe", UserName = "adwf" };
            _employerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employer, bool>>>(), false)).ReturnsAsync(employer);

            // Act
            var result = await _employerServices.GetEmployerByIdAsync(employerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employerId, result.EmployerId);

        }

        [Test]
        public async Task CreateEmployerAsync()
        {
            // Arrange
            var validEmployer = new Employer { EmployerId = 1, CompanyName = "adw", ContactPhone = "1234567890", CwebsiteUrl = "adwf", Email = "wadf@adw.com", EmployerName = "wadf", Gender = "male", Password = "adwfe", UserName = "adwf" };
            _employerRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Employer>())).ReturnsAsync(validEmployer);

            // Act
            var result = await _employerServices.CreateEmployerAsync(validEmployer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(validEmployer, result);
        }

        [Test]
        public async Task UpdateEmployerAsync()
        {
            // Arrange
            var existingEmployer = new Employer { EmployerId = 1, CompanyName = "adw", ContactPhone = "1234567890", CwebsiteUrl = "adwf", Email = "wadf@adw.com", EmployerName = "wadf", Gender = "male", Password = "adwfe", UserName = "adwf" };

            _employerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employer, bool>>>(), true)).ReturnsAsync(existingEmployer);

            // Act
            var result = await _employerServices.UpdateEmployerAsync(existingEmployer,true);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteEmployerAsync()
        {
            // Arrange
            var existingEmployerId = 1;
            var existingEmployer = new Employer { EmployerId = existingEmployerId };
            _employerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Employer, bool>>>(), false)).ReturnsAsync(existingEmployer);

            // Act
            var result = await _employerServices.DeleteEmployerAsync(existingEmployerId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
