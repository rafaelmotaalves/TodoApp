using Api.Controllers;
using Core.Board;
using Core.User;
using Core.Exceptions;
using Api.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
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

    var configuration = new MapperConfiguration(cfg => {
      cfg.AddProfile<AutoMapperProfile>();
    });

    controller = new BoardController(mockRepository.Object, mockService.Object, configuration.CreateMapper());
  }

  [Fact]
  async public void Get_Should_Return_A_Board_With_The_Passed_Id()
  {
    // given
    int id = 100;
    var board = new UserBoard
    {
      Id = id,
      Name = "Test board",
      Columns = new List<Column>()
      {
        new Column { Name = "Test column"}
      },
      UserId = "user_id"
    };
    mockRepository.Setup(m => m.Get(100))
      .ReturnsAsync(board);
    // when
    var res = await controller.Get(100);
    // then
//    Assert.Equal(res.Value, board);
  }

  [Fact]
  async public void CreateColumn_Should_Return_Not_Found_If_Doesnt_Find_Board()
  {
    // given
    int id = 100;
    mockService.Setup(m => m.CreateColumn("user_id", id, "Name")).Throws<EntityNotFoundException>();
    // when
    var res = await controller.CreateColumn(id, new CreateColumnDto { Name = "Name" });
    // then
    Assert.IsType<NotFoundResult>(res);
  }

  [Fact]
  async public void CreateColumn_Should_Create_A_Column()
  {
    // given
    int id = 1;
    // when
    var res = await controller.CreateColumn(id, new CreateColumnDto { Name = "Name" });
    // then
    Assert.IsType<CreatedAtActionResult>(res);
    mockService.Verify(s => s.CreateColumn("user_id", id, "Name"), Times.Once());
  }

}