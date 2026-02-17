public class ChildTicket : Ticket
{
    public ChildTicket(int id, int eventId, int place, double basePrice)
    {
        Id = id;
        EventId = eventId;
        Place = place;
        Type = TicketType.Child;
        SetPrice(basePrice);
    }

    public override void SetPrice(double basePrice)
    {
        BasePrice = basePrice * 0.8;
    }

    public override string ToReceiptLine(string clientInfo)
    {
        return $"ДИТЯЧИЙ КВИТОК\n" +
            $"ID квитка: {Id} | Місце: {Place} | " +
            $"ID події: {EventId} | " +
            $"Ціна: {BasePrice} грн | " +
            $"Покупець: {clientInfo}";
    }
}

