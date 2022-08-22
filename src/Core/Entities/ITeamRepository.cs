using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface ITeamRepository
    {
        public Task<List<Team>> GetAll();

        public Task Create(Team team);

        public Task<Team?> Get(string Id);

        public Task Update(Team team);

    }
}