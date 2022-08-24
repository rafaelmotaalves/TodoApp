namespace Core.Team
{
  using Core.Exceptions;
  using Core.User;

  public interface ITeamService
  {
    public Task Create(string ownerId, string name);
    public Task AddMember(string teamId, string ownerId, string memberId, Permission permission);

    public Task AddBoard(string teamId, string userId, string boardName);
  }

  public class TeamService : ITeamService
  {
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;

    public TeamService(ITeamRepository teamRepository, IUserRepository userRepository)
    {
      _teamRepository = teamRepository;
      _userRepository = userRepository;
    }

    async public Task Create(string ownerId, string name)
    {
      var team = new Team { Name = name, OwnerId = ownerId };

      await _teamRepository.Create(team);
    }

    async public Task AddMember(string teamId, string ownerId, string memberId, Permission permission)
    {
      var team = await _teamRepository.Get(teamId);
      if (team is null)
        throw new EntityNotFoundException();

      var newMember = await _userRepository.Get(memberId);
      if (newMember is null)
        throw new EntityNotFoundException();

      team.AddMember(ownerId, newMember, permission);
      await _teamRepository.Update(team);
    }

    async public Task AddBoard(string teamId, string userId, string boardName)
    {
      var team = await _teamRepository.Get(teamId);
      if (team is null)
        throw new EntityNotFoundException();
      
      var board = new TeamBoard { Name = boardName };
      team.AddBoard(userId, board);
      await _teamRepository.Update(team);
    }
  }
}