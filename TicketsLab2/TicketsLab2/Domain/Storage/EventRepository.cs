public class EventRepository
{
    public Event[] Events = new Event[50];
    public int EventCounter = 0;


    public Event? FindEventById(int id)
    {
        for (int i = 0; i < EventCounter; i++)
        {
            if (Events[i].Id == id)
            {
                return Events[i];
            }
        }

        return null;
    }

    public bool isSameDateHall(Event e)
    {
        for (int i = 0; i < EventCounter; i++)
        {
            if (Events[i].Date == e.Date && Events[i].HallNumber == e.HallNumber)
            {
                return true;
            }
        }

        return false;
    }

    public bool Add(Event newEvent)
    {
        if (EventCounter < Events.Length 
            && FindEventById(newEvent.Id) == null
            && !isSameDateHall(newEvent))
        {
            Events[EventCounter] = newEvent;
            EventCounter++;
            return true;
        }

        return false;
    }

    public string PrintAll(int condition = 0)
    {
        string result = "";

        for (int i = 0; i < EventCounter; i++)
        {
            if (condition == 0
                || (condition == 1 && Events[i].Status != EventStatus.Cancelled && Events[i].Status != EventStatus.Finished && Events[i].TicketsRepo.IsSellPossible())
                || (condition == 2 && Events[i].Status != EventStatus.Ongoing && Events[i].Status != EventStatus.Finished))
            {
                result += Events[i].Print();
                result += "\n";
            }
        }

        if (result == "")
        {
            result = "Подій ще немає.";
        }

        return result;
    }

    public string SortByStatus(int status)
    {
        string result = "";

        for (int i = 0; i < EventCounter; i++)
        {
            if (Events[i].Status == (EventStatus)status)
            {
                result += Events[i].Print();
                result += "\n";
            }
        }

        if (result == "")
        {
            result = "Подій з таким статусом немає.";
        }

        return result;
    }


    public void SortById()
    {
        for (int i = 0; i < EventCounter - 1; i++)
        {
            bool isSorted = true;

            for (int j = 0; j < EventCounter - i - 1; j++)
            {
                if (Events[j].Id > Events[j + 1].Id)
                {
                    Event temp = Events[j];
                    Events[j] = Events[j + 1];
                    Events[j + 1] = temp;

                    isSorted = false;
                }
            }

            if (isSorted)
            {
                break;
            }
        }
        if (EventCounter != 0)
        {
            Console.WriteLine("Події відсортовано за ID.");
        }       
    }

    public string AllEventReport()
    {
        string result = "";
        double totalSum = 0;
        int totalTickets = 0;

        for (int i = 0; i < EventCounter; i++)
        {
            totalSum += Events[i].TicketsRepo.CountTotalSum();
            totalTickets += Events[i].TicketsRepo.SoldTicketCounter;
            result += $"ID події: {Events[i].Id}\n";
            result += Events[i].EventReport();
            result += "\n";
        }

        result += "\n";
        result += $"Всього продано квитків: {totalTickets}\n";
        result += $"Всього зароблено коштів: {totalSum} грн";

        return result;
    }
}

