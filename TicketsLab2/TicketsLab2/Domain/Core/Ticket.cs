public abstract class Ticket : IComparable
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public TicketType Type { get; set; }

    public int Place { get; set; }

    public double BasePrice { get; set; }

    public abstract void SetPrice(double basePrice);
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        Ticket other = obj as Ticket;
        if (other == null)
            throw new ArgumentException("Об'єкт має бути Ticket");
        return this.Id.CompareTo(other.Id);
    }
    public virtual string ToReceiptLine(string clientInfo)
    {
        return $"ID квитка: {Id} | Місце: {Place} | " +
            $"ID події: {EventId} | " +
            $"Ціна: {BasePrice} грн | " +
            $"Покупець: {clientInfo}";
    }

}
