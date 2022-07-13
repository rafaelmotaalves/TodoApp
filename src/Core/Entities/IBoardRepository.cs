namespace Core.Entities;

public interface IBoardRepository
{
  public List<Board> GetAll();

  public Board? Get(int id);

  public void Create(Board board);
}