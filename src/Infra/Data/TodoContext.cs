using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infra.Data;

public class TodoContext : DbContext
{

  public virtual DbSet<Board> Boards { get; set; }

  public TodoContext(DbContextOptions options) : base(options)
  {
    Database.EnsureCreated();
  }

  // protected override void OnConfiguring(DbContextOptionsBuilder options)
  //   => options.UseSqlite($"Data Source=todo.db");
}