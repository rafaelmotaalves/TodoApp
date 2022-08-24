using Core.Board;
using Core.Team;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class BoardRepository : IBoardRepository
{

  private TodoContext todoContext;

  public BoardRepository(TodoContext _todoContext)
  {
    todoContext = _todoContext;
  }

  async public Task<Board?> Get(int id)
  {
    var board = await todoContext.Boards
      .Include(b => b.Columns)
      .ThenInclude(c => c.Cards)
      .FirstOrDefaultAsync(b => b.Id == id);

    if (board is TeamBoard) {
      var teamBoard = (TeamBoard)board;
      await todoContext
        .Entry(teamBoard)
        .Reference(b => b.Team)
        .LoadAsync();
      await todoContext
        .Entry(teamBoard.Team)
        .Collection(t => t.Members)
        .LoadAsync();
    }

    return board;
  }

  async public Task Create(Board board)
  {
    await todoContext.AddAsync(board);
    await todoContext.SaveChangesAsync();
  }

  public async Task Update(Board board)
  {
    todoContext.Update(board);
    await todoContext.SaveChangesAsync();
  }

}