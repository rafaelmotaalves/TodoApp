namespace Core.Entities
{
  public class UserBoard : Board
  {
    public String UserId { get; set; }

    public User User { get; set; }

    override public bool IsOwner(string ownerId) => this.UserId == ownerId;
  }
}