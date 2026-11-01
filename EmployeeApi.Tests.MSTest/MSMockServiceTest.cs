using EmployeeApi.Services;

namespace EmployeeApi.Tests.MSTest;

using EmployeeApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public sealed class EmployeeServiceMSTestTests
{
    private Mock<IEmployeeService> _repoMock = null!;
    private IEmployeeService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _repoMock = new Mock<IEmployeeService>(MockBehavior.Strict);

        var logger = Mock.Of<ILogger<EmployeeService>>();

        //   _service = new EmployeeService(_repoMock.Object, logger);
        _service = new EmployeeService();
    }

    [TestMethod]
    public async Task GetByIdAsync_EmployeeExists_ReturnsEmployee()
    {
        // Arrange
        const int employeeId = 1;

        var expected = new Employee(employeeId, "Raheel", "IT", 75000m);

        _repoMock.Setup(x => x.GetByIdAsync(employeeId, It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var result = await _service.GetByIdAsync(employeeId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);

        //    _repoMock.Verify(x => x.GetByIdAsync(employeeId, It.IsAny<CancellationToken>()), Times.Once);
        //  _repoMock.Verify(x => x.GetByIdAsync(employeeId));
    }
}