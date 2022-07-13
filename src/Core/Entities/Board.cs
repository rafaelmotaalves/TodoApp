namespace Core.Entities;

public class Board
{

  public int Id { get; set; }
  public String? Name { get; set; }

  public List<Column> Columns { get; set; } = new List<Column>();

  public void AddColumn(Column column)
  {
    if (Columns.Any(c => c.Name.Equals(column.Name)))
      return;

    Columns.Add(column);
  }
}