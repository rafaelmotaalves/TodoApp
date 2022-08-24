namespace Core.Entities
{
  public class TeamBoard : Board
  {
    public string TeamId { get; set; }
    public Team Team { get; set; }

    public override bool IsOwner(string userId) => Team != null && Team.IsMember(userId);
  }
}