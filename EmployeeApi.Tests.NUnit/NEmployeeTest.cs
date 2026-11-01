using EmployeeApi.Controllers;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace EmployeeApi.Tests.NUnit;

[TestFixture]
public class NEmployeeTest
{
    private readonly IEmployeeService _service = new EmployeeService();

    [Test]
    public void Employee_Should_Be_Return_4_Count()
    {
        //Arrange
        int totalEmp = 4;

        //Act
        var response = _service.GetEmployees().Count();

        //Assert
        Assert.That(response, Is.EqualTo(totalEmp));
    }

    [Test]
    public void Employee_Should_Be_Return()
    {
        //Arrange
        int totalEmp = 4;

        //Act
        var response = _service.GetEmployees();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Has.Count.EqualTo(totalEmp));
    }

    [Test]
    public void Get_By_Id()
    {
        //Arrange
        int employeeId = 1;

        //Act
        var response = _service.GetEmployeeById(employeeId);

        //Assert
        Assert.That(response?.Name, Is.EqualTo("Raheel"));
    }

    [Test]
    public void Controller_Should_Be_200_Success()
    {
        //Arrange
        int EmployeeId = 1;
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(EmployeeId).Result;

        //Assert
        var isInstance = Is.InstanceOf<OkObjectResult>();
        Assert.That(response, isInstance);
        Assert.That(response, Is.Not.Null);
    }

    [Test]
    public void Controller_Should_Be_200_Ok()
    {
        //Arrange
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(1);

        //Assert
        var isInstance = Is.InstanceOf<OkObjectResult>();
        Assert.That(response.Result, isInstance);
    }

    [Test]
    public void Controller_Should_Be_Not_Found()
    {
        //Arrange
        int EmployeeId = 999;
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(EmployeeId).Result;

        //Assert
        var isInstance = Is.InstanceOf<NotFoundResult>();
        Assert.That(response, isInstance);
    }

    [Test]
    public void Controller_Should_Be_BadRequest()
    {
        //Arrange
        var id = 0;
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(id).Result;

        //Assert
        var isInstance = Is.InstanceOf<BadRequestResult>();
        Assert.That(response, isInstance);
    }

    [Test]
    [TestCase(1, "Raheel")]
    [TestCase(2, "Samad")]
    public void GetById_Return_Employees(int id, string expectedName)
    {
        //Arrange
        expectedName = expectedName.ToLower();
        //Act
        var response = _service.GetEmployeeById(id)?.Name.ToLower();
        //Assert
        Assert.That(response, Is.EqualTo(expectedName));
    }
}
