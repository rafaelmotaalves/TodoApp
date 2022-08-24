namespace Core.Team
{
    public interface ITeamRepository
    {
        public Task<List<Team>> GetAll();

        public Task Create(Team team);

        public Task<Team?> Get(string Id);

        public Task Update(Team team);

    }
}