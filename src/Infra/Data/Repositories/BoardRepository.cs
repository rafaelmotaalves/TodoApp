using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class BoardRepository : IBoardRepository
{

  private TodoContext todoContext;

  public BoardRepository(TodoContext _todoContext)
  {
    todoContext = _todoContext;
  }

  public Task<List<Board>> GetAll(string userId) => todoContext.Boards.Where(b => b.UserId == userId).ToListAsync();

  public Task<Board?> Get(string userId, int id) => todoContext.Boards
    .Include(b => b.Columns)
    .ThenInclude(c => c.Cards)
    .FirstOrDefaultAsync(b => b.UserId == userId && b.Id == id);

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