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

  public void AddTask(int columnId, Task task)
  {
    var column = Columns.FirstOrDefault(c => c.Id == columnId);
    if (column is null)
      throw new EntityNotFoundException();

    column.AddTask(task);
  }

  public void MoveTask(int columnId, int newColumnId, int taskId)
  {
    if (columnId == newColumnId)
      return;

    var oldColumn = Columns.FirstOrDefault(c => c.Id == columnId);
    if (oldColumn is null)
      throw new EntityNotFoundException();

    var task = oldColumn.GetTask(taskId);
    if (task is null)
      throw new EntityNotFoundException();

    var newColumn = Columns.FirstOrDefault(c => c.Id == newColumnId);
    if (newColumn is null)
      throw new EntityNotFoundException();

    newColumn.AddTask(task);
    // no need to remove from other column cause EF will handle that
    // but maybe we should remove it so that this part is EF independent
  }

}