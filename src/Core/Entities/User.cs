using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {

        public List<Board> Boards { get; set; }
    }
}