public abstract class Ticket : IReceiptLine
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public TicketType Type { get; set; }

    public int Place { get; set; }

    public double BasePrice { get; set; }

    public abstract void SetPrice(double basePrice);

    public virtual string ToReceiptLine(string clientInfo)
    {
        return $"ID квитка: {Id} | Місце: {Place} | " +
            $"ID події: {EventId} | " +
            $"Ціна: {BasePrice} грн | " +
            $"Покупець: {clientInfo}";
    }

}
