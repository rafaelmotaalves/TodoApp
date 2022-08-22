namespace Core.Entities
{    
    public interface ITeamService {
        public Task Create(string ownerId, string name);
        public Task AddMember(string teamId, string ownerId, string memberId);
    }

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        public TeamService(ITeamRepository teamRepository, IUserRepository userRepository) {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        async public Task Create(string ownerId, string name) {
            var team = new Team { Name = name, OwnerId = ownerId };

            await _teamRepository.Create(team);
        }

        async public Task AddMember(string teamId, string ownerId, string memberId) {
            var team = await _teamRepository.Get(teamId);
            if (team is null)
                throw new EntityNotFoundException();

            var newMember = await _userRepository.Get(memberId);
            if (newMember is null)
                throw new EntityNotFoundException();

            team.AddMember(ownerId, newMember);
            await _teamRepository.Update(team);
        }

    }
}