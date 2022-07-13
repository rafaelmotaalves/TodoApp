namespace UnitTests.Util;

public abstract class DatabaseTests : IDisposable
{
  protected DatabaseFixture fixture;

  public DatabaseTests()
  {
    fixture = new DatabaseFixture();
  }

  public void Dispose()
  {
    fixture.Dispose();
  }
}