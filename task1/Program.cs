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
        Console.WriteLine("<--- Welcome to RPGBattler --->");
        Console.Write("Please provide your name to continue >> ");
        string? name = Console.ReadLine();
        if(name == "")
        {
            throw new Exception("Name not provided. Please provide name for continuing");
        }

        user = new User(name, 100);
        rnd = new Random();
    }

    static void PrintStatus(User u)
    {
        Console.Write("Current status: ");
        Console.WriteLine($"Health: {u.Health}\n\n");
    }

    public void StartGame()
    {
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    int damage = rnd.Next(1, 101);
                    if(damage >= user.Health/2)
                    {
                        Console.WriteLine("Critical Hit!!");
                    }
                    user.Health -= damage;
                    PrintStatus(user);
                    Console.ResetColor();
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    int heal_points = rnd.Next(1, 100);
                    user.Health += heal_points;
                    PrintStatus(user);
                    Console.ResetColor();
                    break;

                case 3:
                    Console.WriteLine("Aborting the game!!");
                    abort = true;
                    break;
            }

            if (abort)
            {
                break;
            }
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
