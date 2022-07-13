namespace Core.Entities;


public interface IBoardService
{

  public void CreateBoard(String name);
  public void CreateColumn(int boardId, string name);


}

public class BoardService : IBoardService
{

  private IBoardRepository boardRepository;

  public BoardService(IBoardRepository _boardRepository)
  {
    boardRepository = _boardRepository;
  }

  public void CreateBoard(string name)
  {
    var board = new Board { Name = name };

    boardRepository.Create(board);
  }

  public void CreateColumn(int boardId, string name)
  {
    var board = boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();
    var column = new Column { Name = name };
    board.AddColumn(column);
    boardRepository.Update(board);
  }
}