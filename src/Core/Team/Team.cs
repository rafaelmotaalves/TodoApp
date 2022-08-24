namespace Core.Team
{
  using Core.Board;
  using Core.Exceptions;
  using Core.User;
  using System.ComponentModel.DataAnnotations.Schema;

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

    public List<TeamBoard> Boards { get; set; } = new List<TeamBoard>();

    public void AddMember(string userId, User user)
    {
      if (OwnerId != userId)
        throw new UnauthorizedException();

      if (IsMember(user.Id))
        return;

      Members.Add(user);
    }

    public void AddBoard(string userId, TeamBoard teamBoard)
    {
      if (!IsMember(userId))
        throw new UnauthorizedException();

      Boards.Add(teamBoard);
    }

    public bool IsMember(string userId)
    {
      return OwnerId == userId || Members.Any(u => u.Id == userId);
    }
  }
}