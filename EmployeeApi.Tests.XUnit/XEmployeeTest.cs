using EmployeeApi.Controllers;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EmployeeApi.Tests.XUnit;

public class XEmployeeTest
{
    private readonly EmployeeService _service = new EmployeeService();

    [Fact]
    public void Get_All_Return_Three_Employees()
    {
        //Arrange
        int totalEmployees = 4;

        //Act
        var response = _service.GetEmployees();

        //Assert
        Assert.Equal(totalEmployees, response.Count);
        Assert.NotNull(response);
    }

    [Theory]
    [InlineData(1, "Raheel")]
    [InlineData(2, "Samad")]
    public void GetById_Return_Employees(int id, string expectedName)
    {
        //Arrange
        expectedName = expectedName.ToLower();

        //Act
        var response = _service.GetEmployeeById(id)?.Name.ToLower();

        //Assert
        Assert.Equal(expectedName, response);

    }

    [Fact]
    public void GetEmployeeById_Salary_Should_Be_Greater_Than_Zero()
    {
        //Arrange
        int id = 2;
        decimal salary = 50000m;
        decimal expectedSalary = 25000m;

        //Act
        var response = _service.GetEmployeeById(id);

        //Assert
        Assert.NotNull(response);
        Assert.False(response?.Salary > salary);
        Assert.Equal(expectedSalary, response?.Salary);
    }

    public void Controller_GetById_Existing_ReturnsOk()
    {
        var result = new EmployeeController(_service).GetEmployeeById(1);
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, ok.StatusCode);
    }
    [Fact]
    public void Controller_GetById_Invalid_ReturnsBadRequest() =>
        Assert.IsType<BadRequestResult>(new EmployeeController(_service).GetEmployeeById(0).Result);
    [Fact]
    public void Controller_GetById_Missing_ReturnsNotFound() =>
        Assert.IsType<NotFoundResult>(new EmployeeController(_service).GetEmployeeById(999).Result);
}
