using Moq;
using Core.Entities;

namespace UnitTests.Core.Entities;

public class BoardServiceTests
{

  BoardService service;
  Mock<IBoardRepository> mock;

  public BoardServiceTests() : base()
  {
    mock = new Mock<IBoardRepository>();
    service = new BoardService(mock.Object);
  }

  [Fact]
  async public void CreateBoard_Should_Create_a_Board_With_The_Passed_Name()
  {
    string name = "Board name";
    string userId = "user_id";
    await service.CreateUserBoard(userId, name);

    mock.Verify(r => r.Create(It.Is<Board>(b => b.Name == name)), Times.Once());
  }

}