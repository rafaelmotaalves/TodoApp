using Core.User;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
  public class UserBoardRepository : IUserBoardRepository
  {

    private readonly TodoContext _todoContext;

    public UserBoardRepository(TodoContext todoContext)
    {
      _todoContext = todoContext;
    }

    public Task<List<UserBoard>> GetAll(string userId) => _todoContext.Boards
        .OfType<UserBoard>()
        .ToListAsync();
  }
}