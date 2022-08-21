using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core.Entities;

namespace Infra.Data;

public class TodoContext :  IdentityDbContext<IdentityUser>
{

  public virtual DbSet<Board> Boards { get; set; }

  public TodoContext(DbContextOptions options) : base(options)
  {
    Database.EnsureCreated();
  }


  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
  }

  // protected override void OnConfiguring(DbContextOptionsBuilder options)
  //   => options.UseSqlite($"Data Source=todo.db");
}