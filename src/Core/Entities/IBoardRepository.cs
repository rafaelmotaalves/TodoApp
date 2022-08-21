namespace Core.Entities;

public interface IBoardRepository
{
  public Task<List<Board>> GetAll(string userId);

  public Task<Board?> Get(string userId, int id);

  public Task Create(Board board);

  public Task Update(Board board);
}