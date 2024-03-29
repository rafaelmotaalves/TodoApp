namespace Core.Board;

using Core.Exceptions;
using Core.User;

public interface IBoardService
{

  public Task CreateColumn(string userId, int boardId, string name);
  public Task CreateTask(string userId, int boardId, int columnId, string name);
  public Task UpdateTask(string userId, int boardId, int columnId, int newColumnId, int taskId);

  public Task<Board> GetBoard(string userId, int boardId);

}

public class BoardService : IBoardService
{

  private IBoardRepository boardRepository;

  public BoardService(IBoardRepository _boardRepository)
  {
    boardRepository = _boardRepository;
  }


  public async Task CreateColumn(string ownerId, int boardId, string name)
  {
    var board = await boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();

    var column = new Column { Name = name };
    board.AddColumn(ownerId, column);
    await boardRepository.Update(board);
  }

  public async Task CreateTask(string ownerId, int boardId, int columnId, string name)
  {
    var board = await boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();
    
    var card = new Card { Name = name };
    board.AddCard(ownerId, columnId, card);
    await boardRepository.Update(board);
  }

  public async Task UpdateTask(string ownerId, int boardId, int columnId, int newColumnId, int cardId)
  {
    var board = await boardRepository.Get(boardId);
    if (board is null)
      throw new EntityNotFoundException();

    board.MoveCard(ownerId, columnId, newColumnId, cardId);
    await boardRepository.Update(board);
  }

  async public Task<Board> GetBoard(string userId, int boardId)
  {
    var board = await boardRepository.Get(boardId);
    if (board is null || !board.CanRead(userId))
      throw new EntityNotFoundException();
    return board;
  }
}