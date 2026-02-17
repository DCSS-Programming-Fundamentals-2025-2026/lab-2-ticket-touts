public static class ConsoleGUI
{
    public static void AddEvent(AppState appState)
    {
        Console.Write("Введіть числове ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Введіть назву концерту: ");
        string name = Console.ReadLine();
        Console.Write("Введіть назву виконавця: ");
        string artist = Console.ReadLine();
        Console.Write("Введіть номер зали: ");
        int hall = int.Parse(Console.ReadLine());
        Console.Write("Введіть дату проведення події(дд.мм.рррр): ");
        string date = Console.ReadLine();
        Console.Write("Введіть ціну на квитки: ");
        double price = double.Parse(Console.ReadLine());

        Event newEvent = new Event(id, name, artist, date, hall, price);

        if (appState.Events.Add(newEvent))
        {
            Console.WriteLine("Подію успішно додано!");
        }
        else
        {
            Console.WriteLine("Не вдалося додати подію.");
            Console.WriteLine("Можливо подія з таким ID вже існує.");
        }
    }

    public static void ChangeEventStatus(AppState appState)
    {
        Console.Write("Введіть ID події: ");
        int id = int.Parse(Console.ReadLine());

        Event temp = appState.Events.FindEventById(id);
        if (temp != null)
        {
            Console.WriteLine();
            Console.WriteLine("Оберіть новий статус події: ");
            Console.WriteLine("1) Запланована");
            Console.WriteLine("2) Відбувається");
            Console.WriteLine("3) Завершилася");
            Console.WriteLine("4) Скасована");
            Console.WriteLine();
            Console.Write("Ваш вибір: ");

            int newStatus = int.Parse(Console.ReadLine());
            temp.EventStatusChange(newStatus);
        }
        else
        {
            Console.WriteLine("Подію не знайдено.");
        }
    }

    public static void SearchEventById(AppState appState)
    {
        Console.Write("Введіть ID події: ");
        int id = int.Parse(Console.ReadLine());

        Event temp = appState.Events.FindEventById(id);
        if (temp != null)
        {
            Console.WriteLine(temp.Print());
        }
        else
        {
            Console.WriteLine("Подію не знайдено.");
        }
    }

    public static void SortEventsByStatus(AppState appState)
    {
        Console.WriteLine("Оберіть події якого статусу вас цікавлять: ");
        Console.WriteLine("1) Запланована");
        Console.WriteLine("2) Відбувається");
        Console.WriteLine("3) Завершилася");
        Console.WriteLine("4) Скасована");
        Console.WriteLine();
        Console.Write("Ваш вибір: ");

        int status = int.Parse(Console.ReadLine());
        Console.WriteLine(appState.Events.SortByStatus(status));
    }

    public static void SellTicketFromConsole(AppState appState)
    {
        Console.WriteLine(appState.Events.PrintAll(1));
        Console.Write("Введіть ID події, на яку хочете придбати квиток: ");
        int eventId = int.Parse(Console.ReadLine());

        Event temp = appState.Events.FindEventById(eventId);

        if (temp != null)
        {
            if (temp.Status == EventStatus.Finished || temp.Status == EventStatus.Cancelled)
            {
                Console.WriteLine("Продаж квитків на цю подію неможливий, оскільки вона закінчилася або скасована.");
                return;
            }

            Console.Write("Введіть ID квитка: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введіть тип квитка (1 - Звичайний, 2 - Дитячий, 3 - Пільговий, 4-VIP): ");
            int type = int.Parse(Console.ReadLine());

            Console.WriteLine("Схема залу:");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    string seat = temp.TicketsRepo.AvailablePlaces[i, j];
                    Console.Write($"[{seat}] ");
                }
                Console.WriteLine();
            }
            Console.Write("Введіть номер місця: ");
            int place = int.Parse(Console.ReadLine());

            Ticket newTicket;

            if (type == 2)
            {
                newTicket = new ChildTicket(id, eventId, place, temp.BasePrice);
            }
            else if (type == 3)
            {
                newTicket = new PreferentialTicket(id, eventId, place, temp.BasePrice);
            }
            else if (type == 4)
            {
                newTicket = new VIPTicket(id, eventId, place, temp.BasePrice);
            }
            else
            {
                newTicket = new RegularTicket(id, eventId, place, temp.BasePrice);
            }

            if (temp.TicketsRepo.SellTicket(newTicket))
            {
                Console.Write("Введіть ім'я клієнта: ");
                string client = Console.ReadLine();

                Console.WriteLine("Квиток успішно продано!");
                Console.WriteLine(newTicket.ToReceiptLine(client));
            }
            else
            {
                Console.WriteLine("Продаж неможливий. Можливо обране місце зайняте або квиток з таким ID вже існує.");
            }
        }
        else
        {
            Console.WriteLine("Подію не знайдено.");
        }
    }

    public static void ReturnTicketFromConsole(AppState appState)
    {
        Console.WriteLine(appState.Events.PrintAll(2));
        Console.Write("Введіть ID події, квиток на яку хочете повернути: ");
        int eventId = int.Parse(Console.ReadLine());

        Event tempEvent = appState.Events.FindEventById(eventId);

        if (tempEvent != null)
        {
            if (tempEvent.Status == EventStatus.Finished || tempEvent.Status == EventStatus.Ongoing)
            {
                Console.WriteLine("Повернення квитків неможливе, оскільки подія вже почалася або закінчилася.");
                return;
            }

            Console.Write("Введіть ID квитка: ");
            int id = int.Parse(Console.ReadLine());

            Ticket tempTicket = tempEvent.TicketsRepo.FindTicketById(id);

            if (tempTicket != null)
            {
                if (tempEvent.TicketsRepo.ReturnTicket(tempTicket))
                {
                    Console.WriteLine("Операція успішна\n" +
                        $"Сума повернення: {tempTicket.BasePrice} грн");
                }
                else
                {
                    Console.WriteLine("Виникла помилка під час повернення квитка");
                }
            }
            else
            {
                Console.WriteLine("Такого квитка не знайдено!");
            }
        }
        else
        {
            Console.WriteLine("Подію не знайдено.");
        }
    }

    public static void ShowEventReport(AppState appState)
    {
        Console.Write("Введіть ID події: ");
        int id = int.Parse(Console.ReadLine());

        Event temp = appState.Events.FindEventById(id);
        if (temp != null)
        {
            Console.WriteLine(temp.EventReport());
        }
        else
        {
            Console.WriteLine("Подію не знайдено.");
        }
    }
}

