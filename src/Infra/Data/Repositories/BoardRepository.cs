using C = Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;

public class BoardRepository : C.IBoardRepository
{

  private TodoContext todoContext;

  public BoardRepository(TodoContext _todoContext)
  {
    todoContext = _todoContext;
  }

  public Task<List<C.Board>> GetAll() => todoContext.Boards.ToListAsync();

  public Task<C.Board?> Get(int id) => todoContext.Boards
    .Include(b => b.Columns)
    .ThenInclude(c => c.Cards)
    .FirstOrDefaultAsync(b => b.Id == id);

  async public Task Create(C.Board board)
  {
    await todoContext.AddAsync(board);
    await todoContext.SaveChangesAsync();
  }

  public async Task Update(C.Board board)
  {
    todoContext.Update(board);
    await todoContext.SaveChangesAsync();
  }

}