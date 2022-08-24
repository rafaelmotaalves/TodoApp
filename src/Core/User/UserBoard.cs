namespace Core.User
{
  using Core.Board;

  public class UserBoard : Board
  {
    public String UserId { get; set; }

    public User User { get; set; }

    override public bool CanWrite(string userId) => this.UserId == userId;

    public override bool CanRead(string userId)  => CanWrite(userId);
  }
}