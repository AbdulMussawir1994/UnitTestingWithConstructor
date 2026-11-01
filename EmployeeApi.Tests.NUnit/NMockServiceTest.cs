using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EmployeeApi.Tests.NUnit;

[TestFixture]
public sealed class EmployeeServiceNUnitTests
{
    private Mock<IEmployeeService> _repoMock = null!;
    private IEmployeeService _service = null!;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<IEmployeeService>(MockBehavior.Strict);

        var logger = Mock.Of<ILogger<EmployeeService>>();

        //  _service = new EmployeeService(_repoMock.Object, logger);
        _service = new EmployeeService();
    }

    [Test]
    public async Task GetByIdAsync_EmployeeExists_ReturnsEmployee()
    {
        // Arrange
        const int employeeId = 1;

        var expected = new Employee(employeeId, "Raheel", "IT", 75000m);

        _repoMock.Setup(x => x.GetByIdAsync(employeeId, It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var result = await _service.GetByIdAsync(employeeId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(expected));

        //      _repoMock.Verify(x => x.GetByIdAsync(employeeId, It.IsAny<CancellationToken>()), Times.Once);
        //   _repoMock.Verify(x => x.GetByIdAsync(employeeId));
    }
}
