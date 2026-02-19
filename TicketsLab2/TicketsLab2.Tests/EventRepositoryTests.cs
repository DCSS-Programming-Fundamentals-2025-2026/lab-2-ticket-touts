using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketsLab2.Tests;
public class EventRepositoryTests
{ 
    [Test]
    public void AddEvent()
    {
        // ARRANGE
        EventRepository eventRepo = new EventRepository();
        Event newEvent = new Event(101, "Test Concert", "Test Artist", "01.01.2025", 1, 500);

        // ACT
        bool result = eventRepo.Add(newEvent);

        // ASSERT
        Assert.That(result, Is.True);
        Event foundEvent = eventRepo.FindEventById(101);
        Assert.That(foundEvent, Is.Not.Null);
    }

    [Test]
    public void Add_DuplicateId()
    {
        // ARRANGE
        EventRepository eventRepo = new EventRepository();
        Event event1 = new Event(101, "E1", "A1", "01.01.2025", 1, 500);
        Event event2 = new Event(101, "E2", "A2", "02.02.2025", 2, 600);

        eventRepo.Add(event1);

        // ACT
        bool result = eventRepo.Add(event2);

        // ASSERT
        Assert.That(result, Is.False); 
        Assert.That(eventRepo.EventCounter, Is.EqualTo(1));
    }

    [Test]
    public void Add_SameDateAndHall_ShouldReturnFalse()
    {
        // ARRANGE
        EventRepository eventRepo = new EventRepository();
        Event event1 = new Event(1, "Morning Show", "A1", "01.01.2025", 1, 500);
        Event event2 = new Event(2, "Evening Show", "A2", "01.01.2025", 1, 600);

        eventRepo.Add(event1);

        // ACT
        bool result = eventRepo.Add(event2);

        // ASSERT
        Assert.That(result, Is.False);
    }

    [Test]
    public void DateFormatCheck_ShouldWorkCorrectly()
    { 
        Assert.That(ConsoleGUI.DateFormatCheck("01.01.2026"), Is.True);
        Assert.That(ConsoleGUI.DateFormatCheck("31.12.2026"), Is.True);

        Assert.That(ConsoleGUI.DateFormatCheck("32.01.2027"), Is.False); 
        Assert.That(ConsoleGUI.DateFormatCheck("00.01.2025"), Is.False);
        Assert.That(ConsoleGUI.DateFormatCheck("15.13.2028"), Is.False);
        Assert.That(ConsoleGUI.DateFormatCheck("01.00.2026"), Is.False);

        Assert.That(ConsoleGUI.DateFormatCheck("01/01/2029"), Is.False);
        Assert.That(ConsoleGUI.DateFormatCheck("1.1.2026"), Is.False); 
        
        Assert.That(ConsoleGUI.DateFormatCheck("abcdefghij"), Is.False);
        Assert.That(ConsoleGUI.DateFormatCheck(""), Is.False);
    }

    [Test]
    public void SortByIdChecking()
    {
        // ARRANGE
        EventRepository repo = new EventRepository();

        Event e3 = new Event(300, "Event C", "Artist C", "03.01.2025", 1, 100);
        Event e1 = new Event(100, "Event A", "Artist A", "01.01.2025", 1, 100);
        Event e2 = new Event(200, "Event B", "Artist B", "02.01.2025", 1, 100);

        repo.Add(e3);
        repo.Add(e1);
        repo.Add(e2);

        // ACT
        repo.SortById();

        // ASSERT
        Assert.That(repo.Events[0].Id, Is.EqualTo(100));
        Assert.That(repo.Events[1].Id, Is.EqualTo(200));
        Assert.That(repo.Events[2].Id, Is.EqualTo(300));
    }

    [Test]
    public void PrintAll_StatusFilterChecking()
    {
        // 1. ARRANGE
        EventRepository repo = new EventRepository();

        Event planned = new Event(1, "CheckPlanned", "Artist", "01.01.2025", 1, 100);
        planned.Status = EventStatus.Planned;

        Event ongoing = new Event(2, "CheckOngoing", "Artist", "02.01.2025", 1, 100);
        ongoing.Status = EventStatus.Ongoing;

        Event finished = new Event(3, "CheckFinished", "Artist", "03.01.2025", 1, 100);
        finished.Status = EventStatus.Finished;

        Event cancelled = new Event(4, "CheckCancelled", "Artist", "04.01.2025", 1, 100);
        cancelled.Status = EventStatus.Cancelled;

        repo.Add(planned);
        repo.Add(ongoing);
        repo.Add(finished);
        repo.Add(cancelled);

        // ACT
        string resultAll = repo.PrintAll(0);
        string resultForSale = repo.PrintAll(1);
        string resultForEdit = repo.PrintAll(2);

        // ASSERT
        Assert.That(resultAll, Does.Contain("CheckPlanned"));
        Assert.That(resultAll, Does.Contain("CheckOngoing"));
        Assert.That(resultAll, Does.Contain("CheckFinished"));
        Assert.That(resultAll, Does.Contain("CheckCancelled"));
            
            

        Assert.That(resultForSale, Does.Contain("CheckPlanned"));
        Assert.That(resultForSale, Does.Contain("CheckOngoing"));

        Assert.That(resultForSale, Does.Not.Contain("CheckFinished"));
        Assert.That(resultForSale, Does.Not.Contain("CheckCancelled"));



        Assert.That(resultForEdit, Does.Contain("CheckPlanned"));
        Assert.That(resultForEdit, Does.Contain("CheckCancelled"));

        Assert.That(resultForEdit, Does.Not.Contain("CheckOngoing"));
        Assert.That(resultForEdit, Does.Not.Contain("CheckFinished"));
    }

    [Test]
    public void EventStatusChangeChecking()
    {
        // ARRANGE
        Event ev = new Event(300, "Event C", "Artist C", "03.01.2025", 1, 100);

        // ACT
        ev.EventStatusChange(3);
        EventStatus status = ev.Status;

        // ASSERT
        Assert.That(status, Is.EqualTo(EventStatus.Finished));
    }

    [Test]
    public void SameDateHallChecking()
    {
        // ARRANGE
        EventRepository repo = new EventRepository();

        Event e1 = new Event(1, "Event A", "Artist A", "18.04.2026", 1, 100);
        Event e2 = new Event(2, "Event B", "Artist B", "18.04.2026", 1, 100);
        Event e3 = new Event(3, "Event C", "Artist C", "18.04.2026", 3, 100);

        repo.Add(e1);

        // ACT
        bool check1 = repo.isSameDateHall(e2);
        bool check2 = repo.isSameDateHall(e3);

        // ASSERT
        Assert.That(check1, Is.True);
        Assert.That(check2, Is.False);
    }

    [Test]
    public void SortByStatusChecking()
    {
        // ARRANGE
        EventRepository repo = new EventRepository();

        Event e1 = new Event(1, "Event A", "Artist A", "18.04.2026", 1, 100);
        Event e2 = new Event(2, "Event B", "Artist B", "22.04.2026", 2, 100);
        Event e3 = new Event(3, "Event C", "Artist C", "18.04.2026", 3, 100);

        repo.Add(e1);
        repo.Add(e2);
        repo.Add(e3);

        e2.EventStatusChange(4);

        // ACT
        string result = repo.SortByStatus(4);

        // ASSERT
        Assert.That(result, Is.EqualTo("ID події: 2 | Статус: Cancelled | " +
            $"Концерт: Event B | Виконавець: Artist B | " +
            $"Зала: 2 | Дата: 22.04.2026\n"));
    }
}
