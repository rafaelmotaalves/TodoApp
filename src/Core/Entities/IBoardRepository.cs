namespace Core.Entities;

public interface IBoardRepository
{
  public Task<List<Board>> GetAll();

  public Task<Board?> Get(int id);

  public Task Create(Board board);

  public Task Update(Board board);
}