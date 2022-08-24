namespace Api.Controllers.Dtos
{
    using Core.Team;

    public class TeamUserDto
    {
        public UserDto User { get; set; }

        public Permission Permission { get; set; }
    }
}