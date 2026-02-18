namespace TicketsLab2.Tests;


public class TicketRepositoryTests
{
    
    [Test]
    public void SellTicket()
    {
        // ARRANGE
        TicketRepository repo = new TicketRepository();
        RegularTicket ticket = new RegularTicket(1, 100, 11, 500); 
        int initialAvailable = repo.AvailableTickets;

        // ACT
        bool result = repo.SellTicket(ticket);

        // ASSERT
        Assert.That(result, Is.True);
        Assert.That(repo.AvailableTickets, Is.EqualTo(initialAvailable - 1));
        Assert.That(repo.isPlaceAvailable(11), Is.False);
    }

    [Test]
    public void SellTicket_DublicatePlace()
    {
        // ARRANGE
        TicketRepository repo = new TicketRepository();
        RegularTicket ticket1 = new RegularTicket(1, 100, 15, 500);
        RegularTicket ticket2 = new RegularTicket(2, 100, 15, 500); 

        repo.SellTicket(ticket1);

        // ACT
        bool result = repo.SellTicket(ticket2);

        // ASSERT
        Assert.That(result, Is.False);
        Assert.That(repo.SoldTicketCounter, Is.EqualTo(1));
    }

    [Test]
    public void ReturnTicket_ArrayChecking()
    {
        // ARRANGE
        TicketRepository repo = new TicketRepository();
        RegularTicket t1 = new RegularTicket(1, 100, 11, 100);
        RegularTicket t2 = new RegularTicket(2, 100, 12, 100); 
        RegularTicket t3 = new RegularTicket(3, 100, 13, 100);

        repo.SellTicket(t1);
        repo.SellTicket(t2);
        repo.SellTicket(t3);

        // ACT
        bool result = repo.ReturnTicket(t2);

        // ASSERT
        Assert.That(result, Is.True);
        Assert.That(repo.SoldTicketCounter, Is.EqualTo(2));
        
        Assert.That(repo.SoldTickets[0].Id, Is.EqualTo(1));
        Assert.That(repo.SoldTickets[1].Id, Is.EqualTo(3)); 
        Assert.That(repo.SoldTickets[2], Is.Null);
    }

    [Test]
    public void CountEventSumChecking()
    {
        // ARRANGE
        TicketRepository repo = new TicketRepository();
        repo.SellTicket(new RegularTicket(1, 1, 11, 100)); 
        repo.SellTicket(new RegularTicket(2, 1, 12, 200));     

        // ACT
        double sum = repo.CountTotalSum();

        // ASSERT
        Assert.That(sum, Is.EqualTo(300));
    }
}
