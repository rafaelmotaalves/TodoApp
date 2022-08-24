namespace Core.User
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core.Board;
    using Core.Team;

    public class User : IdentityUser
    {

        public List<UserBoard> Boards { get; set; }


        [InverseProperty("Owner")]
        public List<Team> OwnedTeams { get; set; }

        public List<TeamUser> MemberTeams { get; set; }
    }
}