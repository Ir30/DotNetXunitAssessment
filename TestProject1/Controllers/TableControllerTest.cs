using AssessmentAPI.Controllers;
using AssessmentAPI.Models;
using AssessmentAPI.Service.Interface;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace TestProject1.Controllers
{
    public class TableControllerTest
    {
        private readonly IFixture fixture;
        private readonly Mock<ItableInterface> tableInterface;
        private readonly TableController tableController;
        public TableControllerTest()
        {
            fixture = new Fixture();
            tableInterface = fixture.Freeze<Mock<ItableInterface>>();
            tableController = new TableController(tableInterface.Object);
        }

        [Fact]
        public void AddTable_ShouldReturnOk_WhenAddSuccess()
        {
            //Arrange
            var table = fixture.Create<Aotable>();
            var returnData = fixture.Create<Aotable>();
            tableInterface.Setup(t => t.AddTable(table)).ReturnsAsync(returnData);
            //Act
            var result = tableController.AddTable(table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            tableInterface.Verify(t => t.AddTable(table), Times.Once());
        }

        [Fact]
        public void AddTable_ShouldReturnBadRequest_WhenTableObjectIsNull()
        {
            //Arrange
            Aotable table = null;
            tableInterface.Setup(t => t.AddTable(table)).ReturnsAsync((Aotable)null);
            //Act
            var result = tableController.AddTable(table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            tableInterface.Verify(t => t.AddTable(table), Times.Never());
        }

        [Fact]
        public void AddTable_ShouldReturnBadRequestObjectResult_WhenAddFailed()
        {
            //Arrange
            var table = fixture.Create<Aotable>();
            tableInterface.Setup(t => t.AddTable(table)).Returns(Task.FromResult<Aotable>(null));
            //Act
            var result = tableController.AddTable(table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            tableInterface.Verify(t => t.AddTable(table), Times.Once());
        }
        [Fact]
        public void AddTable_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            //Arrange
            var table = fixture.Create<Aotable>();
            tableInterface.Setup(t => t.AddTable(table)).Throws(new Exception());
            //Act
            var result = tableController.AddTable(table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            tableInterface.Verify(t => t.AddTable(table), Times.Once());
        }


        [Fact]
        public void EditTable_ShouldReturnOk_WhenEditSuccess()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var table = fixture.Create<Aotable>();
            var returnData = fixture.Create<Aotable>();
            tableInterface.Setup(t => t.UpdateTable(id,table)).ReturnsAsync(returnData);

            //Act
            var result = tableController.EditTable(id,table);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            tableInterface.Verify(t => t.UpdateTable(id, table), Times.Once());
        }

        [Fact]
        public void EditTable_ShouldReturnBadRequestResult_WhenEditFailed()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            Aotable table = null;
            tableInterface.Setup(t => t.UpdateTable(id, table)).ReturnsAsync((Aotable)null);
            //Act
            var result = tableController.EditTable(id, table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            tableInterface.Verify(t => t.UpdateTable(id, table), Times.Never());
        }

        [Fact]
        public void EditTable_ShouldReturnNotFoundObjectResult_WhenDataNotFound()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            Aotable table = fixture.Create<Aotable>();
            tableInterface.Setup(t => t.UpdateTable(id, table)).Returns(Task.FromResult<Aotable>(null));
            //Act
            var result = tableController.EditTable(id, table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            tableInterface.Verify(t => t.UpdateTable(id, table), Times.Once());
        }

        [Fact]
        public void EditTable_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            Aotable table = fixture.Create<Aotable>();
            tableInterface.Setup(t => t.UpdateTable(id, table)).Throws(new Exception());
            //Act
            var result = tableController.EditTable(id, table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            tableInterface.Verify(t => t.UpdateTable(id, table), Times.Once());
        }

        [Fact]
        public void GetAllTableByType_ShouldReturnOk_WhenDataFound()
        {
            //Arrange
            var tableMock = fixture.Create<IEnumerable<Aotable>>();
            tableInterface.Setup(t => t.GetAllTableByType()).ReturnsAsync(tableMock);
            //Act
            var result = tableController.GetAllTableByType();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            tableInterface.Verify(t =>t.GetAllTableByType(), Times.Once());    
        }

        [Fact]
        public void GetAllTableByType_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            tableInterface.Setup(t => t.GetAllTableByType()).Returns(Task.FromResult<IEnumerable<Aotable>>(null));
            //Act
            var result = tableController.GetAllTableByType();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            tableInterface.Verify(t => t.GetAllTableByType(), Times.Once());
        }

        [Fact]
        public void GetAllTableByType_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            //Arrange
            tableInterface.Setup(t => t.GetAllTableByType()).Throws(new Exception());
            //Act
            var result = tableController.GetAllTableByType();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            tableInterface.Verify(t => t.GetAllTableByType(), Times.Once());
        }

    }
}