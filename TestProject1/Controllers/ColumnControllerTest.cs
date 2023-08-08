using AssessmentAPI.Controllers;
using AssessmentAPI.Models;
using AssessmentAPI.Service.Interface;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject1.Controllers
{
    public class ColumnControllerTest
    {

        private readonly IFixture fixture;
        private ColumnController columnController;
        private Mock<IColumnInterface> columnInterface;
        public ColumnControllerTest()
        {
            fixture = new Fixture();
            columnInterface = fixture.Freeze<Mock<IColumnInterface>>();
            columnController = new ColumnController(columnInterface.Object);
        }

        [Fact]
        public void AddColumn_ShouldRetuenOk_WhenSuccess()
        {
            //Arrange
            var column = fixture.Create<Aocolumn>();
            var returnData = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.AddColumn(column)).ReturnsAsync(returnData);
            //Act
            var result = columnController.AddColumn(column);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.AddColumn(column), Times.Once());
        }

        public void AddColumn_ShouldReturnBadRequest_WhenInvalidFreignKey()
        {
            //Arrange
            var column = fixture.Create<Aocolumn>();
            column.TableId = null;
            var expectedExceptionMessage = "Invalid Foreign Key";
            var returnData = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.AddColumn(column)).Throws(new Exception(expectedExceptionMessage));
            //Act
            var result = columnController.AddColumn(column);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            columnInterface.Verify(t => t.AddColumn(column), Times.Once());
        }

        [Fact]
        public void AddColumn_ShouldRetuenBadRequest_WhenFailed()
        {
            //Arrange
            Aocolumn column = null;
            columnInterface.Setup(c => c.AddColumn(column)).ReturnsAsync((Aocolumn)null);
            //Act
            var result = columnController.AddColumn(column);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
        }


        [Fact]
        public void EditColumn_ShouldRetuenOk_WhenSuccess()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var column = fixture.Create<Aocolumn>();
            var returnData = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.EditColumn(id,column)).ReturnsAsync(returnData);
            //Act
            var result = columnController.EditColumn(id,column);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.EditColumn(id,column), Times.Once());
        }

        [Fact]
        public void EditColumn_ShouldRetuenBadRequest_WhenFailed()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            Aocolumn column = null;
            columnInterface.Setup(c => c.EditColumn(id, column)).ReturnsAsync((Aocolumn)null);
            //Act
            var result = columnController.EditColumn(id, column);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
        }


        [Fact]
        public void DeleteColumn_ShouldRetuenOk_WhenSuccess()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var returnData = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.DeleteColumn(id)).Returns(returnData);
            //Act
            var result = columnController.DeleteColumn(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<ActionResult>>();
            result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.DeleteColumn(id), Times.Once());
        }

        [Fact]
        public void DeleteColumn_ShouldNotfoundk_WhenFailed()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            Aocolumn returnData = null;
            columnInterface.Setup(c => c.DeleteColumn(id)).Returns(returnData);
            //Act
            var result = columnController.DeleteColumn(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult>();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            columnInterface.Verify(t => t.DeleteColumn(id), Times.Once());
        }

        [Fact]

        public void GetColumnBytype_ShouldReturnOk_WhenSuccess()
        {
            //Arrange
            var column = fixture.Create<IEnumerable<Aocolumn>>();
            columnInterface.Setup(c => c.GetColumnBytype()).Returns(column);
            //Act
            var result = columnController.GetColumnBytype();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.GetColumnBytype(), Times.Once());
        }

        [Fact]
        public void GetColumnBytype_ShouldNotfoundk_WhenFailed()
        {
            //Arrange
            IEnumerable<Aocolumn> returnData = null;
            columnInterface.Setup(c => c.GetColumnBytype()).Returns(returnData);
            //Act
            var result = columnController.GetColumnBytype();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult>();
            result.Should().BeAssignableTo<NotFoundResult>();
            columnInterface.Verify(t => t.GetColumnBytype(), Times.Once());
        }

        [Fact]
        public void GetTableDataByname_ShouldReturnOk_WhenSuccess()
        {
            //Arrange
            var name = fixture.Create<string>();
            var table = fixture.Create<IEnumerable<Aotable>>();
            columnInterface.Setup(c => c.GetTableDataByname(name)).Returns(table);
            //Act
            var result = columnController.GetTableDataByname(name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.GetTableDataByname(name), Times.Once());
        }


        [Fact]
        public void GetTableDataByname_ShouldRetrnBad_WhenNameEmpty()
        {
            //Arrange
            string name = null;
            var table = fixture.Create<IEnumerable<Aotable>>();
            columnInterface.Setup(c => c.GetTableDataByname(name)).Returns(table);
            //Act
            var result = columnController.GetTableDataByname(name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult>();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public void GetTableDataByname_ShouldReturnNotFound_WhenNodataFound()
        {
            //Arrange
            var name = fixture.Create<string>();
            IEnumerable<Aotable> table = null;
            columnInterface.Setup(c => c.GetTableDataByname(name)).Returns(table);
            //Act
            var result = columnController.GetTableDataByname(name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult>();
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            columnInterface.Verify(t => t.GetTableDataByname(name), Times.Once());
        }

    }
}