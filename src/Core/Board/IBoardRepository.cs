namespace Core.Board;
using Core.User;

public interface IBoardRepository
{
  public Task<Board?> Get(int id);

  public Task Create(Board board);

  public Task Update(Board board);
}