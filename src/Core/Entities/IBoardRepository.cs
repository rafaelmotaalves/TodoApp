namespace Core.Entities;

public interface IBoardRepository
{
  public Task<List<Board>> GetAll();

  public Task<Board?> Get(int id);

  public System.Threading.Tasks.Task Create(Board board);

  public System.Threading.Tasks.Task Update(Board board);
}