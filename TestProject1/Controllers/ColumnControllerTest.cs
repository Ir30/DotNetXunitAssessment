using AssessmentAPI.Controllers;
using AssessmentAPI.Models;
using AssessmentAPI.Service.Interface;
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper.Execution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;

namespace TestProject1.Controllers
{
    public class ColumnControllerTest
    {

        private readonly IFixture fixture;
        private readonly IFixture _fixture;
        private ColumnController columnController;
        private Mock<IColumnInterface> columnInterface;
        public ColumnControllerTest()
        {
            fixture = new Fixture();

            columnInterface = fixture.Freeze<Mock<IColumnInterface>>();
            columnController = new ColumnController(columnInterface.Object);
        }
        //Test cases AddColumn()
        [Fact]
        public void AddColumn_ShouldReturnOk_WhenSuccess()
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

        [Fact]
        public void AddColumn_ShouldReturnBadRequest_WhenForeignkeyIsInValid()
        {
            //Arrange
            var column = fixture.Create<Aocolumn>();
            column.TableId = null;
            var expectedExceptionMessage = "Please give a valid foreign key";
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
        public void AddColumn_ShouldReturnBadRequest_WhenInputObjectIsNull()
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
            columnInterface.Verify(t => t.AddColumn(column), Times.Never());

        }

        [Fact]
        public void AddColumn_ShouldReturnNotFound_WhenAddFailed()
        {
            //Arrange
            var column = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.AddColumn(column)).Returns(Task.FromResult<Aocolumn>(null));

            //Act
            var result = columnController.AddColumn(column);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            columnInterface.Verify(t => t.AddColumn(column), Times.Once());
        }

        //Test cases EditColumn()
        [Fact]
        public void EditColumn_ShouldReturnOkObjectResult_WhenEditSuccess()
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
        public void EditColumn_ShouldReturnBadRequest_WhenInputObjectIsNull()
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
            columnInterface.Verify(t => t.EditColumn(id,column), Times.Never());

        }

        [Fact]
        public void EditColumn_ShouldReturnNotFoundObjectResult_WhenNoDataFound()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var column = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.EditColumn(id, column)).Returns(Task.FromResult<Aocolumn>(null));

            //Act
            var result = columnController.EditColumn(id, column);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            columnInterface.Verify(t => t.EditColumn(id, column), Times.Once());
        }

        [Fact]
        public void EditColumn_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var column = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.EditColumn(id, column)).Throws(new Exception());

            //Act
            var result = columnController.EditColumn(id, column);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            columnInterface.Verify(t => t.EditColumn(id, column), Times.Once());
        }

        //Test cases DeleteColumn()
        [Fact]
        public void DeleteColumn_ShouldReturnOk_WhenSuccess()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            var returnData = fixture.Create<Aocolumn>();
            columnInterface.Setup(c => c.DeleteColumn(id)).ReturnsAsync(returnData);

            //Act
            var result = columnController.DeleteColumn(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<ActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.DeleteColumn(id), Times.Once());
        }

        [Fact]
        public void DeleteColumn_ShouldReturnNotfound_WhenFailed()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            columnInterface.Setup(c => c.DeleteColumn(id)).Returns(Task.FromResult<Aocolumn>(null));

            //Act
            var result = columnController.DeleteColumn(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<ActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            columnInterface.Verify(t => t.DeleteColumn(id), Times.Once());
        }

        [Fact]
        public void DeleteColumn_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            //Arrange
            Guid id = fixture.Create<Guid>();
            columnInterface.Setup(c => c.DeleteColumn(id)).Throws(new Exception());

            //Act
            var result = columnController.DeleteColumn(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<ActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            columnInterface.Verify(t => t.DeleteColumn(id), Times.Once());
        }
        //Test cases GetColumnBytype()
        [Fact]
        public void GetColumnBytype_ShouldReturnOk_WhenSuccess()
        {
            //Arrange
            var column = fixture.Create<IEnumerable<Aocolumn>>();
            columnInterface.Setup(c => c.GetColumnBytype()).ReturnsAsync(column);

            //Act
            var result = columnController.GetColumnBytype();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.GetColumnBytype(), Times.Once());
        }

        [Fact]
        public void GetColumnBytype_ShouldReturnNotfoundk_WhenDataNotFound()
        {
            //Arrange
            columnInterface.Setup(c => c.GetColumnBytype()).Returns(Task.FromResult<IEnumerable<Aocolumn>>(null));

            //Act
            var result = columnController.GetColumnBytype();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            columnInterface.Verify(t => t.GetColumnBytype(), Times.Once());
        }

        [Fact]
        public void GetColumnBytype_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            //Arrange
            columnInterface.Setup(c => c.GetColumnBytype()).Throws(new Exception());

            //Act
            var result = columnController.GetColumnBytype();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            columnInterface.Verify(t => t.GetColumnBytype(), Times.Once());
        }

        //Test cases GetTableDataByName()
        [Fact]
        public void GetTableDataByName_ShouldReturnOk_WhenSuccess()
        {
            //Arrange
            var name = fixture.Create<string>();
            var table = fixture.Create<IEnumerable<Aotable>>();
            columnInterface.Setup(c => c.GetTableDataByname(name)).ReturnsAsync(table);

            //Act
            var result = columnController.GetTableDataByname(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            columnInterface.Verify(t => t.GetTableDataByname(name), Times.Once());
        }

        [Fact]
        public void GetTableDataByName_ShouldRetrnBadRequest_WhenNameIsNull()
        {
            //Arrange
            string name = null;
            var table = fixture.Create<IEnumerable<Aotable>>();
            columnInterface.Setup(c => c.GetTableDataByname(name)).ReturnsAsync(table);

            //Act
            var result = columnController.GetTableDataByname(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            columnInterface.Verify(t => t.GetTableDataByname(name), Times.Never());
        }
        [Fact]
        public void GetTableDataByName_ShouldRetrnBadRequest_WhenNameIsEmpty()
        {
            //Arrange
            string name = "";
            columnInterface.Setup(c => c.GetTableDataByname(name));

            //Act
            var result = columnController.GetTableDataByname(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            columnInterface.Verify(t => t.GetTableDataByname(name), Times.Never());
        }

        [Fact]
        public void GetTableDataByName_ShouldReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            var name = fixture.Create<string>();
            columnInterface.Setup(c => c.GetTableDataByname(name)).Returns(Task.FromResult<IEnumerable<Aotable>>(null));

            //Act
            var result = columnController.GetTableDataByname(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            columnInterface.Verify(t => t.GetTableDataByname(name), Times.Once());
        }

        [Fact]
        public void GetTableDataByName_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            //Arrange
            var name = fixture.Create<string>();
            columnInterface.Setup(c => c.GetTableDataByname(name)).Throws(new Exception());

            //Act
            var result = columnController.GetTableDataByname(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            columnInterface.Verify(t => t.GetTableDataByname(name), Times.Once());
        }

    }
}