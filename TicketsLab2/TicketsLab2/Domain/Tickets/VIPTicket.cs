public class VIPTicket : Ticket
{
    public VIPTicket(int id, int eventId, int place, double basePrice)
    {
        Id = id;
        EventId = eventId;
        Place = place;
        Type = TicketType.VIP;
        SetPrice(basePrice);
    }

    public override void SetPrice(double basePrice)
    {
        BasePrice = basePrice * 1.3;
    }

    public override string ToReceiptLine(string clientInfo)
    {
        return $"=====VIP КВИТОК=====\n" +
            $"ID квитка: {Id} | Місце: {Place} | " +
            $"ID події: {EventId} | " +
            $"Ціна: {BasePrice} грн | " +
            $"Покупець: {clientInfo}";
    }
}

