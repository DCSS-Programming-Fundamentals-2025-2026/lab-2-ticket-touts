namespace TicketsLab2.Tests;
public class ScenarioTests
{
    [Test]
    public void Scenario_Sell_Return_StatusChanging_CheckAvailable()
    {
        // ARRANGE
        Event myEvent = new Event(600, "Return Show", "Artist B", "25.02.2025", 1, 200);
        RegularTicket ticket = new RegularTicket(10, 600, 55, myEvent.BasePrice);

        // ACT 
        bool sold = myEvent.TicketsRepo.SellTicket(ticket);

        bool returned = myEvent.TicketsRepo.ReturnTicket(ticket);

        myEvent.Status = EventStatus.Finished;

        // ASSERT
        Assert.That(returned, Is.True);
        Assert.That(myEvent.TicketsRepo.SoldTicketCounter, Is.EqualTo(0));
        Assert.That(myEvent.TicketsRepo.isPlaceAvailable(55), Is.True);
        Assert.That(myEvent.Status, Is.EqualTo(EventStatus.Finished));
    }

    [Test]
    public void Scenario_CreateEvent_SellTickets_CheckEventReport()
    {
        // ARRANGE
        Event myEvent = new Event(500, "Event A", "Artist A", "20.02.2025", 1, 1000);
        
        RegularTicket t1 = new RegularTicket(1, 500, 11, myEvent.BasePrice);
        VIPTicket t2 = new VIPTicket(2, 500, 12, myEvent.BasePrice);
        
        t1.SetPrice(myEvent.BasePrice);
        t2.SetPrice(myEvent.BasePrice);

        // ACT
        myEvent.TicketsRepo.SellTicket(t1); 
        myEvent.TicketsRepo.SellTicket(t2); 

        string report = myEvent.EventReport();
        double totalSum = myEvent.TicketsRepo.CountTotalSum();

        // ASSERT
        Assert.That(myEvent.TicketsRepo.SoldTicketCounter, Is.EqualTo(2));
        Assert.That(totalSum, Is.EqualTo(2300));
    }

    [Test]
    public void Scenario_CreateEvent_SellTickets_CheckTotalReport()
    {
        // ARRANGE
        EventRepository repo = new EventRepository();

        Event event1 = new Event(1, "Show 1", "Artist 1", "01.01.2025", 1, 100);
        repo.Add(event1);

        Event event2 = new Event(2, "Show 2", "Artist 2", "02.01.2025", 1, 200);
        repo.Add(event2);

        event1.TicketsRepo.SellTicket(new RegularTicket(1, 1, 11, 100));
        event1.TicketsRepo.SellTicket(new RegularTicket(2, 1, 12, 100));
        event2.TicketsRepo.SellTicket(new RegularTicket(3, 2, 11, 200));

        // ACT
        string report = repo.AllEventReport();

        // ASSERT            
        Assert.That(report, Does.Contain("Всього продано квитків: 3"));
        Assert.That(report, Does.Contain("Всього зароблено коштів: 400"));
    }
}