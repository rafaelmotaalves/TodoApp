using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core.Entities;

namespace Infra.Data;

public class TodoContext :  IdentityDbContext<User>
{

  public virtual DbSet<Board> Boards { get; set; }
  public virtual DbSet<UserBoard> UserBoards { get; set; }

  public virtual DbSet<Team> Teams  { get; set; }

  public TodoContext(DbContextOptions options) : base(options)
  {
    Database.EnsureCreated();
  }

}