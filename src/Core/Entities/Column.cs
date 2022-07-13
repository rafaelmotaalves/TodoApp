namespace Core.Entities;

public class Column
{
  public int Id { get; set; }

  public string Name { get; set; }

  public List<Task> Tasks { get; set; } = new List<Task>();

  public void AddTask(Task task)
  {
    if (Tasks.Any(t => t.Name.Equals(task.Name)))
      return;

    Tasks.Add(task);
  }

  public Task? GetTask(int taskId)
  {
    return Tasks.FirstOrDefault(t => t.Id == taskId);
  }
}