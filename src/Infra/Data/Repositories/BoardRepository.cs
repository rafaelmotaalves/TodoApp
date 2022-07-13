using Core.Entities;

namespace Infra.Data.Repositories;

public class BoardRepository : IBoardRepository
{

  private TodoContext todoContext;

  public BoardRepository(TodoContext _todoContext)
  {
    todoContext = _todoContext;
  }

  public List<Board> GetAll() => todoContext.Boards.ToList();

  public Board? Get(int id) => todoContext.Boards.FirstOrDefault(b => b.Id == id);

  public void Create(Board board)
  {
    todoContext.Add(board);
    todoContext.SaveChanges();
  }

}