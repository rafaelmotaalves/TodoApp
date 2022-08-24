using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Dtos
{
    public class TeamDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<TeamUserDto> Members { get; set; }

        public UserDto Owner { get; set; }

        public List<BoardDto> Boards { get; set; }
    }
}