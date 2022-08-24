namespace Core.Team
{
    using Core.User;
    public class TeamUser
    {
        public int Id { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public Team Team { get; set; }
        public string TeamId { get; set; }

        public Permission Permission { get; set; }
    }
}