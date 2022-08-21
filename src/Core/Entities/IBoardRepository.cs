namespace Core.Entities;

public interface IBoardRepository
{
  public Task<List<UserBoard>> GetAllUser(string userId);

  public Task<UserBoard?> GetUser(string userId, int id);

  public Task<Board?> Get(int id);

  public Task Create(Board board);

  public Task Update(Board board);
}