namespace Core.Entities;


public interface IBoardService
{

  public Task CreateBoard(String name);
  public Task CreateColumn(int boardId, string name);
  public Task CreateTask(int boardId, int columnId, string name);

  public Task UpdateTask(int boardId, int columnId, int newColumnId, int taskId);

}

public class BoardService : IBoardService
{

  private IBoardRepository boardRepository;

  public BoardService(IBoardRepository _boardRepository)
  {
    boardRepository = _boardRepository;
  }

  async public Task CreateBoard(string name)
  {
    var board = new Board { Name = name };

    await boardRepository.Create(board);
  }

  public async Task CreateColumn(int boardId, string name)
  {
    var board = await boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();
    var column = new Column { Name = name };
    board.AddColumn(column);
    await boardRepository.Update(board);
  }

  public async Task CreateTask(int boardId, int columnId, string name)
  {
    var board = await boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();
    var card = new Card { Name = name };
    board.AddCard(columnId, card);
    await boardRepository.Update(board);
  }

  public async Task UpdateTask(int boardId, int columnId, int newColumnId, int cardId)
  {
    var board = await boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();

    board.MoveCard(columnId, newColumnId, cardId);
    await boardRepository.Update(board);
  }
}