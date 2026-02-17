public class DemoRunner
{
    public void Run()
    {
        AppState appState = new AppState();

        while (true)
        {
            Menu menu = new Menu();
            menu.ShowMainMenu();

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                try
                {
                    ConsoleGUI.AddEvent(appState);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Притримуйтесь формату вводу.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка.");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine(appState.Events.PrintAll());
            }
            else if (choice == "3")
            {
                try
                {
                    ConsoleGUI.ChangeEventStatus(appState);
                    Console.WriteLine("Статус успішно змінено!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Введіть числове значення.");
                }
                catch (WrongChoiceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка.");
                }
            }
            else if (choice == "4")
            {
                try
                {
                    ConsoleGUI.SearchEventById(appState);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Введіть числове значення.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка.");
                }             
            }
            else if (choice == "5")
            {
                try
                {
                    ConsoleGUI.SortEventsByStatus(appState);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Оберіть одне з запропонованих значень.");
                }
                catch (WrongChoiceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка.");
                }
            }
            else if (choice == "6")
            {
                appState.Events.SortById();
                Console.WriteLine(appState.Events.PrintAll());
            }
            else if (choice == "7")
            {
                try
                {
                    ConsoleGUI.SellTicketFromConsole(appState);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Введіть числове значення.");
                }
                catch (WrongChoiceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка.");
                }
            }
            else if (choice == "8")
            {
                try
                {
                    ConsoleGUI.ReturnTicketFromConsole(appState);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Введіть числове значення.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка.");
                }
            }
            else if (choice == "9")
            {
                try
                {
                    ConsoleGUI.ShowEventReport(appState);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Введіть числове значення.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка.");
                }
            }
            else if (choice == "10")
            {
                Console.WriteLine(appState.Events.AllEventReport());
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Невідомий вибір.");
            }
        }
    }
}

