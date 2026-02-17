public class PreferentialTicket : Ticket
{
    public PreferentialTicket(int id, int eventId, int place, double basePrice)
    {
        Id = id;
        EventId = eventId;
        Place = place;
        Type = TicketType.Preferential;
        SetPrice(basePrice);
    }

    public override void SetPrice(double basePrice)
    {
        BasePrice = basePrice * 0.6;
    }

    public override string ToReceiptLine(string clientInfo)
    {
        return $"ПІЛЬГОВИЙ КВИТОК\n" +
            $"ID квитка: {Id} | Місце: {Place} | " +
            $"ID події: {EventId} | " +
            $"Ціна: {BasePrice} грн | " +
            $"Покупець: {clientInfo}";
    }
}

