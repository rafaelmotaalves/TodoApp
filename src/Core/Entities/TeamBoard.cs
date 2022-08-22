namespace Core.Entities
{
  public class TeamBoard : Board
  {
    public string TeamId { get; set; }
    public Team Team { get; set; }

    public override bool IsOwner(string userId) {
      return Team.OwnerId == userId || Team.Members.Any(u => u.Id == userId);
    }
  }
}