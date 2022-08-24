namespace Core.User
{
  using Core.Board;

  public interface IUserBoardService
  {
    public Task Create(string userId, string name);
  }

  public class UserBoardService : IUserBoardService
  {

    private IBoardRepository _boardRepository;

    public UserBoardService(IBoardRepository boardRepository)
    {
      _boardRepository = boardRepository;
    }

    async public Task Create(string userId, string name)
    {
      var board = new UserBoard { Name = name, UserId = userId };

      await _boardRepository.Create(board);
    }

  }
}