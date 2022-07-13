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

  public List<Board> GetAll() => todoContext.Boards
    .Include(b => b.Columns)
    .ToList();

  public Board? Get(int id) => todoContext.Boards
    .Include(b => b.Columns)
    .FirstOrDefault(b => b.Id == id);

  public void Create(Board board)
  {
    todoContext.Add(board);
    todoContext.SaveChanges();
  }

  public void Update(Board board)
  {
    todoContext.Update(board);
    todoContext.SaveChanges();
  }

}