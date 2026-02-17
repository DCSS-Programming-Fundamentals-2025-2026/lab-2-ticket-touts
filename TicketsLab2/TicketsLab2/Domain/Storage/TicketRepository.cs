public class TicketRepository
{
    public Ticket[] SoldTickets = new Ticket[81];
    public int SoldTicketCounter = 0;

    public int AvailableTickets = 81;
    public string[,] AvailablePlaces = new string[9, 9];

    public TicketRepository()
    {
        for (int i = 0; i < AvailablePlaces.GetLength(0); i++)
        {
            for (int j = 0; j < AvailablePlaces.GetLength(1); j++)
            {
                int place = (i + 1) * 10 + j + 1;
                AvailablePlaces[i, j] = place.ToString();
            }
        }
    }

    public double CountTotalSum()
    {
        double totalSum = 0;

        for (int i = 0; i < SoldTicketCounter; i++)
        {
            totalSum += SoldTickets[i].BasePrice;
        }

        return totalSum;
    }

    public bool IsSellPossible()
    {
        if (AvailableTickets > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Ticket? FindTicketById(int id)
    {
        for (int i = 0; i < SoldTicketCounter; i++)
        {
            if (id == SoldTickets[i].Id)
            {
                return SoldTickets[i];
            }
        }

        return null;
    }

    public int FindTicketIndex(int id)
    {
        for (int i = 0; i < SoldTicketCounter; i++)
        {
            if (id == SoldTickets[i].Id)
            {
                return i;
            }
        }

        return -1;
    }

    public bool isPlaceAvailable(int place)
    {
        if (AvailablePlaces[(place / 10 - 1), (place % 10 - 1)] == "--")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool SellTicket(Ticket ticket)
    {
        if (FindTicketById(ticket.Id) == null && isPlaceAvailable(ticket.Place))
        {
            SoldTickets[SoldTicketCounter] = ticket;
            SoldTicketCounter++;
            AvailableTickets--;

            int place = ticket.Place;
            AvailablePlaces[(place / 10 - 1), (place % 10 - 1)] = "--";

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ReturnTicket(Ticket ticket)
    {
        int index = FindTicketIndex(ticket.Id);
        if (index == -1)
        {
            return false;
        }
        else
        {
            int place = ticket.Place;
            AvailablePlaces[(place / 10 - 1), (place % 10 - 1)] = place.ToString();

            AvailableTickets++;
            SoldTicketCounter--;
            for (int i = index; i < SoldTicketCounter; i++)
            {
                SoldTickets[i] = SoldTickets[i + 1];
            }
            SoldTickets[SoldTicketCounter] = null;

            return true;
        }
    }
}

