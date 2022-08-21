namespace Core.Entities;


public interface IBoardService
{

  public Task CreateBoard(string userId, string name);
  public Task CreateColumn(string userId, int boardId, string name);
  public Task CreateTask(string userId, int boardId, int columnId, string name);

  public Task UpdateTask(string userId, int boardId, int columnId, int newColumnId, int taskId);

}

public class BoardService : IBoardService
{

  private IBoardRepository boardRepository;

  public BoardService(IBoardRepository _boardRepository)
  {
    boardRepository = _boardRepository;
  }

  async public Task CreateBoard(string userId, string name)
  {
    var board = new Board { Name = name, UserId = userId };

    await boardRepository.Create(board);
  }

  public async Task CreateColumn(string userId, int boardId, string name)
  {
    var board = await boardRepository.Get(userId, boardId);
    if (board is null)
      throw new EntityNotFoundException();
    if (board.UserId != userId) 
      throw new UnauthorizedException();

    var column = new Column { Name = name };
    board.AddColumn(column);
    await boardRepository.Update(board);
  }

  public async Task CreateTask(string userId, int boardId, int columnId, string name)
  {
    var board = await boardRepository.Get(userId, boardId);
    if (board is null)
      throw new EntityNotFoundException();
    if (board.UserId != userId) 
      throw new UnauthorizedException();
    
    var card = new Card { Name = name };
    board.AddCard(columnId, card);
    await boardRepository.Update(board);
  }

  public async Task UpdateTask(string userId, int boardId, int columnId, int newColumnId, int cardId)
  {
    var board = await boardRepository.Get(userId, boardId);
    if (board is null)
      throw new EntityNotFoundException();
    if (board.UserId != userId) 
      throw new UnauthorizedException();

    board.MoveCard(columnId, newColumnId, cardId);
    await boardRepository.Update(board);
  }
}