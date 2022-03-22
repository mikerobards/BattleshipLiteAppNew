
using BattleshipLiteNewLibrary;
using BattleshipLiteNewLibrary.Models;

WelcomeMessage();

PlayerInfoModel player1 = CreatePlayer();
PlayerInfoModel player2 = CreatePlayer();

static void WelcomeMessage()
{
    Console.WriteLine("Welcome to Battleship Lite!");
    Console.WriteLine("created by Mike Robards");
    Console.WriteLine();
}

static PlayerInfoModel CreatePlayer()
{
    PlayerInfoModel output = new PlayerInfoModel();

    // Ask the user for their name
    output.UsersName = AskForUsersName();

    // Load up the shot grid
    GameLogic.InitializeGrid(output);

    // Ask the user for their 5 ship placements
    PlaceShips(output);


    // Clear the screen and done
    Console.Clear();

    return output;

}

static string AskForUsersName()
{
    Console.Write("What is your name: ");
    string output = Console.ReadLine();

    return output;

}

static void PlaceShips(PlayerInfoModel model)
{
    do
    {
        Console.Write($"Where do you want to place ship number { model.ShipLocations.Count + 1 }: ");
        string location = Console.ReadLine();

        bool isValidLocation = GameLogic.PlaceShip(model, location);

        if (isValidLocation == false)
        {
            Console.WriteLine("That was not a valid location. Please try again.");
        }

    } while (model.ShipLocations.Count < 5);
}