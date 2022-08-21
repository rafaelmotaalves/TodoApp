using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{

    public class User : IdentityUser
    {

        public List<UserBoard> Boards { get; set; }
    }
}