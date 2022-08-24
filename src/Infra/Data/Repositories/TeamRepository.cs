namespace Infra.Data.Repositories
{
  using Core.Team;
  using Microsoft.EntityFrameworkCore;

  public class TeamRepository : ITeamRepository
  {

    private readonly TodoContext _todoContext;

    public TeamRepository(TodoContext todoContext)
    {
      _todoContext = todoContext;
    }

    public Task<List<Team>> GetAll() => _todoContext.Teams.ToListAsync();


    async public Task Create(Team team) {
        await _todoContext.AddAsync(team);
        await _todoContext.SaveChangesAsync();
    }

    public Task<Team?> Get(string id) => _todoContext.Teams
        .Include(t => t.Members)
        .ThenInclude(tu => tu.User)
        .Include(t => t.Owner)
        .Include(t => t.Boards)
        .FirstOrDefaultAsync(t => t.Id == id);

    async public Task Update(Team team) {
        _todoContext.Update(team);
        await _todoContext.SaveChangesAsync();
    }

  }
}