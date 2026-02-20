using System.Collections;
using TicketLab2.Domain.Storage;

namespace TicketLab2.Domain.Storage
{
    public class TicketCollection : IEnumerable
    {
        private Ticket[] items = new Ticket[5];
        private int count = 0;

        public bool Add(Ticket ticket)
        {
            if (ticket == null) return false;
            for (int i = 0; i < count; i++)
                if (items[i].Id == ticket.Id) return false;
            if (count >= items.Length) IncreaseCapacity();
            items[count++] = ticket;
            return true;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= count) return false;
            for (int i = index; i < count - 1; i++)
                items[i] = items[i + 1];
            count--;
            items[count] = null;
            return true;
        }

        public Ticket GetAt(int index)
        {
            return (index >= 0 && index < count) ? items[index] : null;
        }

        public bool SetAt(int index, Ticket ticket)
        {
            if (ticket == null) return false;
            if (index < 0 || index > count) return false;
            for (int i = 0; i < count; i++)
                if (items[i].Id == ticket.Id) return false;
            if (count >= items.Length) IncreaseCapacity();
            for (int i = count; i > index; i--)
                items[i] = items[i - 1];
            items[index] = ticket;
            count++;
            return true;
        }

        public int Count() { return count; }

        public IEnumerator GetEnumerator()
        {
            return new TicketEnumerator(items, count);
        }

        private void IncreaseCapacity()
        {
            Ticket[] newArr = new Ticket[items.Length + 10];
            items.CopyTo(newArr, 0);
            items = newArr;
        }
    }
}
