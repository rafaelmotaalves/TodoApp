namespace Api.Controllers.Dtos
{
    using Core.Team;

    public class AddMemberDto
    {
        public string UserId { get; set; }

        public Permission Permission { get; set; }
    }
}