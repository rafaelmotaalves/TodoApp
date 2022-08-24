namespace Core.Team
{
  using Core.Board;

  public class TeamBoard : Board
  {
    public string TeamId { get; set; }
    public Team Team { get; set; }

    public override bool CanWrite(string userId) => Team != null && Team.CanWrite(userId);

    public override bool CanRead(string userId) => Team != null && Team.IsMember(userId);
  }
}