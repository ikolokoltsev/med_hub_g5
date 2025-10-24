namespace App;

public class MenuItem
{
    public string Title;
    public Action Action;

    public MenuItem(string title, Action action)
    {
        Title = title;
        Action = action;
    }
}

public class Menu
{
    string _title;
    List<MenuItem> _items;
    int _selectedIndex;

    public Menu(string title, List<MenuItem> items)
    {
        _title = title;
        _items = items;
        _selectedIndex = 0;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.CursorVisible = false;

            Console.WriteLine(_title);

            for (int menu_item = 0; menu_item < _items.Count; menu_item++)
            {
                if (menu_item == _selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"  > {_items[menu_item].Title} <");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {_items[menu_item].Title}");
                }
            }

            Console.WriteLine("Use arrow up, and arrow down to navigate the menu, Enter to select and esc to exit.");

            ConsoleKeyInfo key_pressed = Console.ReadKey(true);

            switch (key_pressed.Key)
            {
                case ConsoleKey.UpArrow:
                    _selectedIndex = (_selectedIndex - 1 + _items.Count) % _items.Count;
                    break;
                case ConsoleKey.DownArrow:
                    _selectedIndex = (_selectedIndex + 1 + _items.Count) % _items.Count;
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    MenuItem selected_item = _items[_selectedIndex];
                    if (selected_item.Action != null)
                    {
                        selected_item.Action();
                    }
                    else
                    {
                        return;
                    }

                    break;
                case ConsoleKey.Escape:
                    return;
            }
        }
    }
}