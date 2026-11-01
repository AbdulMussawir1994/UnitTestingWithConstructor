using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EmployeeApi.Tests.XUnit;

public class EmployeeServiceXUnitTests
{
    private readonly Mock<IEmployeeService> _repoMock;
    private readonly IEmployeeService _service;

    public EmployeeServiceXUnitTests()
    {
        _repoMock = new Mock<IEmployeeService>(MockBehavior.Strict);

        var logger = Mock.Of<ILogger<EmployeeService>>();

        //   _service = new EmployeeService(_repoMock.Object, logger);
        _service = new EmployeeService();
    }

    [Fact]
    public async Task GetByIdAsync_EmployeeExists_ReturnsEmployee()
    {
        // Arrange
        const int employeeId = 1;

        var expected = new Employee(employeeId, "Raheel", "IT", 75000m);

        _repoMock.Setup(x => x.GetByIdAsync(employeeId, It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var result = await _service.GetByIdAsync(employeeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result);

        //     _repoMock.Verify(x => x.GetByIdAsync(employeeId, It.IsAny<CancellationToken>()), Times.Once);
        //    _repoMock.Verify(x => x.GetByIdAsync(employeeId));
    }
}
