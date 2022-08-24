using Infra.Data.Repositories;
using UnitTests.Util;
using Core.Board;
using Core.User;

namespace UnitTests.Core.Entities;

public class BoardRepositoryTests : DatabaseTests
{

  IBoardRepository repository;

  public BoardRepositoryTests() : base()
  {
    repository = new BoardRepository(fixture.TodoContext);
  }

  [Fact]
  async public void CreateBoard_Should_Add_The_Board_And_Save()
  {
    // when
    var board = new UserBoard { Name = "Board name", UserId = "user_id" };
    await repository.Create(board);

    // then
    var getBoard = await repository.Get(1);
    Assert.NotNull(getBoard);
    if (getBoard is not null)
      Assert.Equal(getBoard.Name, board.Name);
  }

}