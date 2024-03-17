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
    public class ResumeTests
    {
        private Mock<ILogger<ResumeController>> _loggerMock;
        private Mock<IRepository<Resume>> _resumeRepositoryMock;
        private ResumeServices _resumeServices;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ResumeController>>();
            _resumeRepositoryMock = new Mock<IRepository<Resume>>();
            _resumeServices = new ResumeServices(_resumeRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllResumesAsync()
        {
            // Arrange
            var resume = new List<Resume> { new Resume { JobSeekerId=1, ResumeUrl="hello.txt", UploadedDate=DateTime.Now, Status="active"} };
            _resumeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(resume);

            // Act
            var result = await _resumeServices.GetAllResumesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(resume.Count));
        }

        [Test]
        public async Task GetResumeByIdAsync()
        {
            // Arrange
            int resumeId = 1;
            var resume = new Resume { ResumeId = resumeId, JobSeekerId = 1, ResumeUrl = "hello.txt", UploadedDate = DateTime.Now, Status = "active" };
            _resumeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Resume, bool>>>(), false)).ReturnsAsync(resume);

            // Act
            var result = await _resumeServices.GetResumeByIdAsync(resumeId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(resumeId, result.ResumeId);

        }

        [Test]
        public async Task CreateResumeAsync()
        {
            // Arrange
            var validResume = new Resume { ResumeId = 1, JobSeekerId = 1, ResumeUrl = "hello.txt", UploadedDate = DateTime.Now, Status = "active" };
            _resumeRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Resume>())).ReturnsAsync(validResume);

            // Act
            var result = await _resumeServices.CreateResumeAsync(validResume);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(validResume, result);
        }

        [Test]
        public async Task UpdateResumeAsync()
        {
            // Arrange
            var existingResume = new Resume { ResumeId = 1, JobSeekerId = 1, ResumeUrl = "hello.txt", UploadedDate = DateTime.Now, Status = "active" };

            _resumeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Resume, bool>>>(), true)).ReturnsAsync(existingResume);

            // Act
            var result = await _resumeServices.UpdateResumeAsync(existingResume);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteResumeAsync()
        {
            // Arrange
            var existingResumeId = 1;
            var existingResume = new Resume { ResumeId = existingResumeId };
            _resumeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Resume, bool>>>(), false)).ReturnsAsync(existingResume);

            // Act
            var result = await _resumeServices.DeleteResumeAsync(existingResumeId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
