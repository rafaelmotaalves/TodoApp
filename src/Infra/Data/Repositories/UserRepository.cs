using Core.User;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
  public class UserRepository : IUserRepository
  {

    private TodoContext _todoContext;

    public UserRepository(TodoContext todoContext)
    {
      _todoContext = todoContext;
    }

    public Task<User?> Get(string id) => _todoContext.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<List<User>> GetAll() => _todoContext.Users.ToListAsync();
  }
}