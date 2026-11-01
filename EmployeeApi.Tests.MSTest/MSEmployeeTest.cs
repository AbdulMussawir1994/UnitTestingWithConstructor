using EmployeeApi.Controllers;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeApi.Tests.MSTest;

[TestClass]
public class MSEmployeeTest
{
    private readonly IEmployeeService _service = new EmployeeService();
    //  private readonly Mock<EmployeeService> _tenantRepoMock;

    [TestMethod]
    public void Get_Three_Employees_Should_Be_Return()
    {
        //Arrange
        int totalEmployees = 4;

        //Act
        var response = _service.GetEmployees().Count();

        //Assert
        Assert.AreEqual(totalEmployees, response);
    }

    [TestMethod]
    [DataRow(1, "Raheel")]
    [DataRow(2, "Samad")]
    public void Get_Correct_Data(int id, string expectedResult)
    {
        //Act
        var response = _service.GetEmployeeById(id)?.Name.ToLower();

        //Assert
        Assert.AreEqual(expectedResult.ToLower(), response);
    }

    [TestMethod]
    public void Controller_Should_Be_200_Success()
    {
        //Arrange
        int EmployeeId = 1;
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(EmployeeId).Result;

        //Assert
        Assert.IsInstanceOfType<OkObjectResult>(response);
        var okResult = (OkObjectResult)response;
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.IsNotNull(okResult.Value);
    }

    [TestMethod]
    public void Controller_Should_Be_200_Ok()
    {
        //Arrange
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(1);

        //Assert
        Assert.IsNotNull(response.Result);
        var okResult = response.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void Controller_Should_Be_Not_Found()
    {
        //Arrange
        int EmployeeId = 999;
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(EmployeeId).Result;

        //Assert
        Assert.IsInstanceOfType<NotFoundResult>(response);
        var notFound = (NotFoundResult)response;
        Assert.AreEqual(404, notFound.StatusCode);
    }

    [TestMethod]
    public void Controller_Should_Be_BadRequest()
    {
        //Arrange
        var id = 0;
        var controller = new EmployeeController(_service);

        //Act
        var response = controller.GetEmployeeById(id).Result;

        //Assert
        Assert.IsInstanceOfType<BadRequestResult>(response);
        var badRequest = (BadRequestResult)response;
        Assert.AreEqual(400, badRequest.StatusCode);
    }
}
