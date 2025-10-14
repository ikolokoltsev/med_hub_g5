namespace IOUtilsApp;

public static class IOUtils
{
    public static string StringUserInput()
    {
        string? input = Console.ReadLine();
        if (input != null && input.GetType() == typeof(string))
        {
            return input;
        }
        else if (input != null)
        {
            Type type = input.GetType();
            Console.WriteLine($"Unexpected type {type} for text input");
            return "";
        }
        else
        {
            Console.WriteLine($"There is a null in text input");
            return "";
        }
    }
    
    public static int IntUserInput()
    {
        string input = StringUserInput();
        try
        {
            int parsed_input = int.Parse(input);
            return parsed_input;
        }
        catch (FormatException error)
        {
            Console.WriteLine($"Ohhhh... Wrong format {error.Message}");
        }
        catch (OverflowException error)
        {
            Console.WriteLine($"Are you kidding me? {error.Message}");
        }
        catch (ArgumentNullException error)
        {
            Console.WriteLine($"That's not even funny! {error.Message}");
        }

        return -1;
    }
    
    public static string StringUserInputWithCleanUp()
    {
        string input = StringUserInput();
        Console.Clear();
        return input;
    }
    
    public static int IntUserInputWithCleanUp()
    {
        int input = IntUserInput();
        Console.Clear();
        return input;
    }
    
    public static void ColorizedPrint(string print_message, ConsoleColor foreground_color = ConsoleColor.White,
        object background_color = null)
    {
        if (background_color is ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        Console.ForegroundColor = foreground_color;
        Console.WriteLine(print_message);
        Console.ResetColor();
    }

    // Doesnt work yet
    // TODO: MAKE IT WORK
    public static string PasswordInput()
    {
        string password_acc = "";
        ConsoleKeyInfo key_pressed;

        while (true)
        {
            key_pressed = Console.ReadKey(true);
            if (key_pressed.Equals(ConsoleKey.Enter))
            {
                break;
            }
            else if (key_pressed.Equals(ConsoleKey.Backspace))
            {
                if (password_acc.Length > 0)
                {
                    password_acc = password_acc.Substring(0, password_acc.Length - 1);
                }
            }
            else
            {
                password_acc += key_pressed.KeyChar;
            }
        }

        return password_acc;
    }
}