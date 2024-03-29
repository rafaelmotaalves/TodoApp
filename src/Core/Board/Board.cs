namespace Core.Board;

using Core.Exceptions;

abstract public class Board
{

  public int Id { get; set; }
  public String? Name { get; set; }

  public List<Column> Columns { get; set; } = new List<Column>();


  public abstract bool CanWrite(string userId);

  public abstract bool CanRead(string userId);

  public void AddColumn(string ownerId, Column column)
  {
    if (!CanWrite(ownerId))
      throw new UnauthorizedException();

    if (Columns.Any(c => c.Name.Equals(column.Name)))
      return;

    Columns.Add(column);
  }

  public void AddCard(string ownerId, int columnId, Card card)
  {
    if (!CanWrite(ownerId))
      throw new UnauthorizedException();
    
    var column = Columns.FirstOrDefault(c => c.Id == columnId);
    if (column is null)
      throw new EntityNotFoundException();

    column.AddCard(card);
  }

  public void MoveCard(string ownerId, int columnId, int newColumnId, int cardId)
  {
    if (!CanWrite(ownerId))
      throw new UnauthorizedException();
    
    if (columnId == newColumnId)
      return;

    var oldColumn = Columns.FirstOrDefault(c => c.Id == columnId);
    if (oldColumn is null)
      throw new EntityNotFoundException();

    var card = oldColumn.GetCard(cardId);
    if (card is null)
      throw new EntityNotFoundException();

    var newColumn = Columns.FirstOrDefault(c => c.Id == newColumnId);
    if (newColumn is null)
      throw new EntityNotFoundException();

    newColumn.AddCard(card);
    // no need to remove from other column cause EF will handle that
    // but maybe we should remove it so that this part is EF independent
  }

}