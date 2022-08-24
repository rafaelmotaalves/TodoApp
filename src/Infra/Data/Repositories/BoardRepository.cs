using Core.Board;
using Core.User;
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

  public Task<List<UserBoard>> GetAllUser(string userId) => todoContext.UserBoards.Where(b => b.UserId == userId).ToListAsync();

  async public Task<Board?> Get(int id)
  {
    var board = await todoContext.Boards
      .Include(b => b.Columns)
      .ThenInclude(c => c.Cards)
      .FirstOrDefaultAsync(b => b.Id == id);

    if (board is TeamBoard)
      await todoContext
        .Entry((TeamBoard)board)
        .Reference(b => b.Team)
        .LoadAsync();

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