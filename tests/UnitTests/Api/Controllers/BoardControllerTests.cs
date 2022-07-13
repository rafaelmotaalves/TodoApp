using Api.Controllers;
using Core.Entities;
using Api.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

using Moq;

namespace UnitTests.Api.Controllers;


public class BoardControllerTests
{

  private BoardController controller;
  private Mock<IBoardRepository> mockRepository;
  private Mock<IBoardService> mockService;

  public BoardControllerTests()
  {
    mockRepository = new Mock<IBoardRepository>();
    mockService = new Mock<IBoardService>();

    controller = new BoardController(mockRepository.Object, mockService.Object);
  }

  [Fact]
  public void Get_Should_Return_A_Board_With_The_Passed_Id()
  {
    // given
    int id = 100;
    var board = new Board
    {
      Id = id,
      Name = "Test board",
      Columns = new List<Column>()
      {
        new Column { Name = "Test column"}
      }
    };
    mockRepository.Setup(m => m.Get(100)).Returns(board);
    // when
    var res = controller.Get(100);
    // then
    Assert.Equal(res.Value, board);
  }

  [Fact]
  public void CreateColumn_Should_Return_Not_Found_If_Doesnt_Find_Board()
  {
    // given
    int id = 100;
    mockService.Setup(m => m.CreateColumn(id, "Name")).Throws<EntityNotFoundException>();
    // when
    var res = controller.CreateColumn(id, new CreateColumnDto { Name = "Name" });
    // then
    Assert.IsType<NotFoundResult>(res);
  }

  [Fact]
  public void CreateColumn_Should_Create_A_Column()
  {
    // given
    int id = 1;
    // when
    var res = controller.CreateColumn(id, new CreateColumnDto { Name = "Name" });
    // then
    Assert.IsType<CreatedAtActionResult>(res);
    mockService.Verify(s => s.CreateColumn(id, "Name"), Times.Once());
  }


}