using System;
using System.Threading;
public class Player
{
    public bool HasItem { get; set; }
    public bool HasSword { get; set; }
    public int Chances { get; set; }

    public Player()
    {
        HasItem = false;
        HasSword = false;
        Chances = 2;
    }

    public void CheckInventory()
    {
        if (HasSword)
            Console.WriteLine("You have a sword in your inventory.");
        else
            Console.WriteLine("Your inventory is empty.");
    }
}

public class Room
{
    public void ExploreRoom(Player player)
    {
        if (!player.HasSword)
        {
            Console.WriteLine("You see a sword on the ground.");
            Console.WriteLine("Do you wish to pick up the sword? (yes/no)");
            string pickSword = Console.ReadLine().ToLower();

            if (pickSword == "yes")
            {
                player.HasSword = true;
                Console.WriteLine("keep moving ...");
                Thread.Sleep(2000);
                Console.WriteLine("You have picked up the sword.");
                Thread.Sleep(1000);
                Console.WriteLine("keep moving ...");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("You decide not to pick up the sword.");
                Thread.Sleep(2000);
            }
        }

        if (!player.HasItem)
        {
            Console.WriteLine("You encounter an NPC.");
            Console.WriteLine("The NPC offers you a choice: solve a riddle or fight.");
            Console.WriteLine("What do you choose? (riddle/fight)");
            string choice = Console.ReadLine().ToLower();

            if (choice == "fight")
            {
                Console.WriteLine("You chose to fight and lost. Game over!");
                Environment.Exit(0);
            }
            else if (choice == "riddle")
            {
                PresentRiddle(player);
            }
        }
    }

    private void PresentRiddle(Player player)
    {
        while (player.Chances > 0 && !player.HasItem)
        {
            Console.WriteLine("You encounter a riddle NPC.");
            Console.WriteLine("With roots that no one sees, taller than trees, stretching out also stretching but what is it that never grows?");
            Console.WriteLine("1. Seed");
            Console.WriteLine("2. Plain");
            Console.WriteLine("3. Mountains");
            Console.WriteLine("4. Time");
            Console.WriteLine("5. Love");

            Console.Write("Your answer: ");
            int answer = Convert.ToInt32(Console.ReadLine());

            if (answer == 3)
            {
                Console.WriteLine("Congratulations! You've solved the riddle and defeated the NPC.");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Incorrect answer. Try again!");
                player.Chances--;

                if (player.Chances <= 0)
                {
                    Console.WriteLine("You have no more chances left. Game over!");
                    Environment.Exit(0);
                }
            }
        }
    }
}

public class Game
{
    private Player player;
    private Room room;

    public Game()
    {
        player = new Player();
        room = new Room();
    }

    public void Run()
    {
        Console.WriteLine("You find yourself in a magical room. Explore and find clues.\n");

        while (true)
        {
            Console.WriteLine("1. Look around");
            if (player.HasSword || player.HasItem)
            {
                Console.WriteLine("2. Check inventory");
                Console.WriteLine("3. Quit");
            }

            Console.Write("Choose an option: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    room.ExploreRoom(player);
                    break;
                case 2:
                    player.CheckInventory();
                    break;
                case 3:
                    Console.WriteLine("Thanks for playing riddle games");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Run();
    }
}