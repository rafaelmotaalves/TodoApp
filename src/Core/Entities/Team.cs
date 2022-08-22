namespace Core.Entities
{
    using System.ComponentModel.DataAnnotations.Schema ;


    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }


        [InverseProperty("MemberTeams")]
        public List<User> Members { get; set; } = new List<User>();


        [InverseProperty("OwnedTeams")]
        [ForeignKey("OwnerId")]

        public User Owner { get; set; }
        public string OwnerId { get; set; }


        public void AddMember(string ownerId, User user) {
            if (OwnerId != ownerId)
                throw new UnauthorizedException();

            if (Members.Any(u => u.UserName == user.UserName))
                return;

            Members.Add(user);
        }
    }
}