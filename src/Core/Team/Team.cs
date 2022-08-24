namespace Core.Team
{
  using Core.Exceptions;
  using Core.User;
  using System.ComponentModel.DataAnnotations.Schema;

  public class Team
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string Name { get; set; }

    public List<TeamUser> Members { get; set; } = new List<TeamUser>();

    [InverseProperty("OwnedTeams")]
    [ForeignKey("OwnerId")]

    public User Owner { get; set; }
    public string OwnerId { get; set; }

    public List<TeamBoard> Boards { get; set; } = new List<TeamBoard>();

    public void AddMember(string userId, User user,  Permission permission)
    {
      if (!IsOwner(userId))
        throw new UnauthorizedException();

      if (IsMember(user.Id))
        return;

      var teamUser = new TeamUser { Permission = permission, User = user};
      Members.Add(teamUser);
    }

    public void AddBoard(string userId, TeamBoard teamBoard)
    {
      if (!CanWrite(userId))
        throw new UnauthorizedException();

      Boards.Add(teamBoard);
    }

    public bool CanWrite(string userId) => 
      IsOwner(userId)|| Members.Any(u => u.UserId == userId && u.Permission == Permission.ReadWrite);

    public bool IsMember(string userId) => IsOwner(userId) || Members.Any(u => u.UserId == userId);

    private bool IsOwner(string userId) =>  OwnerId == userId;
  }
}