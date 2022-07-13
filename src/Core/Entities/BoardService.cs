namespace Core.Entities;


public interface IBoardService
{

  public void CreateBoard(String name);

}

public class BoardService : IBoardService
{

  private IBoardRepository boardRepository;

  public BoardService(IBoardRepository _boardRepository)
  {
    boardRepository = _boardRepository;
  }

  public void CreateBoard(String name)
  {
    var board = new Board { Name = name };

    boardRepository.Create(board);
  }
}