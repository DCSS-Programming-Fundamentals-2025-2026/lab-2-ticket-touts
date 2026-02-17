public class RegularTicket : Ticket
{
    public RegularTicket(int id, int eventId, int place, double basePrice)
    {
        Id = id;
        EventId = eventId;
        Place = place;
        Type = TicketType.Regular;
        SetPrice(basePrice);
    }

    public override void SetPrice(double basePrice)
    {
        BasePrice = basePrice;
    }
}

