public class Event : IPrintable
{
    public int Id { get; set; }
    public string ConcertName { get; set; }
    public string ArtistName { get; set; }
    public int HallNumber { get; set; }
    public string Date { get; set; }
    public EventStatus Status { get; set; }
    public double BasePrice { get; set; }
    public TicketRepository TicketsRepo = new TicketRepository();

    public Event(int id, string concert, string artist, string date, int hall, double basePrice)
    {
        Id = id;
        ConcertName = concert;
        ArtistName = artist;
        HallNumber = hall;
        Date = date;
        BasePrice = basePrice;
        Status = EventStatus.Planned;
    }

    public void EventStatusChange(int status)
    {
        Status = (EventStatus)status;
    }

    public string Print()
    {
        return $"ID події: {Id} | Статус: {Status} | " +
            $"Концерт: {ConcertName} | Виконавець: {ArtistName} | " +
            $"Зала: {HallNumber} | Дата: {Date}";
    }

    public string EventReport()
    {
        double totalSum = TicketsRepo.CountTotalSum();
        return $"Продано квитків: {TicketsRepo.SoldTicketCounter} | " +
            $"Зароблено коштів: {totalSum} грн";
    }
}
