
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Util;

public class DatabaseFixture
{

  public TodoContext TodoContext { get; }


  public DatabaseFixture()
  {
    var dbContextOptions = new DbContextOptionsBuilder()
      .UseInMemoryDatabase(databaseName: "TestDatabase")
      .Options;
    TodoContext = new TodoContext(dbContextOptions);
  }

  public void Dispose()
  {
    TodoContext.Database.EnsureDeleted();
    TodoContext.Database.EnsureCreated();
  }

}