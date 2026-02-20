using System.Collections;
using TicketLab2.Domain.Storage;

namespace TicketLab2.Domain.Comparers
{
    public class XComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Ticket t1 = x as Ticket;
            Ticket t2 = y as Ticket;
            if (t1 == null || t2 == null)
                throw new System.ArgumentException("Порівнюються повинні бути Ticket");
            int cmp = t1.BasePrice.CompareTo(t2.BasePrice);
            return (cmp != 0) ? cmp : t1.Id.CompareTo(t2.Id);
        }
    }
}
