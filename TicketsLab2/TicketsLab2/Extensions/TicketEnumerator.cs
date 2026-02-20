using System.Collections;
using TicketLab2.Domain.Storage;

namespace TicketLab2.Domain.Storage
{
    public class TicketEnumerator : IEnumerator
    {
        private Ticket[] items;
        private int position = -1;
        private int count;

        public TicketEnumerator(Ticket[] items, int count)
        {
            this.items = items;
            this.count = count;
        }

        public bool MoveNext()
        {
            position++;
            return (position < count);
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get
            {
                if (position < 0 || position >= count)
                    throw new System.InvalidOperationException();
                return items[position];
            }
        }
    }
}
