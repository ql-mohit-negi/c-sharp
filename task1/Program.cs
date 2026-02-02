// See https://aka.ms/new-console-template for more information

class User
{
    public string Name
    {
        get; set;
    }

    public int Health
    {
        get; set;
    }

    public User(string name, int health)
    {
        Name = name;
        Health = health;
    }
}

class Game
{
    User user;
    static Random rnd;
    public Game ()
    {
        Console.Write("<--- Welcome to RPGBattler --->\nPlease provide your name to continue >> ");
        string? name = Console.ReadLine();
        while(name.Length<3)
        {
            PrintInRed("Please enter a valid name. Name must be at least 3 characters long.\n");
            Console.Write("Please provide your name to continue >> ");
            name = Console.ReadLine();
        }

        user = new User(name, 100);
        rnd = new Random();
    }

    static void PrintStatus(User u)
    {
        Console.Write("Current status: ");
        Console.WriteLine($"Health: {u.Health}\n\n");
    }

    static void PrintInRed(string mssg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(mssg);
        Console.ResetColor();
    }

    static void PrintInGreen(string mssg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(mssg);
        Console.ResetColor();
    }

    public void StartGame()
    {
        bool aborted = false;
        while(user.Health > 0)
        {
            Console.WriteLine("Choose any one option >> ");
            Console.WriteLine("1. Attack\n2. Health\n3. Abort");
            bool valid = int.TryParse(Console.ReadLine(), out int user_choice);
            if (!valid )
            {
                Console.WriteLine("Please enter a valid choice");
            }

            bool abort = false;
            switch (user_choice)
            {
                case 1:
                    int damage = rnd.Next(1, 101);
                    if(damage >= user.Health/2)
                    {
                        PrintInRed("Critical Hit!!");
                    }
                    user.Health = Math.Max(user.Health - damage, 0);
                    PrintInRed($"Current health: {user.Health}, Damage taken: {damage}\n");
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    int heal_points = rnd.Next(1, 100);
                    user.Health = Math.Min(user.Health + heal_points, 100);
                    PrintStatus(user);
                    Console.ResetColor();
                    break;

                case 3:
                    PrintInRed("Aborting the game!!");
                    abort = true;
                    break;
                default:
                    PrintInRed("Please enter a valid choice!!");
                    break;
            }

            if (abort)
            {
                aborted = true;
                break;
            }
        }

        if(user.Health <= 0)
        {
            PrintInRed($"{user.Name} you lost!! Better luck next time.");
        }
        else if(user.Health > 0 && !aborted)
        {
            PrintInGreen($"Congratulations {user.Name}!!, You won the game.");
        }
        else
        {
            PrintInRed("Game Aborted!!");
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.StartGame();
    }
}
