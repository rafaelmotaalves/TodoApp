namespace Core.Board;

public class Column
{
  public int Id { get; set; }

  public string Name { get; set; }

  public List<Card> Cards { get; set; } = new List<Card>();

  public void AddCard(Card card)
  {
    if (Cards.Any(t => t.Name.Equals(card.Name)))
      return;

    Cards.Add(card);
  }

  public Card? GetCard(int cardId)
  {
    return Cards.FirstOrDefault(t => t.Id == cardId);
  }
}