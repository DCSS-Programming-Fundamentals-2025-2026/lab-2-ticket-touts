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
                ConsoleGUI.AddEvent(appState);
            }
            else if (choice == "2")
            {
                Console.WriteLine(appState.Events.PrintAll());
            }
            else if (choice == "3")
            {
                ConsoleGUI.ChangeEventStatus(appState);
            }
            else if (choice == "4")
            {
                ConsoleGUI.SearchEventById(appState);
            }
            else if (choice == "5")
            {
                ConsoleGUI.SortEventsByStatus(appState);
            }
            else if (choice == "6")
            {
                appState.Events.SortById();
                Console.WriteLine(appState.Events.PrintAll());
            }
            else if (choice == "7")
            {
                ConsoleGUI.SellTicketFromConsole(appState);
            }
            else if (choice == "8")
            {
                ConsoleGUI.ReturnTicketFromConsole(appState);
            }
            else if (choice == "9")
            {
                ConsoleGUI.ShowEventReport(appState);
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

