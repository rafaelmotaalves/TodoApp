namespace Core.Entities;


public interface IBoardService
{

  public void CreateBoard(String name);
  public void CreateColumn(int boardId, string name);
  public void CreateTask(int boardId, int columnId, string name);

  public void UpdateTask(int boardId, int columnId, int newColumnId, int taskId);

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

  public void CreateTask(int boardId, int columnId, string name)
  {
    var board = boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();
    var task = new Task { Name = name };
    board.AddTask(columnId, task);
    boardRepository.Update(board);
  }

  public void UpdateTask(int boardId, int columnId, int newColumnId, int taskId)
  {
    var board = boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();

    board.MoveTask(columnId, newColumnId, taskId);
    boardRepository.Update(board);
  }
}