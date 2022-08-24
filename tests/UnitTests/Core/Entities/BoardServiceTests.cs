using Moq;
using Core.Board;

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

}