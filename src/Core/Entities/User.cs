using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema ;

namespace Core.Entities
{

    public class User : IdentityUser
    {

        public List<UserBoard> Boards { get; set; }


        [InverseProperty("Owner")]
        public List<Team> OwnedTeams { get; set; }

        [InverseProperty("Members")]
        public List<Team> MemberTeams { get; set; }
    }
}