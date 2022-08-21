using Core.Entities;

namespace UnitTests.Core.Entities;

public class BoardTests
{

  [Fact]
  public void AddColumn_Should_Add_A_Column()
  {
    // given
    var board = new UserBoard { Name = "Board", UserId = "user_id" };

    // when
    board.AddColumn("user_id", new Column { Name = "Column" });
    // then
    Assert.Collection(board.Columns, c => c.Name.Equals("Column"));
  }

  [Fact]
  public void AddColumn_Should_Not_Add_A_New_Column_If_There_Is_Some_With_The_Same_Name()
  {
    // given
    var board = new UserBoard { Name = "Board", Columns = new List<Column>() { new Column { Name = "Column" } }, UserId = "user_id" };

    // when
    board.AddColumn("user_id", new Column { Name = "Column" });

    // then
    Assert.Collection(board.Columns, c => c.Name.Equals("Column"));
    Assert.Equal(board.Columns.Count, 1);
  }

}