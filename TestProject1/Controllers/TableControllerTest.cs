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
        public void AddTable_ShouldRetuenOk_WhenAddSuccess()
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
        public void AddTable_ShouldRetuenBadRequest_WhenAddFailed()
        {
            //Arrange
            Aotable table = null;
            var returnData = fixture.Create<Aotable>();
            tableInterface.Setup(t => t.AddTable(table)).ReturnsAsync((Aotable)null);
            //Act
            var result = tableController.AddTable(table);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            //tableInterface.Verify(t => t.AddTable(table), Times.Once());
        }


        [Fact]
        public void EditTable_ShouldRetuenOk_WhenEditSuccess()
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
        public void EditTable_ShouldReturn_WhenEditFailed()
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
            //tableInterface.Verify(t => t.UpdateTable(id, table), Times.Once());
        }

        [Fact]
        public void GetAllTableByType_ShouldRetuenOk_WhenDataFound()
        {
            //Arrange
            var tableMock = fixture.Create<IEnumerable<Aotable>>();
            tableInterface.Setup(t => t.GetAllTableByType()).Returns(tableMock);
            //Act
            var result = tableController.GetAllTableByType();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>();
            tableInterface.Verify(t =>t.GetAllTableByType(), Times.Once());
            
           
        }

        [Fact]
        public void GetAllTableByType_ShouldRetuenOk_WhenDataNotFound()
        {
            //Arrange
             List<Aotable> tableMock = null;
            tableInterface.Setup(t => t.GetAllTableByType()).Returns(tableMock);
            //Act
            var result = tableController.GetAllTableByType();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<NotFoundResult>();
            tableInterface.Verify(t => t.GetAllTableByType(), Times.Once());


        }
    }
}