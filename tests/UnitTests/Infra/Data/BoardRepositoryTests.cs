using Infra.Data.Repositories;
using UnitTests.Util;
using Core.Entities;

namespace UnitTests.Core.Entities;

public class BoardRepositoryTests : DatabaseTests
{

  IBoardRepository repository;

  public BoardRepositoryTests() : base()
  {
    repository = new BoardRepository(fixture.TodoContext);
  }

  [Fact]
  public void CreateBoard_Should_Add_The_Board_And_Save()
  {
    // when
    var board = new Board { Name = "Board name" };
    repository.Create(board);

    // then
    var getBoard = repository.Get(1);
    Assert.NotNull(getBoard);
    if (getBoard is not null)
      Assert.Equal(getBoard.Name, board.Name);
  }

  [Fact]
  public void GetAll_Should_Get_All_Boards()
  {
    // given
    fixture.TodoContext.AddRange(
      new Board { Name = "Test board 1" },
      new Board { Name = "Test board 2" }
    );
    fixture.TodoContext.SaveChanges();
    // when
    var boards = repository.GetAll();
    // then
    Assert.Collection(boards,
      item => Assert.Equal(item.Name, "Test board 1"),
      item => Assert.Equal(item.Name, "Test board 2")
    );
  }

  [Fact]
  public void GetAll_Should_Return_Columns()
  {
    // given
    fixture.TodoContext.AddRange(
      new Board
      {
        Name = "Test board",
        Columns = new List<Column>() {
          new Column { Name = "Test column" }
        }
      }
    );
    fixture.TodoContext.SaveChanges();
    // when
    var boards = repository.GetAll();
    // then
    Assert.Collection(boards,
      item =>
      {
        Assert.Equal(item.Name, "Test board");
        Assert.Collection(item.Columns, column => Assert.Equal(column.Name, "Test column"));
      });
  }
}